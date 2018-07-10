using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Infrastructure.Tree;
using BISC.Presentation.Interfaces.Tree;

namespace BISC.Presentation.Services
{
   public class TreeManagementService: ITreeManagementService
    {
        private readonly IMainTreeViewModel _mainTreeViewModel;

        public TreeManagementService(IMainTreeViewModel mainTreeViewModel)
        {
            _mainTreeViewModel = mainTreeViewModel;
        }
        
        #region Implementation of ITreeManagementService

        #endregion

        #region Implementation of ITreeManagementService

        public void AddTreeItem(IMainTreeItem treeItem, string viewName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
