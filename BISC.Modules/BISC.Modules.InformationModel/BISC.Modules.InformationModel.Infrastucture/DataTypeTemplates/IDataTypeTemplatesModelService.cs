using BISC.Model.Infrastructure.Project;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DaType;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.EnumType;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.LNodeType;
using BISC.Modules.InformationModel.Infrastucture.Elements;

namespace BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates
{
    public interface IDataTypeTemplatesModelService
    {
        void MergeDataTypeTemplates(ISclModel sclModelTo,
            ISclModel sclModelFrom);

        string AddLnodeType(ILNodeType lNodeType,ISclModel sclModel);
        string AddDoType(IDoType doType,ISclModel sclModel);
        string AddDaType(IDaType daType, ISclModel sclModel);
        string AddEnumType(IEnumType doType, ISclModel sclModel);
        IDa GetDaOfDai(IDai dai,ISclModel sclModel);

    }
}