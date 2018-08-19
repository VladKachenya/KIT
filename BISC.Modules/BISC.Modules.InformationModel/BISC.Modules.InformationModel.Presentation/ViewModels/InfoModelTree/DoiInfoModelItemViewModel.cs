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
  public  class DoiInfoModelItemViewModel : TreeItemViewModelBase
    {
        private List<IInfoModelDetail> _treeItemDetails;
        private readonly ITreeItemDetailsBuilder _treeItemDetailsBuilder;

        public DoiInfoModelItemViewModel(ITreeItemDetailsBuilder treeItemDetailsBuilder)
        {
            _treeItemDetailsBuilder = treeItemDetailsBuilder;
        }


        #region Overrides of TreeItemViewModelBase

 
        public override string TypeName => "DOI";

        public override Brush TypeColorBrush => new SolidColorBrush(Color.FromArgb(0x7F,0xA0,0x60,0x50));

        public override List<IInfoModelDetail> TreeItemDetails
        {
            get { return _treeItemDetails; }
        }

        #endregion


        #region Overrides of TreeItemViewModelBase

        protected override void SetModel(object value)
        {
            IDoi doi = (value as IDoi);
            Header = doi.Name;
            _treeItemDetailsBuilder.Reset();
            _treeItemDetailsBuilder.AddStringDetail("Имя", doi.Name);
            _treeItemDetails = _treeItemDetailsBuilder.Build();
            base.SetModel(value);
        }

        #endregion
    }
}