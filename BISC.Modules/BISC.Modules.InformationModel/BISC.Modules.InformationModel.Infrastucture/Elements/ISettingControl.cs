using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.InformationModel.Infrastucture.Elements
{
    public interface ISettingControl:IModelElement
    {
       int NumOfSGs { get; set; }
        int ActSG { get; set; }
    }
}