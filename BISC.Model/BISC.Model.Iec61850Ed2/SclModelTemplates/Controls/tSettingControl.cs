using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;
using BISC.Model.Iec61850Ed2.DataTypeTemplates.Base;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Controls
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tSettingControl : tUnNaming
    {
        private uint numOfSGsField;
        private uint actSGField;
        private uint LActTmField;
        private uint ResvTMSField;
        private uint CnfeditField;
        private bool EditSGField;
        [Required]
        [XmlAttribute]
        [Category("SettingControl"), Description("Число имеющихся групп настроек")]
        public uint numOfSGs
        {
            get { return this.numOfSGsField; }
            set { this.numOfSGsField = value; }
        }



        [XmlAttribute]
        [Category("SettingControl"),
         Description("Число групп настроек для вызова при загрузке конфигурации")]
        public uint actSG
        {
            get { return this.actSGField; }
            set { this.actSGField = value; }
        }

        [XmlAttribute]
        [Category("SettingControl"),
         Description("Время последнего переключения группы")]
        public uint LActTm
        {
            get { return this.LActTmField; }
            set { this.LActTmField = value; }
        }

        //[XmlAttribute]
        //[Category("SettingControl"),
        // Description("Время последнего переключения группы")]
        //public uint ResvTMS
        //{
        //    get { return this.ResvTMSField; }
        //    set { this.ResvTMSField = value; }
        //}


        //[XmlAttribute]
        //[Category("SettingControl"),
        // Description("Время последнего переключения группы")]
        //public bool Cnfedit
        //{
        //    get { return this.CnfeditField; }
        //    set { this.CnfeditField = value; }
        //}

        //[XmlAttribute]
        //[Category("SettingControl"),
        // Description("Время последнего переключения группы")]
        //public uint EditSG
        //{
        //    get { return this.EditSG; }
        //    set { this.EditSGField = value; }
        //}


    }
}