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
   public class LogicalNodeInfoModelItemViewModel : TreeItemViewModelBase
    {
        private List<IInfoModelDetail> _treeItemDetails;
        private readonly ITreeItemDetailsBuilder _treeItemDetailsBuilder;

        public LogicalNodeInfoModelItemViewModel(ITreeItemDetailsBuilder treeItemDetailsBuilder)
        {
            _treeItemDetailsBuilder = treeItemDetailsBuilder;
        }
        public override List<IInfoModelDetail> TreeItemDetails
        {
            get { return _treeItemDetails; }
        }
        #region Overrides of TreeItemViewModelBase

        public override Brush TypeColorBrush =>new SolidColorBrush(Color.FromArgb(0x5F,0x40,0x80,0x80));

        public override string TypeName => "LN";

        #endregion


        #region Overrides of TreeItemViewModelBase

        protected override void SetModel(object value)
        {
            ILogicalNode ln = (value as ILogicalNode);
            Header = ln.Prefix+ln.LnClass+ln.Inst;
            base.SetModel(value);
            _treeItemDetailsBuilder.Reset();
            _treeItemDetailsBuilder.AddStringDetail("Имя", Header);
            _treeItemDetailsBuilder.AddStringDetailWithToolTip("lnType", ln.LnType, "Ссылка на определение типа данного LN");
            _treeItemDetailsBuilder.AddStringDetailWithToolTip("class", ln.LnClass, "Класс LN");
            _treeItemDetails = _treeItemDetailsBuilder.Build();
            base.SetModel(value);
        }

        #endregion
    }
}