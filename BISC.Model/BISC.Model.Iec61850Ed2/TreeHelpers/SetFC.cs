using System.Collections.Generic;
using System.ComponentModel;
using BISC.Model.Iec61850Ed2.SclModelTemplates;
using BISC.Model.Infrastructure.Common;

namespace BISC.Model.Iec61850Ed2.TreeHelpers
{
    public class SetFC : ILogicalNodeData 
    {
        public object Parent;
        public SetFC()
        {
            DAI = new List<tDAI>();
            SDI = new List<tSDI>();
        }
        [Browsable(false)]
        public List<tDAI> DAI { get; set; }
        [Browsable(false)]
        public List<tSDI> SDI { get; set; }
        public string name { get; set; }
        [Browsable(false)]
        public SetFC Self => this;



        #region Implementation of IHavingChildCollection

        public List<INameableItem> ChildNamableCollection
        {
            get
            {
                List<INameableItem> namables = new List<INameableItem>();
                DAI.ForEach((dai =>
                {
                    namables.Add(dai);
                }));
                SDI.ForEach((sdi =>
                {
                    namables.Add(sdi);
                }));
                return namables;
            }
        }
        #endregion
    }
}
