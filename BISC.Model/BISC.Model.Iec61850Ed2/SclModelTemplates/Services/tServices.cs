using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Services
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tServices
    {
        private tServiceYesNo dynAssociationField;
        private tServicesSettingGroups settingGroupsField;
        private tServiceYesNo getDirectoryField;
        private tServiceYesNo getDataObjectDefinitionField;
        private tServiceYesNo dataObjectDirectoryField;
        private tServiceYesNo getDataSetValueField;
        private tServiceYesNo setDataSetValueField;
        private tServiceYesNo dataSetDirectoryField;
        private tServiceWithMaxAndMaxAttributes confDataSetField;
        private tServiceWithMaxAndMaxAttributes dynDataSetField;
        private tServiceYesNo readWriteField;
        private tServiceYesNo timerActivatedControlField;
        private tServiceWithMax confReportControlField;
        private tServiceYesNo getCBValuesField;
        private tServiceWithMax confLogControlField;
        private tReportSettings reportSettingsField;
        private tLogSettings logSettingsField;
        private tGSESettings gSESettingsField;
        private tSMVSettings sMVSettingsField;
        private tServiceYesNo gSEDirField;
        private tServiceWithMax gooseField;
        private tServiceWithMax gsseField;
        private tServiceYesNo fileHandlingField;
        private tConfLNs confLNsField;

        [XmlElement]
        [Category("Services"), Description("Все сервисы для динамического построения ассоциаций")]
        public tServiceYesNo DynAssociation
        {
            get { return this.dynAssociationField; }
            set { this.dynAssociationField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("Сервисы группы настроек")]
        public tServicesSettingGroups SettingGroups
        {
            get { return this.settingGroupsField; }
            set { this.settingGroupsField = value; }
        }

        [XmlElement]
        [Category("Services"),
         Description("Сервис для чтения содержимого сервера, то есть каталогов LN и LD (всех LD, LN и данных DATA логических узлов)")]
        public tServiceYesNo GetDirectory
        {
            get { return this.getDirectoryField; }
            set { this.getDirectoryField = value; }
        }

        [XmlElement]
        [Category("Services"),
         Description("Сервис для поиска полного списка всех определений DA для справочных данных, " +
                     "которые видимы и доступны запрашивающему клиенту через ссылочный LN")]
        public tServiceYesNo GetDataObjectDefinition
        {
            get { return this.getDataObjectDefinitionField; }
            set { this.getDataObjectDefinitionField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("Сервис для получения данных DATA, определенных в LN")]
        public tServiceYesNo DataObjectDirectory
        {
            get { return this.dataObjectDirectoryField; }
            set { this.dataObjectDirectoryField = value; }
        }

        [XmlElement]
        [Category("Services"),
         Description("Сервис для поиска всех значений данных, к которым обращаются элементы набора данных")]
        public tServiceYesNo GetDataSetValue
        {
            get { return this.getDataSetValueField; }
            set { this.getDataSetValueField = value; }
        }

        [XmlElement]
        [Category("Services"),
         Description("Сервис для записи всех значений данных, к которым обращаются элементы набора данных")]
        public tServiceYesNo SetDataSetValue
        {
            get { return this.setDataSetValueField; }
            set { this.setDataSetValueField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("Сервис для поиска функционально связанных данных FCD/FCDA " +
                                           "всех элементов, запрашиваемых в наборе данных")]
        public tServiceYesNo DataSetDirectory
        {
            get { return this.dataSetDirectoryField; }
            set { this.dataSetDirectoryField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("Сервис конфигурирования новых DataSet до определенного максимума max")]
        public tServiceWithMaxAndMaxAttributes ConfDataSet
        {
            get { return this.confDataSetField; }
            set { this.confDataSetField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("Сервисы для динамического создания и удаления наборов данных")]
        public tServiceWithMaxAndMaxAttributes DynDataSet
        {
            get { return this.dynDataSetField; }
            set { this.dynDataSetField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("Возможность считывания и записи основных данных")]
        public tServiceYesNo ReadWrite
        {
            get { return this.readWriteField; }
            set { this.readWriteField = value; }
        }

        [XmlElement]
        [Category("Services"),
         Description("Указывает на поддержку сервисов управления активированным таймером")]
        public tServiceYesNo TimerActivatedControl
        {
            get { return this.timerActivatedControlField; }
            set { this.timerActivatedControlField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("Возможность статического (путем конфигурирования через язык SCL) " +
                                           "создания блоков управления генерацией отчетов")]
        public tServiceWithMax ConfReportControl
        {
            get { return this.confReportControlField; }
            set { this.confReportControlField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("Считывание значений блоков управления")]
        public tServiceYesNo GetCBValues
        {
            get { return this.getCBValuesField; }
            set { this.getCBValuesField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("Возможность статического (путем конфигурирования через язык SCL) " +
                                           "создания блоков управления журналом")]
        public tServiceWithMax ConfLogControl
        {
            get { return this.confLogControlField; }
            set { this.confLogControlField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("Атрибуты блока управления генерацией отчетов, " +
                                           "для которых возможна оперативная настройка с помощью сервисов " +
                                           "SetURCBValues соответственно SetBRCBValues")]
        public tReportSettings ReportSettings
        {
            get { return this.reportSettingsField; }
            set { this.reportSettingsField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("Атрибуты блока управления журналом, для которых возможна " +
                                           "оперативная настройка с помощью сервисов SetBRCBValues")]
        public tLogSettings LogSettings
        {
            get { return this.logSettingsField; }
            set { this.logSettingsField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("Атрибуты блока управления GSE-сообщениями, " +
                                           "для которых возможна оперативная настройка с помощью сервисов " +
                                           "SetGsCBValues соответственно SetGoCBValues")]
        public tGSESettings GSESettings
        {
            get { return this.gSESettingsField; }
            set { this.gSESettingsField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("Атрибуты блока управления SMV, " +
                                           "для которых возможна оперативная настройка с помощью сервисов " +
                                           "SetMSVCBValues соответственно SetUSVCBValues")]
        public tSMVSettings SMVSettings
        {
            get { return this.sMVSettingsField; }
            set { this.sMVSettingsField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("Сервисы каталога GSE-событий")]
        public tServiceYesNo GSEDir
        {
            get { return this.gSEDirField; }
            set { this.gSEDirField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("Данный элемент показывает, что IED-устройство может быть GOOSE-сервером и/или клиентом")]
        public tServiceWithMax GOOSE
        {
            get { return this.gooseField; }
            set { this.gooseField = value; }
        }

        [XmlElement]
        [Category("Services"),
         Description("Данный элемент показывает, что IED-устройство может быть GSSE-сервером бинарных данных и/или клиентом")]
        public tServiceWithMax GSSE
        {
            get { return this.gsseField; }
            set { this.gsseField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("Все сервисы по обработке файлов")]
        public tServiceYesNo FileHandling
        {
            get { return this.fileHandlingField; }
            set { this.fileHandlingField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("Описывает, что может быть сконфигурировано для LN, определенных в ICD-файле")]
        public tConfLNs ConfLNs
        {
            get { return this.confLNsField; }
            set { this.confLNsField = value; }
        }
    }
}