using BISC.Model.Infrastructure.Project;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DaType;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.EnumType;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.LNodeType;

namespace BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates
{
    public interface IDataTypeTemplatesModelService
    {
        IDataTypeTemplates MergeDataTypeTemplates(IDataTypeTemplates dataTypeTemplates1,
            IDataTypeTemplates dataTypeTemplates2);

        string AddLnodeType(ILNodeType lNodeType,ISclModel sclModel);
        string AddDoType(IDoType doType,ISclModel sclModel);
        string AddDaType(IDaType daType, ISclModel sclModel);

        string AddEnumType(IEnumType doType, ISclModel sclModel);

    }
}