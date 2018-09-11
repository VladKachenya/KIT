using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Model.Global.Model;
using BISC.Model.Iec61850Ed2.Module;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Connection.MMS.Module;
using BISC.Modules.Connection.Model.Module;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Services;
using BISC.Modules.InformationModel.Model.Module;
using BISC.Modules.InformationModel.Model.Services;
using BISC.Tests.Model.Helpers;
using BISC.Tests.Model.InitializeModules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BISC.Tests.Model.Connection
{
    [TestClass]
    public class ConnectionTests : TestBaseClass
    {
        protected override List<Type> GetTestingModules()
        {
            var modules = base.GetTestingModules();
            modules.Add(typeof(InformationModelModule));
            modules.Add(typeof(ConnectionMmsModule));
            modules.Add(typeof(ConnectionModelModule));
            modules.Add(typeof(Iec61850Ed2Module));
            return modules;
        }

        [TestMethod]
        public void StartConnectionMustSucceed()
        {
            IConnectionPoolService connectionPoolService =
                StaticContainer.CurrentContainer.ResolveType<IConnectionPoolService>();
            var connection = connectionPoolService.GetConnection("127.0.0.1");
            connection.OpenConnection().Wait();
            Assert.IsTrue(connection.IsConnected);
            connection.StopConnection();
            Assert.IsFalse(connection.IsConnected);
        }

        [TestMethod]
        public void ShouldIdentifySucceed()
        {
            IConnectionPoolService connectionPoolService =
                StaticContainer.CurrentContainer.ResolveType<IConnectionPoolService>();
            var connection = connectionPoolService.GetConnection("127.0.0.1");
            connection.OpenConnection().Wait();
            var task = connection.MmsConnection.IdentifyAsync();
            task.Wait();
            var ident = task.Result;
            Assert.IsTrue(ident.Item.Count == 3);

            connection.StopConnection();
        }

        [TestMethod]
        public void ShouldGetLdSucceed()
        {
            IConnectionPoolService connectionPoolService =
                StaticContainer.CurrentContainer.ResolveType<IConnectionPoolService>();
            var connection = connectionPoolService.GetConnection("127.0.0.1");
            connection.OpenConnection().Wait();
            var task = connection.MmsConnection.GetLdListAsync();
            task.Wait();
            var ldList = task.Result;
            Assert.IsTrue(ldList.Item.Count != 0);

            connection.StopConnection();
        }


        [TestMethod]
        public void ShouldGetLnsSucceed()
        {
            IConnectionPoolService connectionPoolService =
                StaticContainer.CurrentContainer.ResolveType<IConnectionPoolService>();
            var connection = connectionPoolService.GetConnection("127.0.0.1");
            connection.OpenConnection().Wait();
            var task = connection.MmsConnection.GetLdListAsync();
            task.Wait();
            var ldList = task.Result;
            Assert.IsTrue(ldList.Item.Count != 0);
            var lns = connection.MmsConnection.GetListValiablesAsync(ldList.Item.First()).RunSync();
            Assert.IsTrue(lns.IsSucceed);
            connection.StopConnection();
        }
        [TestMethod]
        public async Task DataTypeTemplatesMustBeFilled()
        {
            IConnectionPoolService connectionPoolService =
                StaticContainer.CurrentContainer.ResolveType<IConnectionPoolService>();
            await connectionPoolService.GetConnection("127.0.0.1").OpenConnection();
            var sclModel = new SclModel();
            ILogicalDeviceLoadingService logicalDeviceLoadingService = StaticContainer.CurrentContainer.ResolveType<ILogicalDeviceLoadingService>();
            await logicalDeviceLoadingService.PrepareProgressData("127.0.0.1");
            var result = await logicalDeviceLoadingService.GetLDeviceFromConnection(new Progress<LogicalNodeLoadingEvent>((event1 => { })),
                   sclModel, "MR771N123");

            IDataTypeTemplates dataTypeTemplates = sclModel.ChildModelElements.First((element => element is IDataTypeTemplates)) as IDataTypeTemplates;
            foreach (var lNodeType in dataTypeTemplates.LNodeTypes)
            {
                Assert.IsNotNull(lNodeType.LnClass);
                Assert.IsNotNull(lNodeType.DoList);
                Assert.IsNotNull(lNodeType.Id);
                Assert.IsNotNull(lNodeType.ChildModelElements);
                Assert.IsNotNull(lNodeType.ElementName);
                foreach (var dDo in lNodeType.DoList)
                {
                    Assert.IsNotNull(dDo.Type);
                    Assert.IsNotNull(dDo.Name);
                    Assert.IsNotNull(dDo.ChildModelElements);
                    Assert.IsNotNull(dDo.ElementName);
                }
            }

            foreach (var doType in dataTypeTemplates.DoTypes)
            {
                Assert.IsNotNull(doType.DaList);
                Assert.IsNotNull(doType.SdoList);
                Assert.IsNotNull(doType.Id);
                Assert.IsNotNull(doType.Cdc);
                Assert.IsNotNull(doType.ChildModelElements);
                Assert.IsNotNull(doType.ElementName);
                foreach (var da in doType.DaList)
                {
                    if (da.BType == "Struct")
                        Assert.IsNotNull(da.Type);

                    Assert.IsNotNull(da.Name);
                    Assert.IsNotNull(da.ChildModelElements);
                    Assert.IsNotNull(da.ElementName);
                    Assert.IsNotNull(da.Fc);
                    Assert.IsNotNull(da.BType);
                }
            }

            foreach (var daType in dataTypeTemplates.DaTypes)
            {
                Assert.IsNotNull(daType.Bdas);
                Assert.IsNotNull(daType.Id);
                Assert.IsNotNull(daType.ChildModelElements);
                Assert.IsNotNull(daType.ElementName);
                foreach (var bda in daType.Bdas)
                {
                    Assert.IsNotNull(bda.BType);
                    if (bda.BType == "Struct"|| bda.BType == "Enum")
                        Assert.IsNotNull(bda.Type);
                    Assert.IsNotNull(bda.Name);
                    Assert.IsNotNull(bda.ChildModelElements);
                    Assert.IsNotNull(bda.ElementName);
                }
            }
            foreach (var enumType in dataTypeTemplates.EnumTypes)
            {
                Assert.IsNotNull(enumType.EnumValList);
                Assert.IsNotNull(enumType.Id);
                Assert.IsNotNull(enumType.ChildModelElements);
                Assert.IsNotNull(enumType.ElementName);
                foreach (var enumVal in enumType.EnumValList)
                {
                    Assert.IsNotNull(enumVal.Ord);
                    Assert.IsNotNull(enumVal.Value);
                    Assert.IsNotNull(enumVal.ChildModelElements);
                    Assert.IsNotNull(enumVal.ElementName);
                }
            }

        }

        [TestMethod]
        public async Task ShouldDeleteDatatypeTemplatesAfterLoading()
        {
            IConnectionPoolService connectionPoolService =
                StaticContainer.CurrentContainer.ResolveType<IConnectionPoolService>();
            await connectionPoolService.GetConnection("127.0.0.1").OpenConnection();
            var sclModel = new SclModel();
            ILogicalDeviceLoadingService logicalDeviceLoadingService = StaticContainer.CurrentContainer.ResolveType<ILogicalDeviceLoadingService>();
            await logicalDeviceLoadingService.PrepareProgressData("127.0.0.1");
            var result = await logicalDeviceLoadingService.GetLDeviceFromConnection(new Progress<LogicalNodeLoadingEvent>((event1 => { })),
                sclModel, "MR771N123");

            IDataTypeTemplates dataTypeTemplates = sclModel.ChildModelElements.First((element => element is IDataTypeTemplates)) as IDataTypeTemplates;
            IDataTypeTemplatesModelService dataTypeTemplatesModelService =
                StaticContainer.CurrentContainer.ResolveType<IDataTypeTemplatesModelService>();
            dataTypeTemplatesModelService.FilterDataTypeTemplates(dataTypeTemplates,result,new List<ILDevice>());
            Assert.AreEqual(dataTypeTemplates.DoTypes.Count , 0);
            Assert.AreEqual(dataTypeTemplates.DaTypes.Count, 0);
            Assert.AreEqual(dataTypeTemplates.EnumTypes.Count, 0);
            Assert.AreEqual(dataTypeTemplates.LNodeTypes.Count, 0);

        }


    }
}
