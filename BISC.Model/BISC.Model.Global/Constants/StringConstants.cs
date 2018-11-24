using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Model.Global.Constants
{
    public static class StringConstants
    {
        public const string ReportControl_Name =
            "Название";


        public const string ReportControl_Dataset_Description =
            "Привязка к набору данных";

        public const string ReportControl_intgPd_Description =
            "Периодичность отправки отчетов в мс [0- 4294967294 мс]";

        public const string ReportControl_bufOvfl_Description =
            "Отправлять признак переполнения буфера";

        public const string ReportControl_configRef_Description =
            "Отправлять количество изменений DataSet";

        public const string ReportControl_dataRef_Description =
            "Отправлять ссылку на передаваемые данные";

        public const string ReportControl_isDataset_Description =
            "Отправлять имя DataSet";


        public const string ReportControl_entryId_Description =
            "Отправлять EntryID";

        public const string ReportControl_reasonCode_Description =
            "Отправлять причину включения в отчет";

        public const string ReportControl_seqNum_Description =
            "Отправлять последовательный номер отчета";

        public const string ReportControl_timeStamp_Description =
            "Отправлять метку времени";

        public const string ReportControl_bufTime_Description =
            "Время буферизации отчетов в мс [0-3600000 мс]";

        public const string ReportControl_isBuffered_Description =
            "Разрешить буферизацию отчетов";


        public const string ReportControl_configRev_Description =
            "Номер ревизии конфигурации";

        public const string ReportControl_GI_Description =
            "Метка общего опроса";

        public const string ReportControl_rptEnabledMax_Description =
            "Максимальное число клиентов";

        public const string ReportControl_rptId_Description =
            "Идентификатор отчета";

        public const string ReportControl_cyclic_Description =
            "Отправка отчета переодически (в цикле)";

        public const string ReportControl_dchg_Description =
            "Отправка отчета по изменению данных";

        public const string ReportControl_dupd_Description =
            "Отправка отчета по обновлению данных";

        public const string ReportControl_qchg_Description =
            "Отправка отчета по изменению метки качества";

        public const string ReportControl_gi_Description =
            "Отправка отчета по общему опросу (GI)";

        public static class GooseDescriptions
        {
            public const string GOOSEName =
                "Название";

            public const string GseType =
                "Тип GOOSE-сообщения";

            public const string GoID =
                "Идентификатор GOOSE-сообщения";

            public const string DataSet =
                "Привязка к набору данных";

            public const string MinTime =
                "Минимальное время между сообщениями в мс";

            public const string MaxTime =
                "Максимальное время между сообщениями в мс";

            public const string DstAddress =
                "Адресная информация";

            public const string Addr =
                "MAC-адрес широковещательной рассылки";

            public const string Priority =
                "Приоритет в виртуальной локальной сети (VLAN) [0-7], 0-самый низкий";

            public const string VID =
                "Признак принадлежности к виртуальной локальной сети (VLAN)  [0-4095] ";

            public const string APPID =
                "идентификатор широковещательной рассылки";

            public const string ConfRev =
                "Номер ревизии конфигурации";
        }
    }
}
