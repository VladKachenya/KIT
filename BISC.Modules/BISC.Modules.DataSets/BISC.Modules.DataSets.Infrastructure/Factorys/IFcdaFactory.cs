using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.DataSets.Infrastructure.Factorys
{
    public interface IFcdaFactory
    {
        IFcda GetFcda(IDai dai);
    }
}
