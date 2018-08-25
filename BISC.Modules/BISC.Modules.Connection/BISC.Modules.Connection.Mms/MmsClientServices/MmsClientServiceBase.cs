using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Connection.MMS.MMS_ASN1_Model;
using BISC.Modules.Connection.MMS.org.bn;

namespace BISC.Modules.Connection.MMS.MmsClientServices
{
    public class MmsClientServiceBase
    {
        protected readonly Iec61850State _state;


        public MmsClientServiceBase(Iec61850State state)
        {
            _state = state;
        }

        protected IDecoder decoder = CoderFactory.getInstance().newDecoder("BER");
        protected IEncoder encoder = CoderFactory.getInstance().newEncoder("BER");

        protected async Task<MMSpdu> SendAsync(MMSpdu pdu)
        {
            if (_state.CaptureDb.CaptureActive)
            {
                MMSCapture cap;
                _state.msMMSout.Seek(0, SeekOrigin.Begin);
                _state.msMMSout.Read(_state.sendBuffer, 0, (int)_state.msMMSout.Length);
                cap = new MMSCapture(_state.sendBuffer, 0, _state.msMMSout.Length, MMSCapture.CaptureDirection.Out);
                cap.MMSPdu = pdu;
                _state.CaptureDb.AddPacket(cap);
            }

            return await _state.iso.SendAsync(_state);
        }
    }
}