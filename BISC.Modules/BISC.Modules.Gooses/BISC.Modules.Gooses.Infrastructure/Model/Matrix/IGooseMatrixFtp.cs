using System.Xml.Serialization;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.Gooses.Infrastructure.Model.Matrix
{
    public interface IGooseMatrixFtp : IModelElement
    {
        //string DeviceOwnerName { get; set; }
        ChildModelsList<IMacAddressEntity> MacAddressList { get; }
        ChildModelsList<IGoCbFtpEntity> GoCbFtpEntities { get; }
        ChildModelsList<IGooseRowFtpEntity> GooseRowFtpEntities { get; }
        ChildModelsList<IGooseRowQualityFtpEntity> GooseRowQualityFtpEntities { get; }
    }

    public interface IMacAddressEntity : IModelElement
    {
        string MacAddress { get; set; }
    }
    public interface IGoCbFtpEntity : IModelElement
    {
        int IndexOfGoose { get; set; }
        string GoCbReference { get; set; } // MR771N125LD0/LLN0$GO$gcbIn
        string AppId { get; set; }
        [XmlElement(IsNullable = true)]
        int? ConfRev { get; set; }

    }

    public interface IGooseRowFtpEntity : IModelElement
    {
        int IndexOfGoose { get; set; }
        int NumberOfFcdaInDataSetOfGoose { get; set; }
        int BitIndex { get; set; }

    }
    public interface IGooseRowQualityFtpEntity : IGooseRowFtpEntity
    {
        bool IsValiditySelected { get; set; }

    }
}