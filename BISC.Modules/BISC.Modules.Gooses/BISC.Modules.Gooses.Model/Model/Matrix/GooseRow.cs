using BISC.Model.Global.Model;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BISC.Modules.Gooses.Infrastructure.Keys.GooseKeys;

namespace BISC.Modules.Gooses.Model.Model.Matrix
{
    public class GooseRow :ModelElement, IGooseRow
    {


        public GooseRow()
        {
            ElementName = GooseModelKeys.GooseRowKey;
        }
        public string Signature { get; set; }
        public string ReferencePath { get; set; }
        public string GooseRowType { get; set; }

        public string ValuesString
        {
            get
            {
                string s = String.Empty;

                foreach (var value in ValueList)
                {
                    s += value ? "1" : "0";
                }
                return s;
            }
            set
            {
                char[] chaArray = value.ToCharArray();
                ValueList=new List<bool>();
                foreach (var cha in chaArray)
                {
                    if (cha == '0')
                    {
                        ValueList.Add(false);
                    }
                    else
                    {
                        ValueList.Add(true);
                    }
                }
                
            }
        }

        public int NumberOfFcdaInDataSetOfGoose { get; set; }

        public List<bool> ValueList { get; set; }

        public override int CompareTo(object obj)
        {
            if (base.CompareTo(obj) == -1) return -1;
            if (!(obj is IGooseRow)) return -1;
            var element = obj as IGooseRow;
            if (element.Signature != Signature) return -1;
            if (element.ReferencePath != ReferencePath) return -1;
            if (element.GooseRowType != GooseRowType) return -1;
            if (element.ValuesString != ValuesString) return -1;
            if (element.NumberOfFcdaInDataSetOfGoose != NumberOfFcdaInDataSetOfGoose) return -1;
            return 1;
        }
    }
}
