using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Model.Iec61850Ed2;
using BISC.Modules.DataSets.Infrastructure.Keys;
using BISC.Modules.DataSets.Infrastructure.Model;

namespace BISC.Modules.DataSets.Model.Model
{
   public class Fcda:ModelElement,IFcda
    {
        public Fcda(string iecAddress, string doName, string daName, string fc):this()
        {
            if (doName != null)
            {
                this.DoName = doName;
            }
            if (daName != null)
            {
                this.DaName = daName;
            }
            tFCEnum fce;
            tFCEnum.TryParse(fc, true, out fce);
            this.Fc = fce.ToString();
            this.LdInst = iecAddress.Substring(0, iecAddress.IndexOf('/'));

            string lnstr = iecAddress.Substring(iecAddress.IndexOf('/') + 1,
                iecAddress.IndexOf('.') - iecAddress.IndexOf('/') - 1);
            uint i;
            foreach (string str in Enum.GetNames(typeof(tLNClassEnum)))
            {
                if (lnstr.Contains(str))
                {
                    string[] s = new string[1];
                    s[0] = str;
                    this.LnClass = str;
                    string[] substr = new string[2];
                    substr = lnstr.Split(s, 2, StringSplitOptions.None);
                    if (substr[0].Length > 0)
                        this.Prefix = substr[0];
                    if (substr[1].Length > 0)
                    {
                        if (uint.TryParse(substr[1], out i))
                            this.LnInst = i.ToString();
                        else
                            this.LnInst = "";
                    }
                    else this.LnInst = "";

                    if (this.Prefix == null)
                        this.Prefix = "";
                    return;
                }
            }
            if (uint.TryParse(lnstr.Substring(lnstr.IndexOf(lnstr.First(char.IsDigit))), out i))
            {
                this.LnInst = i.ToString();
                this.LnClass = lnstr.Substring(lnstr.Length - 5, 4);
                if (lnstr.Length > 5)
                {
                    this.Prefix = lnstr.Substring(0, lnstr.Length - 5);
                }
            }
            else
            {
                if (lnstr.Length > 4)
                {
                    this.LnClass = lnstr.Substring(lnstr.Length - 4, 4);
                    this.Prefix = lnstr.Substring(0, lnstr.Length - 4);
                }
                else
                {
                    this.LnClass = lnstr;
                }
            }

            if (this.Prefix == null)
                this.Prefix = "";
        }
        public Fcda()
        {
            ElementName = DatasetKeys.DatasetModelKeys.FcdaModelKey;
        }
        #region Implementation of IFcda

        public string LdInst { get; set; }
        public string Prefix { get; set; }
        public string LnClass { get; set; }
        public string LnInst { get; set; }
        public string DoName { get; set; }
        public string DaName { get; set; }
        public string Fc { get; set; }

        #endregion
    }
}
