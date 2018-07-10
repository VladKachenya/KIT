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
        [Category("Services"), Description("��� ������� ��� ������������� ���������� ����������")]
        public tServiceYesNo DynAssociation
        {
            get { return this.dynAssociationField; }
            set { this.dynAssociationField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("������� ������ ��������")]
        public tServicesSettingGroups SettingGroups
        {
            get { return this.settingGroupsField; }
            set { this.settingGroupsField = value; }
        }

        [XmlElement]
        [Category("Services"),
         Description("������ ��� ������ ����������� �������, �� ���� ��������� LN � LD (���� LD, LN � ������ DATA ���������� �����)")]
        public tServiceYesNo GetDirectory
        {
            get { return this.getDirectoryField; }
            set { this.getDirectoryField = value; }
        }

        [XmlElement]
        [Category("Services"),
         Description("������ ��� ������ ������� ������ ���� ����������� DA ��� ���������� ������, " +
                     "������� ������ � �������� �������������� ������� ����� ��������� LN")]
        public tServiceYesNo GetDataObjectDefinition
        {
            get { return this.getDataObjectDefinitionField; }
            set { this.getDataObjectDefinitionField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("������ ��� ��������� ������ DATA, ������������ � LN")]
        public tServiceYesNo DataObjectDirectory
        {
            get { return this.dataObjectDirectoryField; }
            set { this.dataObjectDirectoryField = value; }
        }

        [XmlElement]
        [Category("Services"),
         Description("������ ��� ������ ���� �������� ������, � ������� ���������� �������� ������ ������")]
        public tServiceYesNo GetDataSetValue
        {
            get { return this.getDataSetValueField; }
            set { this.getDataSetValueField = value; }
        }

        [XmlElement]
        [Category("Services"),
         Description("������ ��� ������ ���� �������� ������, � ������� ���������� �������� ������ ������")]
        public tServiceYesNo SetDataSetValue
        {
            get { return this.setDataSetValueField; }
            set { this.setDataSetValueField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("������ ��� ������ ������������� ��������� ������ FCD/FCDA " +
                                           "���� ���������, ������������� � ������ ������")]
        public tServiceYesNo DataSetDirectory
        {
            get { return this.dataSetDirectoryField; }
            set { this.dataSetDirectoryField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("������ ���������������� ����� DataSet �� ������������� ��������� max")]
        public tServiceWithMaxAndMaxAttributes ConfDataSet
        {
            get { return this.confDataSetField; }
            set { this.confDataSetField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("������� ��� ������������� �������� � �������� ������� ������")]
        public tServiceWithMaxAndMaxAttributes DynDataSet
        {
            get { return this.dynDataSetField; }
            set { this.dynDataSetField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("����������� ���������� � ������ �������� ������")]
        public tServiceYesNo ReadWrite
        {
            get { return this.readWriteField; }
            set { this.readWriteField = value; }
        }

        [XmlElement]
        [Category("Services"),
         Description("��������� �� ��������� �������� ���������� �������������� ��������")]
        public tServiceYesNo TimerActivatedControl
        {
            get { return this.timerActivatedControlField; }
            set { this.timerActivatedControlField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("����������� ������������ (����� ���������������� ����� ���� SCL) " +
                                           "�������� ������ ���������� ���������� �������")]
        public tServiceWithMax ConfReportControl
        {
            get { return this.confReportControlField; }
            set { this.confReportControlField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("���������� �������� ������ ����������")]
        public tServiceYesNo GetCBValues
        {
            get { return this.getCBValuesField; }
            set { this.getCBValuesField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("����������� ������������ (����� ���������������� ����� ���� SCL) " +
                                           "�������� ������ ���������� ��������")]
        public tServiceWithMax ConfLogControl
        {
            get { return this.confLogControlField; }
            set { this.confLogControlField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("�������� ����� ���������� ���������� �������, " +
                                           "��� ������� �������� ����������� ��������� � ������� �������� " +
                                           "SetURCBValues �������������� SetBRCBValues")]
        public tReportSettings ReportSettings
        {
            get { return this.reportSettingsField; }
            set { this.reportSettingsField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("�������� ����� ���������� ��������, ��� ������� �������� " +
                                           "����������� ��������� � ������� �������� SetBRCBValues")]
        public tLogSettings LogSettings
        {
            get { return this.logSettingsField; }
            set { this.logSettingsField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("�������� ����� ���������� GSE-�����������, " +
                                           "��� ������� �������� ����������� ��������� � ������� �������� " +
                                           "SetGsCBValues �������������� SetGoCBValues")]
        public tGSESettings GSESettings
        {
            get { return this.gSESettingsField; }
            set { this.gSESettingsField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("�������� ����� ���������� SMV, " +
                                           "��� ������� �������� ����������� ��������� � ������� �������� " +
                                           "SetMSVCBValues �������������� SetUSVCBValues")]
        public tSMVSettings SMVSettings
        {
            get { return this.sMVSettingsField; }
            set { this.sMVSettingsField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("������� �������� GSE-�������")]
        public tServiceYesNo GSEDir
        {
            get { return this.gSEDirField; }
            set { this.gSEDirField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("������ ������� ����������, ��� IED-���������� ����� ���� GOOSE-�������� �/��� ��������")]
        public tServiceWithMax GOOSE
        {
            get { return this.gooseField; }
            set { this.gooseField = value; }
        }

        [XmlElement]
        [Category("Services"),
         Description("������ ������� ����������, ��� IED-���������� ����� ���� GSSE-�������� �������� ������ �/��� ��������")]
        public tServiceWithMax GSSE
        {
            get { return this.gsseField; }
            set { this.gsseField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("��� ������� �� ��������� ������")]
        public tServiceYesNo FileHandling
        {
            get { return this.fileHandlingField; }
            set { this.fileHandlingField = value; }
        }

        [XmlElement]
        [Category("Services"), Description("���������, ��� ����� ���� ���������������� ��� LN, ������������ � ICD-�����")]
        public tConfLNs ConfLNs
        {
            get { return this.confLNsField; }
            set { this.confLNsField = value; }
        }
    }
}