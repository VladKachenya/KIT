using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Connection.MMS.Module;
using BISC.Modules.Connection.Model.Module;
using BISC.Modules.InformationModel.Model.Module;
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
            return modules;
        }

        [TestMethod]
        public void StartConnectionMustSucceed()
        {
            IConnectionPoolService connectionPoolService =
                StaticContainer.CurrentContainer.ResolveType<IConnectionPoolService>();
           var connection= connectionPoolService.GetConnection("127.0.0.1");
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
           var ident= task.Result;
            Assert.IsTrue(ident.Item.Count==3);

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

    }
}
