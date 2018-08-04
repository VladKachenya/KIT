using System;
using BISC.Infrastructure.Global.IoC;
using BISC.Model.Global.Serializators.Communication;
using BISC.Model.Global.Services;
using BISC.Tests.Model.InitializeModules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BISC.Tests.Model
{
    [TestClass]
    public class UnitTest1:TestBaseClass
    {
        [TestMethod]
        public void TestMethod1()
        {
            StaticContainer.CurrentContainer.RegisterType<ModelComposingService>();
        }
    }
}
