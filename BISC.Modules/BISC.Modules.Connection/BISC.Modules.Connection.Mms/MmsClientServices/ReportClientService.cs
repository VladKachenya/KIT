using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Connection.Infrastructure.Connection;
using BISC.Modules.Connection.MMS.MMS_ASN1_Model;
using BISC.Modules.Connection.MMS.org.bn.types;

namespace BISC.Modules.Connection.MMS.MmsClientServices
{
    public class ReportClientService : MmsClientServiceBase 
    {
        public ReportClientService(Iec61850State state) : base(state)
        {
        }
        public async Task<MMSpdu> WriteReportValueAsync(tBasicTypeEnum type, string ldFullPath, string rptId, string itemValueName, object valueToSave)
        {
            MMSpdu mymmspdu = new MMSpdu();
            _state.msMMSout = new MemoryStream();

            Confirmed_RequestPDU crreq = new Confirmed_RequestPDU();
            ConfirmedServiceRequest csrreq = new ConfirmedServiceRequest();
            Write_Request wreq = new Write_Request();

            List<VariableAccessSpecification.ListOfVariableSequenceType> vasl = new List<VariableAccessSpecification.ListOfVariableSequenceType>();
            List<Data> datl = new List<Data>();


            VariableAccessSpecification.ListOfVariableSequenceType vas = new VariableAccessSpecification.ListOfVariableSequenceType();
            Data dat = new Data();

            ObjectName on = new ObjectName();
            ObjectName.Domain_specificSequenceType dst = new ObjectName.Domain_specificSequenceType();
            dst.DomainID = new Identifier(ldFullPath);


            var fullItemPath = rptId + "$" + itemValueName;


            dst.ItemID = new Identifier(fullItemPath);
            on.selectDomain_specific(dst);

            vas.VariableSpecification = new VariableSpecification();
            vas.VariableSpecification.selectName(on);
            vasl.Add(vas);

            switch (type)
            {
                case tBasicTypeEnum.bit_string:
                    dat.selectBit_string(valueToSave as BitString);
                    break;
                case tBasicTypeEnum.VisString255:
                    dat.selectVisible_string(valueToSave as string);
                    break;
                case tBasicTypeEnum.BOOLEAN:
                    dat.selectBoolean((bool)valueToSave);
                    break;
                case tBasicTypeEnum.INT32U:
                    dat.selectUnsigned((long.Parse(valueToSave.ToString())));
                    break;
            }
            datl.Add(dat);

            _state.logger.LogDebug("SendWrite: Writing: " + dst.ItemID.Value);

            wreq.VariableAccessSpecification = new VariableAccessSpecification();
            wreq.VariableAccessSpecification.selectListOfVariable(vasl);
            wreq.ListOfData = datl;

            csrreq.selectWrite(wreq);

            crreq.InvokeID = new Unsigned32(_state.InvokeId++);

            crreq.Service = csrreq;

            mymmspdu.selectConfirmed_RequestPDU(crreq);

            encoder.encode<MMSpdu>(mymmspdu, _state.msMMSout);


            return await this.SendAsync(mymmspdu);


        }


    }
}