using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.SclModelTemplates;
using BISC.Model.Infrastructure.Common;

namespace BISC.Model.Iec61850Ed2.DataTypeTemplates.Base
{

    [XmlInclude(typeof (tHitem))]
    [XmlInclude(typeof (tPrivate))]
    [XmlInclude(typeof (tText))]
    [XmlInclude(typeof (tBaseElement))]
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tAnyContentFromOtherNamespace: IParentable
    {
        private XmlNode[] anyField;
        private XmlAttribute[] anyAttrField;
        private object _parent;

        [XmlText]
        [XmlAnyElement]
        [Browsable(false)]
        public XmlNode[] Any
        {
            get { return this.anyField; }
            set { this.anyField = value; }
        }

        [XmlAnyAttribute]
        [Browsable(false)]
        public XmlAttribute[] AnyAttr
        {
            get { return this.anyAttrField; }
            set { this.anyAttrField = value; }
        }

       
        [XmlIgnore, Browsable(false)]
        public tAnyContentFromOtherNamespace Self => this;

        [XmlIgnore, Browsable(false)]
        public object Parent {
            get { return _parent; }
            set { _parent = value; }
        }


        //public tIED GetIedParent()
        //{
        //    var parent = this.Parent;
        //    int counter = 0;
        //    while (!(parent is tIED))
        //    {
        //        if (!(parent is IParentable)) return null;
        //        parent = (parent as IParentable)?.Parent;
        //        counter++;
        //        if (counter > 100) return null;
        //    }
        //    return (parent as tIED);
        //}
    }
}