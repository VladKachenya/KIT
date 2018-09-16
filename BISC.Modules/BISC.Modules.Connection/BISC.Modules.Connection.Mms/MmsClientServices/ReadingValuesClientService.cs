using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Connection.MMS.MMS_ASN1_Model;

namespace BISC.Modules.Connection.MMS.MmsClientServices
{
   public class ReadingValuesClientService:MmsClientServiceBase
    {
        public ReadingValuesClientService(Iec61850State state) : base(state)
        {
        }
        public async Task<MMSpdu> SendReadAsync(string ldFullPath, string lnName, string fcName, List<string> customItemPathParts = null)
        {
            MMSpdu mymmspdu = new MMSpdu();
            _state.msMMSout = new MemoryStream();

            Confirmed_RequestPDU crreq = new Confirmed_RequestPDU();
            ConfirmedServiceRequest csrreq = new ConfirmedServiceRequest();
            Read_Request rreq = new Read_Request();

            List<VariableAccessSpecification.ListOfVariableSequenceType> vasl = new List<VariableAccessSpecification.ListOfVariableSequenceType>();

            //foreach (NodeBase b in el.Data)
            //{
            //    VariableAccessSpecification.ListOfVariableSequenceType vas = new VariableAccessSpecification.ListOfVariableSequenceType();

            //    ObjectName on = new ObjectName();
            //    ObjectName.Domain_specificSequenceType dst = new ObjectName.Domain_specificSequenceType();

            //    vas.VariableSpecification = new VariableSpecification();
            //    vas.VariableSpecification.selectName(on);

            //    dst.DomainID = new Identifier(b.CommAddress.Domain);
            //    dst.ItemID = new Identifier(b.CommAddress.Variable);

            //  ;
            //    on.selectDomain_specific(dst);

            //    vasl.Add(vas);
            //}

            VariableAccessSpecification.ListOfVariableSequenceType vas = new VariableAccessSpecification.ListOfVariableSequenceType();

            ObjectName on = new ObjectName();
            ObjectName.Domain_specificSequenceType dst = new ObjectName.Domain_specificSequenceType();

            vas.VariableSpecification = new VariableSpecification();
            vas.VariableSpecification.selectName(on);

            dst.DomainID = new Identifier(ldFullPath);


            var fullItemPath = lnName + "$" + fcName;
            if (customItemPathParts != null && customItemPathParts.Count > 0)
            {
                foreach (var customItemPathPart in customItemPathParts)
                {
                    fullItemPath += "$" + customItemPathPart;
                }
            }

            dst.ItemID = new Identifier(fullItemPath);


            on.selectDomain_specific(dst);

            vasl.Add(vas);
            rreq.VariableAccessSpecification = new VariableAccessSpecification();
            rreq.VariableAccessSpecification.selectListOfVariable(vasl);
            rreq.SpecificationWithResult = true;

            csrreq.selectRead(rreq);

            crreq.InvokeID = new Unsigned32(_state.InvokeId++);

            crreq.Service = csrreq;

            mymmspdu.selectConfirmed_RequestPDU(crreq);

            encoder.encode<MMSpdu>(mymmspdu, _state.msMMSout);

            if (_state.msMMSout.Length == 0)
            {

                return null;
            }

            return await this.SendAsync(mymmspdu);
        }


    }
}
