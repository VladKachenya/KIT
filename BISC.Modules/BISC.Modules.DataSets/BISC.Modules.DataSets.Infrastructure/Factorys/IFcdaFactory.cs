using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.DataSets.Infrastructure.Factorys
{
    public interface IFcdaFactory
    {
        IFcda GetFcda(IDai dai);
        IFcda GetStructFcda(IModelElement modelElement, string fc);
        //IFcda GetStructFcda(IModelElement element);

        List<IFcda> GetFcdasFromModelElement(IModelElement modelElement);
    }
}
