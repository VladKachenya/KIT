using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BISC.Model.Global.Common;
using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure.Common;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Model.Module;
using BISC.Tests.Model.InitializeModules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BISC.Tests.Model.Model.InfoModel.Serializers
{
    [TestClass]
    public class LDeviceSerializerTest : TestBaseClass
    {

        protected override List<Type> GetTestingModules()
        {
            var modules = base.GetTestingModules();
            modules.Add(typeof(InformationModelModule));
            return modules;
        }


        [TestMethod]
        public void ShouldDeSerializeLDevice()
        {
            SclModelElementSerializer sclModelElementSerializer = new SclModelElementSerializer();
            var r = sclModelElementSerializer.DeserializeModelElement(XElement.Parse(Properties.Resources.rel670_gooseUROV));
           bool isFinded= r.TryGetFirstChildOfType(out ILDevice lDevice);
            Assert.IsTrue(isFinded);
            List<ILDevice> lDevices=new List<ILDevice>();
            r.GetAllChildrenOfType(ref lDevices);
            Assert.IsTrue(lDevices.Count==8);

            foreach (var lDeviceItem in lDevices)
            {
                Assert.IsFalse(string.IsNullOrEmpty(lDeviceItem.Inst));
                Assert.IsFalse(string.IsNullOrEmpty(lDeviceItem.ElementName));
                Assert.IsNotNull(lDeviceItem.LogicalNodeZero);
                Assert.IsNotNull(lDeviceItem.LogicalNodes);
                Assert.IsTrue(lDeviceItem.LogicalNodes.Count>0);
            }
            
        }

        [TestMethod]
        public void ShouldSerializeLDevice()
        {
            SclModelElementSerializer sclModelElementSerializer = new SclModelElementSerializer();
            var r = sclModelElementSerializer.DeserializeModelElement(XElement.Parse(Properties.Resources.rel670_gooseUROV));
            bool isFinded = r.TryGetFirstChildOfType(out ILDevice lDevice);
            Assert.IsTrue(isFinded);
            List<ILDevice> lDevices = new List<ILDevice>();
            r.GetAllChildrenOfType(ref lDevices);
            Assert.IsTrue(lDevices.Count == 8);

            foreach (var lDeviceItem in lDevices)
            {
                Assert.IsFalse(string.IsNullOrEmpty(lDeviceItem.Inst));
                Assert.IsFalse(string.IsNullOrEmpty(lDeviceItem.ElementName));
                Assert.IsNotNull(lDeviceItem.LogicalNodeZero);
                Assert.IsNotNull(lDeviceItem.LogicalNodes);
                Assert.IsTrue(lDeviceItem.LogicalNodes.Count > 0);
            }

        }



    }
}
