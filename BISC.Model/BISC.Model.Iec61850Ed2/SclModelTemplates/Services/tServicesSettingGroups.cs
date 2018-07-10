using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Services
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tServicesSettingGroups
    {
        private tServiceYesNo sGEditField;
        private tServiceYesNo confSGField;

        [XmlElement]
        [Category("SettingGroups"), Description("Возможность оперативного редактирования онлайн (сервисы МЭК 61850-7-2 SelectEditSG, ConfirmEditSGValues, SetSGValues)")]
        public tServiceYesNo SGEdit
        {
            get { return this.sGEditField; }
            set { this.sGEditField = value; }
        }

        [XmlElement]
        [Category("SettingGroups"), Description("Возможность конфигурирования ряда групп настроек средствами языка SCL")]
        public tServiceYesNo ConfSG
        {
            get { return this.confSGField; }
            set { this.confSGField = value; }
        }
    }
}