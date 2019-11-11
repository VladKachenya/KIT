using BISC.Model.Infrastructure.Project;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DaType;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.EnumType;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.LNodeType;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using System.Collections.Generic;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates
{
    public interface IDataTypeTemplatesModelService
    {
        void MergeDataTypeTemplates(ISclModel sclModelTo, ISclModel sclModelFrom);

        void MergeDataTypeTemplatesOfDevice(ISclModel sclModelTo, ISclModel sclModelFrom, IDevice device);

        void FilterDataTypeTemplates(IDataTypeTemplates dataTypeTemplates, List<ILDevice> lDevices, List<ILDevice> lDevicesToLeave);

        string AddLnodeType(ILNodeType lNodeType, ISclModel sclModel);
        string AddDoType(IDoType doType, ISclModel sclModel);
        string AddDaType(IDaType daType, ISclModel sclModel);
        string AddEnumType(IEnumType doType, ISclModel sclModel);
        IDa GetDaOfDai(IDai dai, ISclModel sclModel);
        void UpdateTemplatesUnderIdeName(ISclModel sclModel, string oldIdeName, string newIdeName);
        IEnumType GetEnumTypeForDa(IDa da);


    }
}