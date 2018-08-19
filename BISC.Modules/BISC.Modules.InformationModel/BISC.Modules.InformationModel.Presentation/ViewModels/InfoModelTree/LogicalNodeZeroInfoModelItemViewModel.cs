using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Presentation.Interfaces.Helpers;
using BISC.Modules.InformationModel.Presentation.Interfaces.InfoModelDetails;
using BISC.Modules.InformationModel.Presentation.ViewModels.Base;

namespace BISC.Modules.InformationModel.Presentation.ViewModels.InfoModelTree
{
    public class LogicalNodeZeroInfoModelItemViewModel : TreeItemViewModelBase
    {
        private List<IInfoModelDetail> _treeItemDetails;
        private readonly ITreeItemDetailsBuilder _treeItemDetailsBuilder;

        public LogicalNodeZeroInfoModelItemViewModel(ITreeItemDetailsBuilder treeItemDetailsBuilder)
        {
            _treeItemDetailsBuilder = treeItemDetailsBuilder;
        }

        public override List<IInfoModelDetail> TreeItemDetails
        {
            get { return _treeItemDetails; }
        }

        #region Overrides of TreeItemViewModelBase

        public override Brush TypeColorBrush =>new SolidColorBrush(Color.FromArgb(0xAF,0x20,0x40,0x40));

        public override string TypeName => "LN0";

        #endregion


        #region Overrides of TreeItemViewModelBase

        protected override void SetModel(object value)
        {
            ILogicalNodeZero ln0 = (value as ILogicalNodeZero);
            Header = ln0.LnClass;
            base.SetModel(value);
            _treeItemDetailsBuilder.Reset();
            _treeItemDetailsBuilder.AddStringDetailWithToolTip("lnType", ln0.LnType,
                "Ссылка на определение типа данного LN");
            _treeItemDetailsBuilder.AddStringDetailWithToolTip("class", ln0.LnClass,
                "Класс LN0. Всегда является LLN0");
      

            _treeItemDetails = _treeItemDetailsBuilder.Build();
            base.SetModel(value);
        }

        #endregion
    }
}