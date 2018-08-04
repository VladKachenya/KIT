using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Modules.InformationModel.Model.Serializers.DataTypeTemplates;
using BISC.Tests.Model.InitializeModules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BISC.Tests.Model.Model.InfoModel.Serializers
{
    [TestClass]
    public class DataTypeTemplatesSerializerTest: TestBaseClass
    {
        private DataTypeTemplatesSerializer _dataTypeTemplatesSerializer;
        [TestInitialize]
        private void TestInit()
        {
            _dataTypeTemplatesSerializer = StaticContainer.CurrentContainer.ResolveType<DataTypeTemplatesSerializer>();
        }
        [TestMethod]
        public void ShouldInitializeSuccessfull()
        {
            
        }
    }
}
