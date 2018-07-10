using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Global;
using BISC.Model.Iec61850Ed2.Common;
using BISC.Model.Infrastructure.Controls;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Controls
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tGSEControl : tControlWithIEDName, IGseControl
    {
        private tGSEControlTypeEnum typeField;
        private string appIDField;
        private bool _isDynamic;


        public tGSEControl()
        {
            type=tGSEControlTypeEnum.GOOSE;
        }


        [XmlIgnore]
        [Category("GSEControl"), Description("Тип управления GSE"), DefaultValue(tGSEControlTypeEnum.GOOSE)]
        public tGSEControlTypeEnum type
        {
            get { return this.typeField; }
            set { this.typeField = value; }
        }

        [XmlAttribute(AttributeName = "type")]
        [Browsable(false)]
        public string typeString
        {
            get { return this.typeField.ToString(); }
            set { Enum.TryParse(value, out typeField); }
        }



        [Required]
        [XmlAttribute(DataType = "normalizedString")]
        [Category("GSEControl"),
         Description("Уникальная идентификация приложений в рамках системы, к которым принадлежит GOOSE-сообщение")]
        public string appID
        {
            get { return this.appIDField; }
            set { this.appIDField = value; }
        }
        [XmlAttribute]
        public bool fixedOffs { get; set; }

    

        #region Implementation of IStaticDynamicItem
        [XmlAttribute]
        public bool IsDynamic
        {
            get { return _isDynamic; }
            set { _isDynamic = value; }
        }

        public bool ShouldSerializeIsDynamic()
        {
            return StaticSerializingDirectives.IsStaticDynamicItemsDemarcationShouldBeSerialized;
        }

        #endregion
    }
}