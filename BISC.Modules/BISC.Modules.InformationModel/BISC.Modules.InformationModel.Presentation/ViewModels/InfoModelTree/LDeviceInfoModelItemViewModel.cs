using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Presentation.Interfaces;
using BISC.Modules.InformationModel.Presentation.Interfaces.Helpers;
using BISC.Modules.InformationModel.Presentation.Interfaces.InfoModelDetails;
using BISC.Modules.InformationModel.Presentation.ViewModels.Base;
using BISC.Presentation.BaseItems.ViewModels.Behaviors;

namespace BISC.Modules.InformationModel.Presentation.ViewModels.InfoModelTree
{
    public class LDeviceInfoModelItemViewModel : TreeItemViewModelBase
    {
        private List<IInfoModelDetail> _treeItemDetails;
        private readonly ITreeItemDetailsBuilder _treeItemDetailsBuilder;

        public LDeviceInfoModelItemViewModel(ITreeItemDetailsBuilder treeItemDetailsBuilder)
        {
            _treeItemDetailsBuilder = treeItemDetailsBuilder;
            BlockViewModelBehavior = new BlockViewModelBehavior();

        }

        public override List<IInfoModelDetail> TreeItemDetails
        {
            get { return _treeItemDetails; }
        }

        #region Overrides of TreeItemViewModelBase

        public override Brush TypeColorBrush => new SolidColorBrush(Color.FromArgb(0x5F,0x00,0x60,0x90));

        public override string TypeName => "LD";

        #endregion


        #region Overrides of TreeItemViewModelBase

        protected override void SetModel(object value)
        {
            ILDevice lDevice = (value as ILDevice);
            Header = lDevice.Inst;
            base.SetModel(value);
            _treeItemDetailsBuilder.Reset();
            _treeItemDetailsBuilder.AddStringDetailWithToolTip("inst", lDevice.Inst,
                "Идентификация LDevice в пределах IED-устройства");
            _treeItemDetails = _treeItemDetailsBuilder.Build();
            base.SetModel(value);
        }

        #endregion
    }
}