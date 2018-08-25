using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Connection.MMS.MMS_ASN1_Model;
using BISC.Modules.Connection.MMS.org.bn.types;

namespace BISC.Modules.Connection.MMS.MmsClientServices
{
    public class InitService:MmsClientServiceBase
    {
        public bool[] ServiceSupportOptions = new bool[96];
        private int MaxCalls = 10;
        private int ReceiveInitiate(Iec61850State iecs, Initiate_ResponsePDU initiate_ResponsePDU)
        {
            if (initiate_ResponsePDU != null)
            {
                iecs.logger.LogDebug("mymmspdu.Initiate_ResponsePDU exists!");
                int cing = initiate_ResponsePDU.NegotiatedMaxServOutstandingCalling.Value;
                int ced = initiate_ResponsePDU.NegotiatedMaxServOutstandingCalled.Value;
                iecs.logger.LogDebug(String.Format("mymmspdu.Initiate_ResponsePDU.NegotiatedMaxServOutstandingCalling: {0}, Called: {1}",
                    cing, ced));

                MaxCalls = cing < ced ? cing : ced;

                StringBuilder sb2 = new StringBuilder();
                int j = 0;
                foreach (byte b in initiate_ResponsePDU.InitResponseDetail.ServicesSupportedCalled.Value.Value)
                {
                    for (int i = 7; i >= 0; i--)
                    {
                        ServiceSupportOptions[j] = ((b >> i) & 1) == 1;
                        if (ServiceSupportOptions[j]) sb2.Append(Enum.GetName(typeof(ServiceSupportOptionsEnum), (ServiceSupportOptionsEnum)j) + ',');
                        j++;
                    }
                }
                iecs.logger.LogInfo("Services Supported: " + sb2.ToString());

                //if (ServiceSupportOptions[(int)ServiceSupportOptionsEnum.defineNamedVariableList])
                //{
                //    iecs.DataModel.ied.DefineNVL = true;
                //}
                //if (ServiceSupportOptions[(int)ServiceSupportOptionsEnum.identify])
                //{
                //    iecs.DataModel.ied.Identify = true;
                //}
            }
            else
            {
                iecs.logger.LogError("mms.ReceiveInitiate: not an Initiate_ResponsePDU");
                return -1;
            }
            return 0;
        }

        private async Task<bool> SendInitiateAsync(Iec61850State iecs)
        {
            MMSpdu mymmspdu = new MMSpdu();
            iecs.msMMSout = new MemoryStream();

            Initiate_RequestPDU ireq = new Initiate_RequestPDU();
            Initiate_RequestPDU.InitRequestDetailSequenceType idet = new Initiate_RequestPDU.InitRequestDetailSequenceType();

            idet.ProposedVersionNumber = new Integer16(1);
            byte[] ppc = { 0xf1, 0x00 };
            idet.ProposedParameterCBB = new ParameterSupportOptions(new BitString(ppc, 5));

            byte[] ssc = { 0xee, 0x1c, 0x00, 0x00, 0x04, 0x08, 0x00, 0x00, 0x79, 0xef, 0x18 };
            idet.ServicesSupportedCalling = new MMS_ASN1_Model.ServiceSupportOptions(new BitString(ssc, 3));

            ireq.InitRequestDetail = idet;

            ireq.LocalDetailCalling = new Integer32(65000);
            ireq.ProposedMaxServOutstandingCalling = new Integer16(10);
            ireq.ProposedMaxServOutstandingCalled = new Integer16(10);
            ireq.ProposedDataStructureNestingLevel = new Integer8(5);

            mymmspdu.selectInitiate_RequestPDU(ireq);

            encoder.encode<MMSpdu>(mymmspdu, iecs.msMMSout);

            if (iecs.msMMSout.Length == 0)
            {
                iecs.logger.LogError("mms.SendInitiate: Encoding Error!");
                return false;
            }

            MMSpdu received = await this.SendAsync(mymmspdu);

            if (received.Initiate_ResponsePDU != null)
            {
                ReceiveInitiate(iecs, received.Initiate_ResponsePDU);
            }

            return true;
        }




        public async Task<MMSpdu> SendIdentifyAsync()
        {
            MMSpdu mymmspdu = new MMSpdu();
            _state.msMMSout = new MemoryStream();

            Confirmed_RequestPDU crreq = new Confirmed_RequestPDU();
            ConfirmedServiceRequest csrreq = new ConfirmedServiceRequest();
            Identify_Request idreq = new Identify_Request();

            idreq.initWithDefaults();

            csrreq.selectIdentify(idreq);

            _state.InvokeId = 0;
            crreq.InvokeID = new Unsigned32(_state.InvokeId++);
            crreq.Service = csrreq;

            mymmspdu.selectConfirmed_RequestPDU(crreq);

            encoder.encode<MMSpdu>(mymmspdu, _state.msMMSout);

            if (_state.msMMSout.Length == 0)
            {
                _state.logger.LogError("mms.SendIdentify: Encoding Error!");
                return null;
            }

            return await this.SendAsync(mymmspdu);

        }

        public async Task<bool> TryOpenConnection(string ip)
        {
            var isoPar = new IsoConnectionParameters((IsoAcse.AcseAuthenticationParameter) null);
            isoPar.hostname = ip;

            _state.hostname = isoPar.hostname; // due to tcps inheritance
            _state.port = isoPar.port; // due to tcps inheritance
            _state.cp = isoPar;
            _state.logger = Logger.getLogger();

            try
            {
                await TcpRw.StartClientAsync(_state);
                await _state.iso.SendCOTPSessionInitAsync(_state);
            }
            catch (Exception e)
            {
                return false;
            }

            return await SendInitiateAsync(_state);
        }

        public InitService(Iec61850State state) : base(state)
        {
        }
    }
}