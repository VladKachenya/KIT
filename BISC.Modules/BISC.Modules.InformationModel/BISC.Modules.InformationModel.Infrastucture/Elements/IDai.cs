using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.InformationModel.Infrastucture.Elements
{
    public interface IDai:IModelElement
    {
        string Name { get; set; }
        string Description { get; set; }
        IVal Value { get; set; }
    }
}