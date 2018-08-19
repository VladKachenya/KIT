using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.InformationModel.Infrastucture.Elements
{
    public interface IVal:IModelElement
    {
        string Value { get; set; }
    }
}