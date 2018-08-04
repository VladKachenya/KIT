using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BISC.Model.Global.Serializators;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates;
using BISC.Modules.InformationModel.Model.Module;
using BISC.Tests.Model.InitializeModules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BISC.Tests.Model.Model.Global.Serializers
{
    [TestClass]
   public class DataTypeTemplatesSerializerTest:TestBaseClass
    {
        #region Overrides of TestBaseClass

        protected override List<Type> GetTestingModules()
        {
            var modules= base.GetTestingModules();
            modules.Add(typeof(InformationModelModule));
            return modules;
        }

        #endregion


        [TestMethod]
        public void DeserializingDataTypeTemplatesShouldBeSucceed()
        {
            SclModelElementSerializer sclModelElementSerializer = new SclModelElementSerializer();
            var r = sclModelElementSerializer.DeserializeModelElement(XElement.Parse(Properties.Resources.rel670_gooseUROV));
            IDataTypeTemplates dataTypeTemplates =
                r.ChildModelElements.FirstOrDefault((element => element is IDataTypeTemplates)) as IDataTypeTemplates;
            Assert.IsNotNull(dataTypeTemplates);
            Assert.IsNotNull(dataTypeTemplates.DaTypes);
            Assert.IsNotNull(dataTypeTemplates.DoTypes);
            Assert.IsNotNull(dataTypeTemplates.EnumTypes);
            Assert.IsNotNull(dataTypeTemplates.LNodeTypes);

        }

        [TestMethod]
        public void DeserializingDaTypesShouldBeSucceed()
        {
            SclModelElementSerializer sclModelElementSerializer = new SclModelElementSerializer();
            var r = sclModelElementSerializer.DeserializeModelElement(XElement.Parse(Properties.Resources.rel670_gooseUROV));
            IDataTypeTemplates dataTypeTemplates =
                r.ChildModelElements.FirstOrDefault((element => element is IDataTypeTemplates)) as IDataTypeTemplates;
            Assert.IsNotNull(dataTypeTemplates);
            Assert.IsNotNull(dataTypeTemplates.DaTypes);
            Assert.IsNotNull(dataTypeTemplates.DaTypes[0].Id);
            Assert.IsNotNull(dataTypeTemplates.DaTypes[0].Bdas);
            
        }




    }
}
