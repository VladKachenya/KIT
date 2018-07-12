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

        static public bool IsIpAddress(string Address)
        {
            return IpMatch.IsMatch(Address);
        }

    }
}
