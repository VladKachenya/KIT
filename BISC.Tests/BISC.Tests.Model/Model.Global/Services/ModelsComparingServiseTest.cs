using BISC.Model.Global.Model;
using BISC.Model.Global.Model.Communication;
using BISC.Model.Global.Project;
using BISC.Model.Global.Services;
using BISC.Model.Infrastructure.Elements;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Tests.Model.Model.Global.Services
{
    [TestClass]
    public class ModelsComparingServiseTest
    {

        #region CompareBranches NotNullResult

        [TestMethod]
        public void CompareBranches_TestBransh1CompareTestBransh1_NullListOfIMismatch()
        {
            ModelsComparingServise modelsComparingServise = new ModelsComparingServise();
            IModelElement testBransh1 = TestBransh1();
            IModelElement testBransh2 = TestBransh1();
            var result = modelsComparingServise.CompareBranches(testBransh1, testBransh2);
            Assert.AreEqual(result.Count, 0);
        }

        private IModelElement TestBransh1()
        {
            IModelElement result = new BiscProject() { ElementName = "TestElement1", Namespace = "TestLevel1" };
            FormLevel2(result);
            return result;
        }

        private void FormLevel2(IModelElement element)
        {
            IModelElement[] childrens =
                {
                    new AddressProperty{ Type = "T1", ElementName = "Element", Namespace = "TestLevel2", Value = "HellowTest"},
                    new SclAddress{ ElementName = "TestElement", Namespace = "TestLevel2", },
                    new ModelElement{ ElementName = "TestElement2", Namespace = "TestLeve2"},
                    new ModelElement{ElementName = "TestElement3", Namespace = "TestLeve2"}
                };
            element.ChildModelElements.AddRange(childrens);
        }

        private void FormLevel3 (IModelElement element)
        {

        }
        #endregion
    }
}
