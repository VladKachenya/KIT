using System.IO;
using BISC.Modules.Connection.MMS.MMS_ASN1_Model;

namespace BISC.Modules.Connection.MMS
{
    public class Iec61850State: TcpState
    {
        /// <summary>
        /// Size of data buffer.
        /// </summary>
        public const int dataBufferSize = 2048;
        /// <summary>
        /// Index of receive buffer.
        /// </summary>
        public int dataBufferIndex = 0;
        /// <summary>
        /// TPKT Length
        /// </summary>
        public int TpktLen = 0;
        /// <summary>
        /// TPKT Datagram buffer.
        /// </summary>
        public byte[] dataBuffer = new byte[dataBufferSize];

        
        public int InvokeId { get; set; }

        /// <summary>
        /// TPKT Receive state
        /// </summary>
        public IsoTpktState kstate = IsoTpktState.TPKT_RECEIVE_START;
        /// <summary>
        /// OSI Receive state
        /// </summary>
        public IsoProtocolState ostate = IsoProtocolState.OSI_STATE_START;
        /// <summary>
        /// MMS File service state
        /// </summary>
        public FileTransferState fstate = FileTransferState.FILE_NO_ACTION;
 /*       /// <summary>
        /// OSI Protocol emulation
        /// </summary>
        public OsiEmul osi = new OsiEmul();*/
        /// <summary>
        /// ISO Protocol layers (new implementation)
        /// </summary>
        public IsoLayers iso;
        /// <summary>
        /// ISO Layers connection parameters
        /// </summary>
        public IsoConnectionParameters cp;

        /// <summary>
        /// MMS Protocol
        /// </summary>
        public Scsm_MMS mms;
        /// <summary>
        /// Input stream of MMS parsing
        /// </summary>
        public MemoryStream msMMS = new MemoryStream();
        /// <summary>
        /// Output stream of MMS coding
        /// </summary>
        public MemoryStream msMMSout;
  
        /// <summary>
        /// Memory for continuation of file directory requests
        /// </summary>
        public FileName continueAfterFileDirectory;
 

        public MMSCaptureDb CaptureDb;

        public Iec61850State()
        {
         
            CaptureDb = new MMSCaptureDb(this);
            iso = new IsoLayers(this);
            mms=new Scsm_MMS(this);
        }

        public void NextState()
        {
        }

   
    }

}
