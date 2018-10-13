using BISC.Model.Infrastructure.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Model.Infrastructure.Services
{
    public interface IModelsComparingServise
    {
        List<IMismatch> CompareBranches(IModelElement branch1, IModelElement branch2);
    }

    public interface IMismatch
    {
        string MismatchType { get;}
    }
}
