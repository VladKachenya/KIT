using System.Collections.Generic;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType
{
    public interface IDoType : IModelElement
    {
        string Id { get; set; }
        string LnClass { get; set; }
        List<IDa> DaList { get; }
        List<ISdo> SdoList { get; set; }
    }
}