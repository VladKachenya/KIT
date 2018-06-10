using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.SclModelTemplates;
using BISC.Model.Iec61850Ed2.SclModelTemplates.Communication;
using BISC.Model.Iec61850Ed2.SclModelTemplates.Controls;
using BISC.Model.Iec61850Ed2.SclModelTemplates.DataSet;
using BISC.Model.Iec61850Ed2.SclModelTemplates.Substation;
using BISC.Model.Infrastructure.Common;

namespace BISC.Model.Iec61850Ed2.DataTypeTemplates.Base
{
    [XmlInclude(typeof (tSDO))]
    [XmlInclude(typeof (tSubNetwork))]
    [XmlInclude(typeof (tControl))]
    [XmlInclude(typeof (tControlWithIEDName))]
    [XmlInclude(typeof (tSampledValueControl))]
    [XmlInclude(typeof (tGSEControl))]
    [XmlInclude(typeof (tControlWithTriggerOpt))]
    [XmlInclude(typeof (tLogControl))]
    [XmlInclude(typeof (tReportControl))]
    [XmlInclude(typeof (tDataSet))]
    [XmlInclude(typeof (tAccessPoint))]
    [XmlInclude(typeof (tIED))]
    [XmlInclude(typeof (tLNodeContainer))]
    [XmlInclude(typeof (tConnectivityNode))]
    [XmlInclude(typeof (tPowerSystemResource))]
    [XmlInclude(typeof (tSubFunction))]
    [XmlInclude(typeof (tFunction))]
    [XmlInclude(typeof (tTapChanger))]
    [XmlInclude(typeof (tSubEquipment))]
    [XmlInclude(typeof (tEquipment))]
    [XmlInclude(typeof (tGeneralEquipment))]
    [XmlInclude(typeof (tPowerTransformer))]
    [XmlInclude(typeof (tAbstractConductingEquipment))]
    [XmlInclude(typeof (tTransformerWinding))]
    [XmlInclude(typeof (tConductingEquipment))]
    [XmlInclude(typeof (tEquipmentContainer))]
    [XmlInclude(typeof (tBay))]
    [XmlInclude(typeof (tVoltageLevel))]
    [XmlInclude(typeof (tSubstation))]
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tNaming : tBaseElement,INameableItem
    {
        private string nameAttr;
        
        [XmlAttribute(DataType = "normalizedString")]
        public string name
        {
            get { return this.nameAttr; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.nameAttr = value;
                }
            }
        }

        [XmlAttribute(DataType = "normalizedString")]
        public string desc { get; set; }

    }
}