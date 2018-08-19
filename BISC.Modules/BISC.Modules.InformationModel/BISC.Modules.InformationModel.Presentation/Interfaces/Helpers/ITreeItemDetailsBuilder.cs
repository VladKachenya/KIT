using System.Collections.Generic;
using BISC.Modules.InformationModel.Presentation.Interfaces.InfoModelDetails;

namespace BISC.Modules.InformationModel.Presentation.Interfaces.Helpers
{
    public interface ITreeItemDetailsBuilder
    {
        void Reset();
        void AddStringDetail(string description, string value);

        void AddStringDetailWithToolTip(string description, string value, string toolTip);
        void AddBoolDetailWithToolTip(string description, bool value, string toolTip);
        void AddGroupOfTreeItemDetail(string description, List<IInfoModelDetail> value, string toolTip);
        List<IInfoModelDetail> Build();
    }
}