using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tServerAuthentication
    {
        private bool noneField;
        private bool passwordField;
        private bool weakField;
        private bool strongField;
        private bool certificateField;

        public tServerAuthentication()
        {
            this.noneField = true;
            this.passwordField = false;
            this.weakField = false;
            this.strongField = false;
            this.certificateField = false;
        }

        [XmlAttribute]
        [Category("ServerAuthentication"), Description("Нет авторизации")]
        public bool none
        {
            get { return this.noneField; }
            set { this.noneField = value; }
        }

        [XmlAttribute]
        [Category("ServerAuthentication"), Description("Пароль авторизации")]
        public bool password
        {
            get { return this.passwordField; }
            set { this.passwordField = value; }
        }

        [XmlAttribute]
        [Category("ServerAuthentication"), Description("Слабый пароль для авторизации")]
        public bool weak
        {
            get { return this.weakField; }
            set { this.weakField = value; }
        }

        [XmlAttribute]
        [Category("ServerAuthentication"), Description("Сильный пароль для авторизации")]
        public bool strong
        {
            get { return this.strongField; }
            set { this.strongField = value; }
        }

        [XmlAttribute]
        [Category("ServerAuthentication"), Description("Сертификат для авторизации")]
        public bool certificate
        {
            get { return this.certificateField; }
            set { this.certificateField = value; }
        }
    }
}