using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.SclModelTemplates;
using BISC.Model.Iec61850Ed2.SclModelTemplates.Communication;
using BISC.Model.Iec61850Ed2.SclModelTemplates.Controls;
using BISC.Model.Iec61850Ed2.SclModelTemplates.DataSet;
using BISC.Model.Iec61850Ed2.SclModelTemplates.Substation;

namespace BISC.Model.Iec61850Ed2.DataTypeTemplates.Base
{
    [XmlInclude(typeof(tIDNaming))]
    [XmlInclude(typeof(tEnumType))]
    [XmlInclude(typeof(tDAType))]
    [XmlInclude(typeof(tDOType))]
    [XmlInclude(typeof(tLNodeType))]
    [XmlInclude(typeof(tNaming))]
    [XmlInclude(typeof(tSDO))]
    [XmlInclude(typeof(tSubNetwork))]
    [XmlInclude(typeof(tControl))]
    [XmlInclude(typeof(tControlWithIEDName))]
    [XmlInclude(typeof(tSampledValueControl))]
    [XmlInclude(typeof(tGSEControl))]
    [XmlInclude(typeof(tControlWithTriggerOpt))]
    [XmlInclude(typeof(tLogControl))]
    [XmlInclude(typeof(tReportControl))]
    [XmlInclude(typeof(tDataSet))]
    [XmlInclude(typeof(tAccessPoint))]
    [XmlInclude(typeof(tIED))]
    [XmlInclude(typeof(tLNodeContainer))]
    [XmlInclude(typeof(tConnectivityNode))]
    [XmlInclude(typeof(tPowerSystemResource))]
    [XmlInclude(typeof(tSubFunction))]
    [XmlInclude(typeof(tFunction))]
    [XmlInclude(typeof(tTapChanger))]
    [XmlInclude(typeof(tSubEquipment))]
    [XmlInclude(typeof(tEquipment))]
    [XmlInclude(typeof(tGeneralEquipment))]
    [XmlInclude(typeof(tPowerTransformer))]
    [XmlInclude(typeof(tAbstractConductingEquipment))]
    [XmlInclude(typeof(tTransformerWinding))]
    [XmlInclude(typeof(tConductingEquipment))]
    [XmlInclude(typeof(tEquipmentContainer))]
    [XmlInclude(typeof(tBay))]
    [XmlInclude(typeof(tVoltageLevel))]
    [XmlInclude(typeof(tSubstation))]
    [XmlInclude(typeof(tUnNaming))]
    [XmlInclude(typeof(tDO))]
    [XmlInclude(typeof(tAbstractDataAttribute))]
    [XmlInclude(typeof(tBDA))]
    [XmlInclude(typeof(tDA))]
    [XmlInclude(typeof(tCommunication))]
    [XmlInclude(typeof(tSCLControl))]
    [XmlInclude(typeof(tSettingControl))]
    [XmlInclude(typeof(tInputs))]
    [XmlInclude(typeof(tDAI))]
    [XmlInclude(typeof(tSDI))]
    [XmlInclude(typeof(tDOI))]
    [XmlInclude(typeof(tControlBlock))]
    [XmlInclude(typeof(tSMV))]
    [XmlInclude(typeof(tGSE))]
    [XmlInclude(typeof(tConnectedAP))]
    [XmlInclude(typeof(tRptEnabled))]
    [XmlInclude(typeof(tAnyLN))]
    [XmlInclude(typeof(tLN0))]
    [XmlInclude(typeof(tLN))]
    [XmlInclude(typeof(tLDevice))]
    [XmlInclude(typeof(tServer))]
    [XmlInclude(typeof(tTerminal))]
    [XmlInclude(typeof(tLNode))]
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tBaseElement : tAnyContentFromOtherNamespace
    {
        //Name spaces
        [XmlNamespaceDeclarations] public XmlSerializerNamespaces xmlns;

        [XmlElement]
        [Category("BaseElement"), Browsable(false)]
        public tText Text { get; set; }

        [XmlElement]
        [Category("BaseElement"), Browsable(false)]
        public tPrivate[] Private { get; set; }

        public Type GetAttributeByName(string propstr)
        {
            PropertyInfo prop = GetType().GetProperty(propstr);
            if (prop != null)
            {
                return prop.PropertyType;
            }
            else
            {
                if (char.IsDigit(propstr.Last()))
                {
                    return GetTypeWithDigit(propstr);
                }
                else
                {
                    return null;
                }
            }
            
        }


        private Type GetTypeWithDigit(string propstr)
        {
            var propStrWithoutDigit = propstr.Remove(propstr.Length - 1);
            PropertyInfo prop = GetType().GetProperty(propStrWithoutDigit);
            if (prop != null)
            {
                return prop.PropertyType;
            }
            else
            {
                if (char.IsDigit(propStrWithoutDigit.Last()))
                {
                    propStrWithoutDigit = propstr.Remove(propStrWithoutDigit.Length - 1);
                    prop = GetType().GetProperty(propStrWithoutDigit);
                    if (prop != null)
                    {
                        return prop.PropertyType;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public List<Type> GetAllPropertyTypes()
        {
            return GetType().GetProperties().Select((info => info.PropertyType)).ToList();
        }

        public void SetAttributeByName(string propstr, object val)
        {
            PropertyInfo prop = GetType().GetProperty(propstr);
            if (prop != null)
            {
                prop.SetValue(this, val);
            }
        }
        public object GetAttributeValByName(string propstr)
        {

            PropertyInfo prop = GetType().GetProperty(propstr);
            if (prop != null)
            {
                return prop.GetValue(this);
            }
            return null;
        }
    }
}