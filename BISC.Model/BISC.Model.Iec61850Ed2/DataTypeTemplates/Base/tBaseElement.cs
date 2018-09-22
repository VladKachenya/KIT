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
            if (propstr == "name")
            { }
            PropertyInfo prop = GetPropertyInfoForClass(propstr);
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
            PropertyInfo prop = GetPropertyInfoForClass(propStrWithoutDigit);
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
            PropertyInfo prop = GetPropertyInfoForClass(propstr);
            if (prop != null)
            {
                try
                {
                    prop.SetValue(this, val);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }


        private PropertyInfo GetPropertyInfoForClass(string propstr)
        {
            PropertyInfo prop = null;
            try
            {
                prop = GetType()?.GetProperty(propstr);
            }
            catch (Exception e)
            {
            }
            if (prop == null)
            {
                try
                {
                    var t = GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
                    prop = t.FirstOrDefault((a) => a.Name == propstr);
                }
                catch (Exception e)
                {

                }
            }
            return prop;
        }
        public object GetAttributeValByName(string propstr)
        {

            var prop = GetPropertyInfoForClass(propstr);
            if (prop != null)
            {
                return prop.GetValue(this);
            }
            return null;
        }
    }
}