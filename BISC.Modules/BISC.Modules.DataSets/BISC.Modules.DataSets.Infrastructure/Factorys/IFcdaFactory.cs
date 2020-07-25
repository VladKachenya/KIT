using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;

namespace BISC.Modules.DataSets.Infrastructure.Factorys
{
    public interface IFcdaFactory
    {
        IFcda GetFcda(IDai dai, string fc = null, ISclModel sclModel = null);
        IFcda GetStructFcda(IModelElement modelElement, string fc, ISclModel sclModel = null);
        //IFcda GetStructFcda(IModelElement element);

        List<IFcda> GetFcdasFromModelElement(IModelElement modelElement, ISclModel sclModel = null);
    }
}
