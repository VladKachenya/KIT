using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Model.Global
{
    public class StaticSerializingDirectives
    {
        public static bool IsValuesShouldBeSerialized { get; set; }

        public static bool IsReportClientsShouldBeSerialized { get; set; }


        public static bool IsStaticDynamicItemsDemarcationShouldBeSerialized { get; set; }
    }
}
