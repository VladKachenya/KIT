using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.InformationModel.Presentation.Interfaces;
using BISC.Modules.InformationModel.Presentation.Interfaces.Helpers;
using BISC.Modules.InformationModel.Presentation.Interfaces.InfoModelDetails;
using BISC.Modules.InformationModel.Presentation.ViewModels.InfoModelDetails;

namespace BISC.Modules.InformationModel.Presentation.Helpers
{
    public class TreeItemDetailsBuilder : ITreeItemDetailsBuilder
    {
        private List<IInfoModelDetail> _infoModelDetails;

        public TreeItemDetailsBuilder()
        {

        }



        #region Implementation of ITreeItemDetailsBuilder

        public void Reset()
        {
            _infoModelDetails = new List<IInfoModelDetail>();
        }

        public void AddStringDetail(string description, string value)
        {
            IInfoModelDetail treeItemDetail = new DefaultInfoModelDetail();
            treeItemDetail.DetailDescription = description;
            treeItemDetail.DetailValue = value;
            _infoModelDetails.Add(treeItemDetail);
        }

        public void AddStringDetailWithToolTip(string description, string value, string toolTip)
        {
            IInfoModelDetail treeItemDetail =new DefaultInfoModelDetail();
            treeItemDetail.DetailDescription = description;
            treeItemDetail.DetailValue = value;
            treeItemDetail.ToolTip = toolTip;
            _infoModelDetails.Add(treeItemDetail);
        }

        public void AddBoolDetailWithToolTip(string description, bool value, string toolTip)
        {
            IInfoModelDetail treeItemDetail =new BoolInfoModelDetail();
            treeItemDetail.DetailDescription = description;
            treeItemDetail.DetailValue = value;
            treeItemDetail.ToolTip = toolTip;
            _infoModelDetails.Add(treeItemDetail);
        }

        public void AddGroupOfTreeItemDetail(string description, List<IInfoModelDetail> value, string toolTip)
        {
            IInfoModelDetail treeItemDetail = new GroupInfoModelDetail();
            treeItemDetail.DetailDescription = description;
            treeItemDetail.DetailValue = value;
            treeItemDetail.ToolTip = toolTip;
            _infoModelDetails.Add(treeItemDetail);
            foreach (var treeItemDetailInValue in value)
            {
                treeItemDetailInValue.IsGrouped = true;
                _infoModelDetails.Add(treeItemDetailInValue);
            }
        }

        public List<IInfoModelDetail> Build()
        {
            return _infoModelDetails;
        }

        #endregion
    }
}
