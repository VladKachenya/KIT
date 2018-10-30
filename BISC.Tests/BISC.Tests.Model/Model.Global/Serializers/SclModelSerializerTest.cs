using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BISC.Infrastructure.Global.IoC;
using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure.Serializing;
using BISC.Tests.Model.InitializeModules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates;

namespace BISC.Tests.Model.Model.Global.Serializers
{
    [TestClass]
    public class SclModelSerializerTest : TestBaseClass
    {
        [TestMethod]
        public void DeserializingShouldBeSucceed()
        {
            SclModelElementSerializer sclModelElementSerializer = new SclModelElementSerializer();
            var r = sclModelElementSerializer.DeserializeModelElement(XElement.Parse(Properties.Resources.rel670_gooseUROV));
        }

        [TestMethod]
        public void SerializingMustBeSucceed()
        {

            SclModelElementSerializer sclModelElementSerializer=new SclModelElementSerializer();
            var deserializedElement = sclModelElementSerializer.DeserializeModelElement(XElement.Parse(Properties.Resources.rel670_gooseUROV));

            var serializedFile = sclModelElementSerializer.SerializeModelElement(deserializedElement,SerializingType.Extended);
        }
        [TestMethod]
        public void SerializingMustBeSucceed_SclModelFilled()
        {

            SclModelElementSerializer sclModelElementSerializer = new SclModelElementSerializer();
            var deserializedElementEtalon = sclModelElementSerializer.DeserializeModelElement(XElement.Parse(Properties.Resources.rel670_gooseUROV));
            
            var serializedFile = sclModelElementSerializer.SerializeModelElement(deserializedElementEtalon,SerializingType.Extended);

            var deserializedElementResult=   sclModelElementSerializer.DeserializeModelElement(serializedFile);
        }


    }
}
