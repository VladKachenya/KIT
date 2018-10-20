using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Connection.Infrastructure.Connection;
using BISC.Modules.Connection.MMS.MMS_ASN1_Model;
using DataSetDto = BISC.Modules.Connection.MMS.Dto.DataSetDto;

namespace BISC.Modules.Connection.MMS.MmsClientServices
{
   public class DataSetClientService : MmsClientServiceBase
    {
        public DataSetClientService(Iec61850State state) : base(state)
        {
        }
        public async Task<MMSpdu> SendGetNameListNamedVariableListAsync(string ldFullPath)
        {
            MMSpdu mymmspdu = new MMSpdu();
            _state.msMMSout = new MemoryStream();

            Confirmed_RequestPDU crreq = new Confirmed_RequestPDU();
            ConfirmedServiceRequest csrreq = new ConfirmedServiceRequest();
            GetNameList_Request nlreq = new GetNameList_Request();

            nlreq.ObjectClass = new ObjectClass();
            nlreq.ObjectClass.selectBasicObjectClass(ObjectClass.ObjectClass__basicObjectClass_namedVariableList);
            nlreq.ObjectScope = new GetNameList_Request.ObjectScopeChoiceType();
            nlreq.ObjectScope.selectDomainSpecific(new Identifier(ldFullPath));

            csrreq.selectGetNameList(nlreq);

            crreq.InvokeID = new Unsigned32(_state.InvokeId++);

            crreq.Service = csrreq;

            mymmspdu.selectConfirmed_RequestPDU(crreq);

            encoder.encode<MMSpdu>(mymmspdu, _state.msMMSout);


            return await this.SendAsync(mymmspdu);

        }


        public async Task<MMSpdu> GetDatasetInformationAsync(string ldFullPath, string lnName, string datsetName)
        {
            MMSpdu mymmspdu = new MMSpdu();
            _state.msMMSout = new MemoryStream();

            Confirmed_RequestPDU crreq = new Confirmed_RequestPDU();
            ConfirmedServiceRequest csrreq = new ConfirmedServiceRequest();
            GetNamedVariableListAttributes_Request nareq = new GetNamedVariableListAttributes_Request();
            ObjectName on = new ObjectName();
            ObjectName.Domain_specificSequenceType dst = new ObjectName.Domain_specificSequenceType();


            dst.DomainID = new Identifier(ldFullPath);
            dst.ItemID = new Identifier(lnName + "$" + datsetName);         // List name e.g. MMXU0$MX

            on.selectDomain_specific(dst);

            nareq.Value = on;

            csrreq.selectGetNamedVariableListAttributes(nareq);

            crreq.InvokeID = new Unsigned32(_state.InvokeId++);

            crreq.Service = csrreq;

            mymmspdu.selectConfirmed_RequestPDU(crreq);

            encoder.encode<MMSpdu>(mymmspdu, _state.msMMSout);

            if (_state.msMMSout.Length == 0)
            {
                _state.logger.LogError("mms.SendGetNamedVariableListAttributes: Encoding Error!");
                return null;
            }

            return await this.SendAsync(mymmspdu);

        }

        public async Task<MMSpdu> SendDefineNVLAsync(Dto.DataSetDto dataSetDto, List<FcdaDto> fcdaDtos)
        {
            MMSpdu mymmspdu = new MMSpdu();
            _state.msMMSout = new MemoryStream();

            Confirmed_RequestPDU crreq = new Confirmed_RequestPDU();
            ConfirmedServiceRequest csrreq = new ConfirmedServiceRequest();
            DefineNamedVariableList_Request nvlreq = new DefineNamedVariableList_Request();
            List<DefineNamedVariableList_Request.ListOfVariableSequenceType> dnvl = new List<DefineNamedVariableList_Request.ListOfVariableSequenceType>();
            DefineNamedVariableList_Request.ListOfVariableSequenceType var;

            foreach (FcdaDto fcdaDto in fcdaDtos)
            {
                var = new DefineNamedVariableList_Request.ListOfVariableSequenceType();
                var.VariableSpecification = new VariableSpecification();
                var.VariableSpecification.selectName(new ObjectName());
                var.VariableSpecification.Name.selectDomain_specific(new ObjectName.Domain_specificSequenceType());
                var.VariableSpecification.Name.Domain_specific.DomainID = new Identifier(fcdaDto.Ied + fcdaDto.Ld);

                string identifierStr = fcdaDto.Ln + "$" + fcdaDto.Fc;
                foreach (string fcdaDtoDaDoPathPart in fcdaDto.DaDoPathParts)
                {
                    identifierStr += "$" + fcdaDtoDaDoPathPart;
                }
                var.VariableSpecification.Name.Domain_specific.ItemID = new Identifier(identifierStr);
                dnvl.Add(var);
            }


            nvlreq.ListOfVariable = dnvl;
            nvlreq.VariableListName = new ObjectName();
            nvlreq.VariableListName.selectDomain_specific(new ObjectName.Domain_specificSequenceType());
            nvlreq.VariableListName.Domain_specific.DomainID = new Identifier(dataSetDto.Ied + dataSetDto.Ld);
            nvlreq.VariableListName.Domain_specific.ItemID = new Identifier(dataSetDto.Ln + "$" + dataSetDto.Name);

            csrreq.selectDefineNamedVariableList(nvlreq);

            crreq.InvokeID = new Unsigned32(_state.InvokeId++);

            crreq.Service = csrreq;

            mymmspdu.selectConfirmed_RequestPDU(crreq);

            encoder.encode<MMSpdu>(mymmspdu, _state.msMMSout);



            return await this.SendAsync(mymmspdu);

        }

        public async Task<MMSpdu> SendDeleteNVLAsync(DataSetDto dataSetDto)
        {
            MMSpdu mymmspdu = new MMSpdu();
            _state.msMMSout = new MemoryStream();

            Confirmed_RequestPDU crreq = new Confirmed_RequestPDU();
            ConfirmedServiceRequest csrreq = new ConfirmedServiceRequest();
            DeleteNamedVariableList_Request dnvlreq = new DeleteNamedVariableList_Request();
            List<ObjectName> onl = new List<ObjectName>();
            ObjectName on;

            // foreach (NodeBase d in el.Data)
            // {
            on = new ObjectName();
            on.selectDomain_specific(new ObjectName.Domain_specificSequenceType());
            on.Domain_specific.DomainID = new Identifier(dataSetDto.Ied + dataSetDto.Ld);
            on.Domain_specific.ItemID = new Identifier(dataSetDto.Ln + "$" + dataSetDto.Name);
            onl.Add(on);
            // }

            dnvlreq.ListOfVariableListName = onl;
            //dnvlreq.DomainName = new Identifier(el.Address.Domain);
            dnvlreq.ScopeOfDelete = DeleteNamedVariableList_Request.scopeOfDelete_specific;

            csrreq.selectDeleteNamedVariableList(dnvlreq);

            crreq.InvokeID = new Unsigned32(_state.InvokeId++);

            crreq.Service = csrreq;

            mymmspdu.selectConfirmed_RequestPDU(crreq);

            encoder.encode<MMSpdu>(mymmspdu, _state.msMMSout);



            return await this.SendAsync(mymmspdu);
        }



    }
}
