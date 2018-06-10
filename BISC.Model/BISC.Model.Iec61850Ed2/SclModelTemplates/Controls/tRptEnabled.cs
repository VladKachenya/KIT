using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Global;
using BISC.Model.Iec61850Ed2.DataTypeTemplates.Base;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Controls
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tRptEnabled : tUnNaming, ICloneable
    {
        public tRptEnabled()
        {
            this.max = 1;
        }
        public tRptEnabled(uint max)
        {
            this.max = max;
            List<tClientLN> clientLns=new List<tClientLN>();
            for (int i = 1; i <= max; i++)
            {
                tClientLN clientLn=new tClientLN();
                clientLn.ldInst = "none";
                clientLn.lnClass = "IHMI";
                clientLn.lnInst = "1";
                clientLn.iedName = "Client"+i;
                clientLns.Add(clientLn);
            }
            ClientLN = clientLns.ToArray();

        }
        [Category("RptEnabled"), Browsable(false)]
        [XmlElement]
        public tClientLN[] ClientLN { get; set; }

        public bool ShouldSerializeClientLN()
        {
            return StaticSerializingDirectives.IsReportClientsShouldBeSerialized;
        }




        [XmlAttribute]
        [Category("RptEnabled"),
         Description("ќпредел€ет максимальное число блоков управлени€ генерацией отчетов данного типа, " +
                     "которые инстанцируютс€ во врем€ конфигурировани€ в LN")]
        public uint max { get; set; }

        public object Clone()
        {
            tRptEnabled newRptEnabled = new tRptEnabled { max = max, ClientLN = ClientLN };
            return newRptEnabled;
        }
    }
}