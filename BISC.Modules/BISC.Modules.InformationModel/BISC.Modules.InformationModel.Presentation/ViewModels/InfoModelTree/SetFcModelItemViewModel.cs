using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.InformationModel.Presentation.Interfaces.Helpers;
using BISC.Modules.InformationModel.Presentation.Interfaces.InfoModelDetails;
using BISC.Modules.InformationModel.Presentation.ViewModels.Base;

namespace BISC.Modules.InformationModel.Presentation.ViewModels.InfoModelTree
{
   public class SetFcTreeItemViewModel : TreeItemViewModelBase
    {

        private List<IInfoModelDetail> _treeItemDetails;
        private readonly ITreeItemDetailsBuilder _treeItemDetailsBuilder;

        public SetFcTreeItemViewModel(ITreeItemDetailsBuilder treeItemDetailsBuilder)
        {
            _treeItemDetailsBuilder = treeItemDetailsBuilder;
        }
        public override List<IInfoModelDetail> TreeItemDetails
        {
            get { return _treeItemDetails; }
        }

        #region Overrides of TreeItemViewModelBase




        public override string TypeName => "";

        #endregion


        #region Overrides of TreeItemViewModelBase

        public new void SetModel(object value)
        {
            Header = value.ToString();
            _treeItemDetailsBuilder.Reset();
            _treeItemDetails = _treeItemDetailsBuilder.Build();
            base.SetModel(value);

        }

        #endregion
    }
}
