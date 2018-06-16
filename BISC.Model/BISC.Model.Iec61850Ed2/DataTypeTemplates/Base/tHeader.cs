using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;

namespace BISC.Model.Iec61850Ed2.DataTypeTemplates.Base
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tHeader
    {
        private tText textField;
        private string idField;
        private string versionField;
        private string revisionField;
        private string toolIDField;
        private tHeaderNameStructure nameStructureField;
        private bool logsetversion;

        public tHeader()
        {
            // By default not log any set version updated
            this.logsetversion = false;
            this.version = "1";
            this.revision = "0";
            this.id = "NewProject";
           // this.History = new List<tHitem>();
        }

        /// <summary>
        /// Устанавливаект флаг, позволяющий вести логирование обновлений версий и ревизий
        /// </summary>
        [XmlIgnore]
        public bool LogSetVersion
        {
            get { return this.logsetversion; }
            set { this.logsetversion = value; }
        }

        [XmlElement]
        [Category("Header"), Browsable(false)]
        public tText Text
        {
            get { return this.textField; }
            set { this.textField = value; }
        }

        [XmlArrayItem("Hitem", IsNullable = false)]
        [Category("Header"), Browsable(false)]
        public List<tHitem> History { get; set; }

        /// <summary>
        /// Строка, которая идентифицирует данный SCL-файл
        /// </summary>
        [Required, Category("Header"), Browsable(true)]
        [XmlAttribute(DataType = "normalizedString")]
        public string id
        {
            get { return this.idField; }
            set { this.idField = value; }
        }

        [XmlAttribute(DataType = "normalizedString")]
        [Category("Header"), Description("Версия файла конфигурации (SCL)"), Browsable(true)]
        public string version
        {
            get { return this.versionField; }
            set
            {
                this.versionField = value;
                if (this.logsetversion)
                {
                    tHitem item = new tHitem
                    {
                        version = this.version,
                        revision = this.revision,
                        what = "Version updated: new value = " + value,
                        who = "No one. Automatic",
                        why = "Log forsed version updated"
                    };
                    this.History.Add(item);
                }
            }
        }

        [XmlAttribute(DataType = "normalizedString")]
        [Category("Header"), Browsable(true), Description("Ревизия файла конфигурации (SCL)")]
        public string revision
        {
            get { return this.revisionField; }
            set
            {
                this.revisionField = value;
                if (this.logsetversion)
                {
                    tHitem item = new tHitem
                    {
                        version = this.version,
                        revision = this.revision,
                        what = "Revision updated: new value = " + value,
                        who = "No one. Automatic",
                        why = "Log forsed revision updated"
                    };
                    this.History.Add(item);
                }
            }
        }

        [XmlAttribute(DataType = "normalizedString")]
        [Category("Header"), Browsable(true)]
        public string toolID
        {
            get { return this.toolIDField; }
            set { this.toolIDField = value; }
        }

        [XmlAttribute]
        [Category("Header"), Browsable(false)]
        public tHeaderNameStructure nameStructure
        {
            get { return this.nameStructureField; }
            set { this.nameStructureField = value; }
        }
    }
}