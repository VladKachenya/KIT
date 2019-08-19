using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Factorys;
using BISC.Model.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Model.Global.Services
{
    public class ModelsComparingService : IModelsComparingService
    {
        IMismatchFactory _mismatchFactory;
        List<IMismatch> result = new List<IMismatch>();
        public ModelsComparingService(IMismatchFactory mismatchFactory)
        {
            _mismatchFactory = mismatchFactory; 
        }
        public List<IMismatch> CompareBranches(IModelElement branch1, IModelElement branch2)
        {
            result.Clear();
            if (branch1 == null || branch2 == null) return null;
            if (branch1.ModelElementCompareTo(branch2))
            {
                RecursiveComparer(branch1.ChildModelElements, branch2.ChildModelElements);
            }
            return new List<IMismatch>(result);
        }

        private bool RecursiveComparer(List<IModelElement> ChildOfbranch1, List<IModelElement> ChildOfbranch2)
        {
            // тут необходимо помимо сравнения количества элементов сравнить элементы между собой.
            if (!MissingCompare(ChildOfbranch1, ChildOfbranch2)) return false;
            if (!InequalityCompare(ChildOfbranch1, ChildOfbranch2)) return false;
            for (int i = 0; i < ChildOfbranch1.Count; i++)
            {
                if(!RecursiveComparer(ChildOfbranch1[i].ChildModelElements, ChildOfbranch2[i].ChildModelElements)) return false;
            }
            return true;
        }

        private bool InequalityCompare(List<IModelElement> ChildOfbranch1, List<IModelElement> ChildOfbranch2)
        {
            bool IsItCame = true;
            for (int i = 0; i < ChildOfbranch1.Count; i++)
            {
                if (!ChildOfbranch1[i].ModelElementCompareTo(ChildOfbranch2[i]))
                {
                    result.Add(_mismatchFactory.GetInequalityMismatch(ChildOfbranch1[i], ChildOfbranch2[i], 0));
                    IsItCame = false;
                }
            }
            return IsItCame;
        }
        private bool MissingCompare(List<IModelElement> ChildOfbranch1, List<IModelElement> ChildOfbranch2)
        {
            int countBranch1, countBranch2;
            countBranch1 = ChildOfbranch1.Count;
            countBranch2 = ChildOfbranch2.Count;
            if (countBranch1 == countBranch2)
            {
                return true;
            }
            else if (countBranch1 > countBranch2)
            {
                for(int i = (countBranch2); i < countBranch1; i++)
                    result.Add(_mismatchFactory.GetMissingMismatch(ChildOfbranch1[i]));
                return false;
            }
            else
            {
                for (int i = (countBranch1); i < countBranch2; i++)
                    result.Add(_mismatchFactory.GetMissingMismatch(ChildOfbranch2[i]));
                return false;
            }
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

    public class InequalityMismatch: Mismatch , IInequalityMismatch
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

    public class MissingMismatch : Mismatch, IMissingMismatch
    {
        public MissingMismatch(IModelElement missingItem)
            : base("MissingMismatch")
        {
            MissingItem = missingItem;
        }

        public IModelElement MissingItem { get; }

    }
}
