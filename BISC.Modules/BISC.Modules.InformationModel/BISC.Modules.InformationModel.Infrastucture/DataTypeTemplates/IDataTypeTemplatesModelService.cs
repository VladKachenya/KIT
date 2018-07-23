namespace BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates
{
    public interface IDataTypeTemplatesModelService
    {
        IDataTypeTemplates MergeDataTypeTemplates(IDataTypeTemplates dataTypeTemplates1,
            IDataTypeTemplates dataTypeTemplates2);
      
    }
}