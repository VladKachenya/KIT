using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Presentation.Interfaces.Helpers;
using BISC.Modules.InformationModel.Presentation.Interfaces.InfoModelDetails;
using BISC.Modules.InformationModel.Presentation.ViewModels.Base;

namespace BISC.Modules.InformationModel.Presentation.ViewModels.InfoModelTree
{
   public class SdiInfoModelItemViewModel : TreeItemViewModelBase
    {
        private List<IInfoModelDetail> _treeItemDetails;
        private readonly ITreeItemDetailsBuilder _treeItemDetailsBuilder;

        public SdiInfoModelItemViewModel(ITreeItemDetailsBuilder treeItemDetailsBuilder)
        {
            _treeItemDetailsBuilder = treeItemDetailsBuilder;
        }
        public override List<IInfoModelDetail> TreeItemDetails
        {
            get { return _treeItemDetails; }
        }
        #region Overrides of TreeItemViewModelBase

        public override Brush TypeColorBrush =>new SolidColorBrush(Color.FromArgb(0x5F,0x0A,0x09,0x50));

        public override string TypeName => "SDI";

        #endregion


        #region Overrides of TreeItemViewModelBase

        protected override void SetModel(object value)
        {
            ISdi sdi = (value as ISdi);
            Header = sdi.Name;
            _treeItemDetailsBuilder.Reset();
            _treeItemDetailsBuilder.AddStringDetail("Имя", sdi.Name);
            _treeItemDetails = _treeItemDetailsBuilder.Build();
            base.SetModel(value);
        }

        #endregion
    }
}
