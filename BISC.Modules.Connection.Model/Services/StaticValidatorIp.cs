using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Connection.Model.Services
{
    static public class StaticValidatorIp
    {
        static  private System.Text.RegularExpressions.Regex IpMatch =
        new System.Text.RegularExpressions.Regex(@"\b(?:\d{1,3}\.){3}\d{1,3}\b");

        static private System.Text.RegularExpressions.Regex IpMatchAdvanse =
       new System.Text.RegularExpressions.Regex(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");

        static public bool IsSimplifiedIpAddress(string Address)
        {
            return IpMatch.IsMatch(Address);
        }

        static public bool IsExactFormIpAddress(string Address)
        {
            return IpMatchAdvanse.IsMatch(Address);
        }

    }
}
