using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.DataTypeTemplates.Base;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tAccessPoint : tNaming
    {
        private static int index;
        private List<tLN> _ln;
        private tServer _server;


        public tAccessPoint()
        {
            this.router = false;
            this.clock = false;
            if (name == null)
            {
                name = "P" + (++index);
            }
            this.Server = new tServer();
            this.LN = new List<tLN>();
        }

        [XmlAttribute]
        [Category("AccessPoint"), Description("Определяет наличие у данного IED-устройства функции маршрутизатора")]
        public bool router { get; set; }

        [XmlAttribute]
        [Category("AccessPoint"),
         Description("Определяет данное IED-устройство как главные часы на данной шине")]
        public bool clock { get; set; }

        [XmlElement("Server")]
        [Category("AccessPoint"), Browsable(false)]
        public tServer Server
        {
            get { return _server; }
            set
            {
                _server = value;
            }
        }

        [XmlElement("LN")]
        [Category("AccessPoint"), Browsable(false)]
        public List<tLN> LN
        {
            get { return _ln; }
            set
            {
                _ln = value;
            }
        }

        


    }
}