using BISC.Model.Global.Model;
using BISC.Model.Global.Project;
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

        }

        public IModelElement TestBransh1()
        {
            IModelElement result = new BiscProject() { ElementName = "TestElement1", Namespace = "TestLevel1" };
            result.ChildModelElements.Add(null);
            return result;
        }
        #endregion
    }
}
