using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BISC.Modules.InformationModel.Infrastucture;
using BISC.Modules.InformationModel.Infrastucture.Elements;
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

        public override Brush TypeColorBrush => new SolidColorBrush(Color.FromArgb(0x70, 0x70, 0x70, 0x70));

        public override string TypeName => InfoModelKeys.ModelKeys.FcSetKey;

        #endregion


        #region Overrides of TreeItemViewModelBase

        public void SetFc(object value,IDoi doiParent)
        {
            Header = value.ToString();
            _treeItemDetailsBuilder.Reset();
            _treeItemDetails = _treeItemDetailsBuilder.Build();
            base.SetModel(doiParent);

        }

        #endregion
    }
}
