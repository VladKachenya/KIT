using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Gooses.Infrastructure.Model.FTP
{
    public class GooseFtpDto
    {
        public string Name { get; set; }
        public string GoId { get; set; }
        public string SelectedDataset { get; set; }
        public bool FixedOffs { get; set; }
        public uint MinTime { get; set; }
        public uint MaxTime { get; set; }
        public uint VlanPriority { get; set; }
        public uint VlanId { get; set; }
        public uint AppId { get; set; }
        public string MacAddress { get; set; }
        public string GseType { get; set; }

    }
}