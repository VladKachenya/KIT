using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Controls
{
    [XmlInclude(typeof (tLogControl))]
    [XmlInclude(typeof (tReportControl))]
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tControlWithTriggerOpt : tControl
    {      
        
        [XmlElement]
        [Category("ControlWithTriggerOpt"), Browsable(false),
        Description("Условия срабатывания блока")]
        public tTrgOps TrgOps { get; set; }

        [XmlAttribute]
        [Category("ControlWithTriggerOpt"),
         Description("Время периодической отправки отчетов")]
        public uint intgPd { get; set; }
    }
}