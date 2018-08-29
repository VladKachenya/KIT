using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BISC.Modules.Connection.MMS.MMS_ASN1_Model;

namespace BISC.Modules.Connection.MMS.MmsClientServices
{
    public class InfoModelClientService:MmsClientServiceBase
    {
        public InfoModelClientService(Iec61850State state) : base(state)
        {
        }
        public async Task<MMSpdu> SendGetNameListDomainAsync()
        {
            MMSpdu mymmspdu = new MMSpdu();
            _state.msMMSout = new MemoryStream();

            Confirmed_RequestPDU crreq = new Confirmed_RequestPDU();
            ConfirmedServiceRequest csrreq = new ConfirmedServiceRequest();
            GetNameList_Request nlreq = new GetNameList_Request();

            nlreq.ObjectClass = new ObjectClass();
            nlreq.ObjectClass.selectBasicObjectClass(ObjectClass.ObjectClass__basicObjectClass_domain);
            nlreq.ObjectScope = new GetNameList_Request.ObjectScopeChoiceType();
            nlreq.ObjectScope.selectVmdSpecific();

            csrreq.selectGetNameList(nlreq);

            crreq.InvokeID = new Unsigned32(_state.InvokeId++);

            crreq.Service = csrreq;

            mymmspdu.selectConfirmed_RequestPDU(crreq);

            encoder.encode<MMSpdu>(mymmspdu, _state.msMMSout);

            if (_state.msMMSout.Length == 0)
            {
                _state.logger.LogError("mms.SendGetNameListDomain: Encoding Error!");
                return null;
            }
            return await this.SendAsync(mymmspdu);

        }


        public async Task<MMSpdu> SendGetNameListVariablesAsync(string ldPath, string lastIdentifier)
        {
            MMSpdu mymmspdu = new MMSpdu();
            _state.msMMSout = new MemoryStream();

            Confirmed_RequestPDU crreq = new Confirmed_RequestPDU();
            ConfirmedServiceRequest csrreq = new ConfirmedServiceRequest();
            GetNameList_Request nlreq = new GetNameList_Request();

            nlreq.ObjectClass = new ObjectClass();
            nlreq.ObjectClass.selectBasicObjectClass(ObjectClass.ObjectClass__basicObjectClass_namedVariable);
            nlreq.ObjectScope = new GetNameList_Request.ObjectScopeChoiceType();
            nlreq.ObjectScope.selectDomainSpecific(new Identifier(ldPath));
            if (lastIdentifier == null)
            {
                nlreq.ContinueAfter = null;
            }
            else
            {
                nlreq.ContinueAfter = new Identifier(lastIdentifier);
            }


            csrreq.selectGetNameList(nlreq);

            crreq.InvokeID = new Unsigned32(_state.InvokeId++);

            crreq.Service = csrreq;

            mymmspdu.selectConfirmed_RequestPDU(crreq);

            encoder.encode<MMSpdu>(mymmspdu, _state.msMMSout);

            if (_state.msMMSout.Length == 0)
            {
                _state.logger.LogError("mms.SendGetNameListVariables: Encoding Error!");
                return null;
            }

            return await this.SendAsync(mymmspdu);
        }


        public async Task<MMSpdu> SendGetVariableAccessAttributesAsync(string ldName, string lnName, CancellationToken? cancellationToken=null)
        {
            MMSpdu mymmspdu = new MMSpdu();
            _state.msMMSout = new MemoryStream();

            Confirmed_RequestPDU crreq = new Confirmed_RequestPDU();
            ConfirmedServiceRequest csrreq = new ConfirmedServiceRequest();
            GetVariableAccessAttributes_Request vareq = new GetVariableAccessAttributes_Request();
            ObjectName on = new ObjectName();
            ObjectName.Domain_specificSequenceType dst = new ObjectName.Domain_specificSequenceType();

            dst.DomainID = new Identifier(ldName);
            dst.ItemID = new Identifier(lnName);         // LN name e.g. MMXU0

            _state.logger.LogDebug("SendGetVariableAccessAttributes: Get Attr for: " + dst.ItemID.Value);
            on.selectDomain_specific(dst);

            vareq.selectName(on);

            csrreq.selectGetVariableAccessAttributes(vareq);

            crreq.InvokeID = new Unsigned32(_state.InvokeId++);

            crreq.Service = csrreq;

            mymmspdu.selectConfirmed_RequestPDU(crreq);

            encoder.encode<MMSpdu>(mymmspdu, _state.msMMSout);

            MMSpdu mmSpdu = await this.SendAsync(mymmspdu);
            if (mmSpdu == null)
            {
                cancellationToken?.ThrowIfCancellationRequested();
                await Task.Delay(100);
                mmSpdu = await this.SendAsync(mymmspdu);
            }
            if (mmSpdu == null)
            {
                cancellationToken?.ThrowIfCancellationRequested();
                await Task.Delay(100);
                mmSpdu = await this.SendAsync(mymmspdu);
            }
            if (mmSpdu == null)
            {
                cancellationToken?.ThrowIfCancellationRequested();
                await Task.Delay(100);
                mmSpdu = await this.SendAsync(mymmspdu);
            }
            if (mmSpdu == null)
            {
                cancellationToken?.ThrowIfCancellationRequested();
                await Task.Delay(1000);
                mmSpdu = await this.SendAsync(mymmspdu);
            }
            if (mmSpdu == null)
            {
                cancellationToken?.ThrowIfCancellationRequested();
                await Task.Delay(1000);
                mmSpdu = await this.SendAsync(mymmspdu);
            }
            if (mmSpdu == null)
            {
                cancellationToken?.ThrowIfCancellationRequested();
                await Task.Delay(2000);
                mmSpdu = await this.SendAsync(mymmspdu);
            }
            return mmSpdu;

        }

    }
}