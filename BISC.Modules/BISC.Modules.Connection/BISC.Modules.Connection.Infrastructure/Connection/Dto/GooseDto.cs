namespace BISC.Modules.Connection.Infrastructure.Connection
{
    public class GooseDto
    {
        public string Name { get; set; }
        public string DatSet { get; set; }
        public string GoId { get; set; }
   public bool FixedOffs { get; set; }

        public int ConfRev { get; set; }

        public string LdInst { get; set; }
        public string CbName { get; set; }
        public string MAC_Address { get; set; }

        public uint APPID { get; set; }
     

        public uint VLAN_ID { get; set; }
        public uint VLAN_PRIORITY { get; set; }
        public long MaxTime { get; set; }
        public long MinTime { get; set; }
    }
}