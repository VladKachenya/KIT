using BISC.Model.Infrastructure.Project;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.LNodeType;

namespace BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates
{
    public interface IDataTypeTemplatesModelService
    {
        IDataTypeTemplates MergeDataTypeTemplates(IDataTypeTemplates dataTypeTemplates1,
            IDataTypeTemplates dataTypeTemplates2);

        void AddLnodeType(ILNodeType lNodeType,ISclModel sclModel);
        void AddDoType(IDoType doType,ISclModel sclModel);

    }
}