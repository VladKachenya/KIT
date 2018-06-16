using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.SclModelTemplates;
using BISC.Model.Iec61850Ed2.SclModelTemplates.Communication;
using BISC.Model.Iec61850Ed2.SclModelTemplates.Controls;
using BISC.Model.Iec61850Ed2.SclModelTemplates.Substation;

namespace BISC.Model.Iec61850Ed2.DataTypeTemplates.Base
{
    [XmlInclude(typeof (tDO))]
    [XmlInclude(typeof (tAbstractDataAttribute))]
    [XmlInclude(typeof (tBDA))]
    [XmlInclude(typeof (tDA))]
    [XmlInclude(typeof (tCommunication))]
    [XmlInclude(typeof (tSCLControl))]
    [XmlInclude(typeof (tSettingControl))]
    [XmlInclude(typeof (tInputs))]
    [XmlInclude(typeof (tDAI))]
    [XmlInclude(typeof (tSDI))]
    [XmlInclude(typeof (tDOI))]
    [XmlInclude(typeof (tControlBlock))]
    [XmlInclude(typeof (tSMV))]
    [XmlInclude(typeof (tGSE))]
    [XmlInclude(typeof (tConnectedAP))]
    [XmlInclude(typeof (tRptEnabled))]
    [XmlInclude(typeof (tAnyLN))]
    [XmlInclude(typeof (tLN0))]
    [XmlInclude(typeof (tLN))]
    [XmlInclude(typeof (tLDevice))]
    [XmlInclude(typeof (tServer))]
    [XmlInclude(typeof (tTerminal))]
    [XmlInclude(typeof (tLNode))]
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tUnNaming : tBaseElement
    {
        [XmlAttribute(DataType = "normalizedString")]
        [Category("Description"), Description("Описательный текст для атрибута")]
        public string desc { get; set; }
    }
}