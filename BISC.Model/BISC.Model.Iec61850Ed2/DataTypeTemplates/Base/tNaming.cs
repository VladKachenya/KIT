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