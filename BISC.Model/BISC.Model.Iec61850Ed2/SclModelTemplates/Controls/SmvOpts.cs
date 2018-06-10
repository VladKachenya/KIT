using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Controls
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class SmvOpts
    {
        private bool refreshTimeField;
        private bool sampleSynchronizedField;
        private bool sampleRateField;
        private bool securityField;
        private bool dataRefField;

        public SmvOpts()
        {
            this.refreshTimeField = false;
            this.sampleSynchronizedField = false;
            this.sampleRateField = false;
            this.securityField = false;
            this.dataRefField = false;
        }

        [XmlAttribute]
        [Category("SampledValueControlSmvOpts"),
        Description("”казывает, должно ли значение пол€ refreshTime включаетьс€ в сообщение SV")]
        public bool refreshTime
        {
            get { return this.refreshTimeField; }
            set { this.refreshTimeField = value; }
        }

        [XmlAttribute]
        [Category("SampledValueControlSmvOpts"), Description("”казывает, должно ли значение пол€ sampleSynchronized включаетьс€ в сообщение SV")]
        public bool sampleSynchronized
        {
            get { return this.sampleSynchronizedField; }
            set { this.sampleSynchronizedField = value; }
        }

        [XmlAttribute]
        [Category("SampledValueControlSmvOpts"), Description("”казывает, должно ли значение пол€ sampleRate включаетьс€ в сообщение SV")]
        public bool sampleRate
        {
            get { return this.sampleRateField; }
            set { this.sampleRateField = value; }
        }

        [XmlAttribute]
        [Category("SampledValueControlSmvOpts"), Description(" ")]
        public bool security
        {
            get { return this.securityField; }
            set { this.securityField = value; }
        }

        [XmlAttribute]
        [Category("SampledValueControlSmvOpts"),
         Description("”казывает, должна ли ссылка на набор данных включаетьс€ в сообщение SV")]
        public bool dataRef
        {
            get { return this.dataRefField; }
            set { this.dataRefField = value; }
        }
    }
}