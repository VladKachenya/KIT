using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure.Common;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Model.Module;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Model.Module;
using BISC.Tests.Model.InitializeModules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BISC.Tests.Model.Model.Global.Serializers
{
    [TestClass]
    public class DeviceAccessPointSerializerTest : TestBaseClass
    {
        #region Overrides of TestBaseClass

        protected override List<Type> GetTestingModules()
        {
            var modules = base.GetTestingModules();
            modules.Add(typeof(InformationModelModule));
            modules.Add(typeof(DeviceModelModule));

            return modules;
        }

        #endregion



        [TestMethod]
        public void DeserializingDataTypeTemplatesShouldBeSucceed()
        {
            SclModelElementSerializer sclModelElementSerializer = new SclModelElementSerializer();
            var r = sclModelElementSerializer.DeserializeModelElement(
                XElement.Parse(Properties.Resources.rel670_gooseUROV));
            List<IDevice> devices = new List<IDevice>();

            r.GetAllChildrenOfType(ref devices);
            Assert.IsTrue(devices.Count == 2);
            devices.FirstOrDefault().TryGetFirstChildOfType(out IDeviceAccessPoint deviceAccessPoint);
            Assert.IsTrue(deviceAccessPoint.Name!=null);
            Assert.IsTrue(deviceAccessPoint.DeviceServer != null);

        }
    }
}