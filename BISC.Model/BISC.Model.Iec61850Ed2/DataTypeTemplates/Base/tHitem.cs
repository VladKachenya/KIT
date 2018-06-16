using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;

namespace BISC.Model.Iec61850Ed2.DataTypeTemplates.Base
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tHitem : tAnyContentFromOtherNamespace
    {
        private string versionField;
        private string revisionField;
        private string whenField;
        private string whoField;
        private string whatField;
        private string whyField;

        public tHitem()
        {
            this.versionField = "0";
            this.revisionField = "0";
            this.whenField = DateTime.Now.ToString();
            this.whatField = "";
            this.whoField = "No one. Automatic";
        }

        [Required]
        [XmlAttribute(DataType = "normalizedString")]
        [Category("Hitem"), Description("The version of this history entry")]
        public string version
        {
            get { return this.versionField; }
            set { this.versionField = value; }
        }

        [Required]
        [XmlAttribute(DataType = "normalizedString")]
        [Category("Hitem"), Description("The revision of this history entry")]
        public string revision
        {
            get { return this.revisionField; }
            set { this.revisionField = value; }
        }

        [Required]
        [XmlAttribute(DataType = "normalizedString")]
        [Category("Hitem"), Description("Date when the version/revision was released")]
        public string when
        {
            get { return this.whenField; }
            set { this.whenField = value; }
        }

        [XmlAttribute(DataType = "normalizedString")]
        [Category("Hitem"), Description("Who made/approved this version/revision")]
        public string who
        {
            get { return this.whoField; }
            set { this.whoField = value; }
        }

        [XmlAttribute(DataType = "normalizedString")]
        [Category("Hitem"), Description("What has been changed since the last approval")]
        public string what
        {
            get { return this.whatField; }
            set { this.whatField = value; }
        }

        [XmlAttribute(DataType = "normalizedString")]
        [Category("Hitem"), Description("Why the change has happened")]
        public string why
        {
            get { return this.whyField; }
            set { this.whyField = value; }
        }
    }
}