using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure.Project;
using BISC.Tests.Model.InitializeModules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BISC.Tests.Model.Model.Global.Serializers
{
    [TestClass]
    public class CommunicationSerializerTest : TestBaseClass
    {
        [TestMethod]
        public void DeSerializationMustBeSucceed()
        {
            SclModelElementSerializer sclModelElementSerializer = new SclModelElementSerializer();
            var sclModel = sclModelElementSerializer.DeserializeModelElement(XElement.Parse(Properties.Resources.rel670_gooseUROV));
            Assert.IsNotNull(sclModel.ChildModelElements.FirstOrDefault((element => element is ISclCommunicationModel)));

        }


        [TestMethod]
        public void SubnetworksMustBeFilled()
        {
            SclModelElementSerializer sclModelElementSerializer = new SclModelElementSerializer();
            var sclModel = sclModelElementSerializer.DeserializeModelElement(XElement.Parse(Properties.Resources.rel670_gooseUROV));
            ISclCommunicationModel sclCommunicationModel=sclModel.ChildModelElements.FirstOrDefault((element => element is ISclCommunicationModel)) as ISclCommunicationModel;
            Assert.IsNotNull(sclCommunicationModel);
            Assert.AreEqual(sclCommunicationModel.SubNetworks.Count,1); 
            Assert.AreEqual(sclCommunicationModel.SubNetworks[0].Name,"WA1");
            Assert.AreEqual(sclCommunicationModel.SubNetworks[0].Desc, "Subnetwork");
            Assert.AreEqual(sclCommunicationModel.SubNetworks[0].Type, "8-MMS");
            Assert.AreEqual(sclCommunicationModel.ChildModelElements.Count, 1);

        }
    }
}
