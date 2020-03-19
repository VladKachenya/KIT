using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.InformationModel.Infrastucture.Elements
{
    public interface IDai:IModelElement, INameable
    {
        string Description { get; set; }
        ChildModelProperty<IVal> Value { get; }

    }
}