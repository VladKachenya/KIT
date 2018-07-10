using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.DataTypeTemplates.Base;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Controls
{
    [XmlInclude(typeof(tControlWithIEDName))]
    [XmlInclude(typeof(tSampledValueControl))]
    [XmlInclude(typeof(tGSEControl))]
    [XmlInclude(typeof(tControlWithTriggerOpt))]
    [XmlInclude(typeof(tLogControl))]
    [XmlInclude(typeof(tReportControl))]
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tControl : tNaming
    {
        string _datset;
        static ObservableCollection<string> _datSetCollection;

        [XmlAttribute(DataType = "normalizedString")]
        [Category("Control"), DisplayName("Data Set"),
        
         Description("Имя набора данных, направляемого блоком управления генерацией отчетов; " +
                     "в пределах ICD-файла datSet может быть только пустым")]
        [RefreshProperties(RefreshProperties.All)]

        public string datSet
        {
            get { return _datset; }
            set
            {
                _datset = value;
            }
        }

        [XmlIgnore, Browsable(false)]
        [RefreshProperties(RefreshProperties.All)]
        public static ObservableCollection<string> datSetCollection
        {
            get { return _datSetCollection; }
            set
            {
                _datSetCollection = value; 
             //   OnPropertyChanged();
            }
        }

    


       
    }
}