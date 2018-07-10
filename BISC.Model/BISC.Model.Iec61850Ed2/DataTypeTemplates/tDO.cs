using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;
using BISC.Model.Iec61850Ed2.DataTypeTemplates.Base;

namespace BISC.Model.Iec61850Ed2.DataTypeTemplates
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tDO : tUnNaming
    {
        private string nameField;
        private string typeField;
        private string accessControlField;

        
        [Required, ReadOnly(true)]
        [XmlAttribute(DataType = "Name")]
        [Category("DO"), Description("»м€ данных DATA")]
        public string name
        {
            get { return this.nameField; }
            set { this.nameField = value; }
        }

        [Required, ReadOnly(true)]
        [XmlAttribute(DataType = "normalizedString")]
        [Category("DO"), Description("“ип обращаетс€ к id определени€ DOType")]
        public string type
        {
            get { return this.typeField; }
            set { this.typeField = value; }
        }

        [XmlAttribute(DataType = "normalizedString")]
        [Category("DO"), Description("ќпределение управлени€ доступом дл€ данного DO")]
        public string accessControl
        {
            get { return this.accessControlField; }
            set { this.accessControlField = value; }
        }

        [XmlIgnore]
        [Category("DO"), Description("ѕри задании логической единицы (true) указывает на применение определени€ Transien")]
        public bool? transient { get; set; }



        [XmlAttribute("transient"),Browsable(false)]
        public string transientAsText
        {
            get
            {
                if (this.transient.HasValue)
                    return this.transient.Value.ToString();
                else
                    return null;
            }
            set
            {
                if (value != null)
                    this.transient = bool.Parse(value);
                else
                    this.transient = null;
            }
        }



    }
}