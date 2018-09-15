using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using BISC.Model.Global;
using BISC.Model.Iec61850Ed2.DataTypeTemplates.Base;
using BISC.Model.Infrastructure.Common;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.DataSet
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tDataSet : tNaming
    {
        private bool _isDynamic;


        public tDataSet()
        {
            this.FCDA = new List<tFCDA>();
            IsDynamic = false;
        }
        public tDataSet(string nameInit)
        {
            this.FCDA = new List<tFCDA>();
            name = nameInit;
        }
        [XmlElement("FCDA")]
        [Category("DataSet"), Browsable(false)]
        public List<tFCDA> FCDA { get; private set; }

        public void AddFCDA(tFCDA newfcda)
        {
            foreach (tFCDA fcda in FCDA)
            {
                if(fcda.ToString()==newfcda.ToString())return;
            }
            FCDA.Add(newfcda);
        }

        public object Clone()
        {
            tDataSet newDataSet=new tDataSet();
            newDataSet.Parent = this.Parent;
            foreach (tFCDA fcda in FCDA)
            {
                newDataSet.AddFCDA((tFCDA) fcda.Clone());
            }
            newDataSet.name = name;
            newDataSet.Text = Text;
            newDataSet.desc = desc;
            return newDataSet;
        }


        //public string GetIedParentName()
        //{
        //    var parent = this.Parent;
        //    while (!(parent is tIED))
        //    {
        //        if(!(parent is IParentable))return String.Empty;
        //        parent = (parent as IParentable)?.Parent;
        //    }
        //    return (parent as INameableItem)?.name;
        //}


   

        #region Implementation of IHavingChildCollection
        [XmlIgnore]
        public List<INameableItem> ChildNamableCollection
        {
            get { return FCDA.Cast<INameableItem>().ToList(); }
        }

        #endregion


        #region Implementation of IStaticDynamicItem

        public bool ShouldSerializeIsDynamic()
        {
            return StaticSerializingDirectives.IsStaticDynamicItemsDemarcationShouldBeSerialized;
        }


        [XmlAttribute]
        public bool IsDynamic
        {
            get { return _isDynamic; }
            set { _isDynamic = value; }
        }

        #endregion
    }
}