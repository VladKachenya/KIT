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