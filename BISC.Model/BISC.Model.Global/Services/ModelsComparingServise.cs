using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Model.Global.Services
{
    public class ModelsComparingServise : IModelsComparingServise
    {
        public List<IMismatch> CompareBranches(IModelElement branch1, IModelElement branch2)
        {
            return new List<IMismatch>();
        }
    }

    public abstract class Mismatch : IMismatch
    {
        public Mismatch(string mismatchType)
        {
            MismatchType = mismatchType;
        }
        public string MismatchType { get;}
    }

    public class InequalityMismatch: Mismatch
    {
        public InequalityMismatch(IModelElement item1, IModelElement item2, int childPosition)
            : base("InequalityMismatch")
        {
            Item1 = item1;
            Item2 = item2;
            ChildPosition = childPosition;
        }

        public int ChildPosition { get; }
        public IModelElement Item1 { get; }
        public IModelElement Item2 { get; }

    }

    public class MissingMismatch : Mismatch
    {
        public MissingMismatch(IModelElement missingItem)
            : base("MissingMismatch")
        {
            MissingItem = missingItem;
        }

        public IModelElement MissingItem { get; }

    }
}
