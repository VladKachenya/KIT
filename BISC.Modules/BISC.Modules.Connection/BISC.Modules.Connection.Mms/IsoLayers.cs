using System.IO;
using System.Threading.Tasks;
using BISC.Modules.Connection.MMS.MMS_ASN1_Model;

namespace BISC.Modules.Connection.MMS
{
    public class IsoLayers
    {
        // ISO layers TPKT, COTP, SESS, PRES, ACSE coordinations
        Iec61850State iecs;

        /// <summary>
        /// OSI Protocol ACSE layer (new implementation)
        /// </summary>
        public IsoAcse isoAcse;

        /// <summary>
        /// OSI Protocol PRES layer (new implementation)
        /// </summary>
        public IsoPres isoPres;

        /// <summary>
        /// OSI Protocol SESS layer (new implementation)
        /// </summary>
        public IsoSess isoSess;

        /// <summary>
        /// OSI Protocol COTP layer (new implementation)
        /// </summary>
        public IsoCotp isoCotp;

        public MMSpdu ReceiveResult;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iec"></param>
        public IsoLayers(Iec61850State iec)
        {
            iecs = iec;
            Reset();
        }

        void Reset()
        {
            isoAcse = new IsoAcse(iecs);
            isoPres = new IsoPres(iecs);
            isoSess = new IsoSess(iecs);
            isoCotp = new IsoCotp(iecs.cp);
        }

        //public int SendCOTPSessionInit(Iec61850State iecs)
        //{
        //    // Make COTP init telegramm
        //    return isoCotp.SendInit(iecs);
        //}

        public async Task<int> SendCOTPSessionInitAsync(Iec61850State iecs)
        {
            // Make COTP init telegramm
            return await isoCotp.SendInitAsync(iecs);
        }
   

        async Task<MMSpdu> SendPresentationInitAsync(Iec61850State iecs)
        {
            // Make session & present. init telegramm
            byte[] b1 = new byte[1024];
            byte[] b2 = new byte[1024];
            //cp = new IsoConnectionParameters();
            bool dbg = false; // local debug enable var

            // MMS Initiate already encoded in iecs.msMMSout
            if (dbg) iecs.logger.LogDebugBuffer("Send MMS", iecs.msMMSout.GetBuffer(), 0, iecs.msMMSout.Length);

            int len = isoAcse.createAssociateRequestMessage(iecs.cp, b1, 0, iecs.msMMSout.GetBuffer(),
                (int) iecs.msMMSout.Length);
            if (dbg) iecs.logger.LogDebugBuffer("Send Acse", b1, 0, len);

            len = isoPres.createConnectPdu(iecs.cp, b2, b1, len);
            if (dbg) iecs.logger.LogDebugBuffer("Send Pres", b2, 0, len);

            len = isoSess.createConnectSpdu(iecs.cp, b1, b2, len);
            if (dbg) iecs.logger.LogDebugBuffer("Send Sess", b1, 0, len);

            b1.CopyTo(iecs.sendBuffer, IsoCotp.COTP_HDR_DT_SIZEOF + IsoTpkt.TPKT_SIZEOF);
            iecs.sendBytes = len;
            return await isoCotp.SendAsync(iecs);
        }




      async Task<MMSpdu> SendData(Iec61850State iecs)
        {
            //fastSend(iecs);
           return await layeredSend(iecs);
            
        }

        

        async Task<MMSpdu> layeredSend(Iec61850State iecs)
        {
            // Make COTP data telegramm directly
            // MMS already encoded in iecs.msMMSout
            iecs.sendBytes = (int) iecs.msMMSout.Length;

            int spos = isoSess.createDataSpdu(iecs.sendBuffer, IsoCotp.COTP_HDR_DT_SIZEOF + IsoTpkt.TPKT_SIZEOF);

            int dpos = isoPres.createUserData(iecs.sendBuffer, spos, iecs.sendBytes);

            iecs.msMMSout.Seek(0, SeekOrigin.Begin);
            iecs.msMMSout.Read(iecs.sendBuffer, dpos, iecs.sendBytes);

            iecs.sendBytes += dpos - IsoCotp.COTP_HDR_DT_SIZEOF - IsoTpkt.TPKT_SIZEOF;

           return await isoCotp.SendAsync(iecs);
        }

        
        public async Task<IsoCotp.CotpReceiveResult> Receive(Iec61850State iecs)
        {
            iecs.logger.LogDebug("Iso.Receive");
            IsoCotp.CotpReceiveResult res =await isoCotp.Receive(iecs);
            byte[] buffer = iecs.msMMS.GetBuffer();
            long len = iecs.msMMS.Length;
            iecs.logger.LogDebugBuffer("Rec buffer", buffer, 0, len);
            ReceiveResult = null;
            if (res == IsoCotp.CotpReceiveResult.DATA)
            {
                // Incoming data
                iecs.logger.LogDebug($"Calling isoSess.parseMessage with data len {iecs.msMMS.Length}");
                IsoSess.IsoSessionIndication sess = isoSess.parseMessage(buffer, (int) len);
                if (sess == IsoSess.IsoSessionIndication.SESSION_DATA)
                {
                    int dataPos = isoPres.parseUserData(buffer, (int) isoSess.UserDataIndex,
                        (int) (len - isoSess.UserDataIndex));
                    if (dataPos > 0)
                    {
                        // Adjust the stream position to the MMS message start
                        iecs.msMMS.Seek(dataPos, SeekOrigin.Begin);
                      ReceiveResult= await iecs.mms.ReceiveData(iecs);
                      
                    }
                    else
                    {
                        iecs.ostate = IsoProtocolState.OSI_STATE_SHUTDOWN;
                    }
                }
                else if (sess == IsoSess.IsoSessionIndication.SESSION_CONNECT)
                {
                    iecs.ostate = IsoProtocolState.OSI_STATE_SHUTDOWN;
                    int dataPosPres = isoPres.parseAcceptMessage(buffer, isoSess.UserDataIndex,
                        (int) (len - isoSess.UserDataIndex));
                    if (dataPosPres > 0)
                    {
                        IsoAcse.AcseIndication acseRes = isoAcse.parseMessage(buffer, isoPres.UserDataIndex,
                            (int) (len - isoPres.UserDataIndex));
                        if (acseRes == IsoAcse.AcseIndication.ACSE_ASSOCIATE)
                        {
                            iecs.msMMS.Seek(isoAcse.UserDataIndex, SeekOrigin.Begin);
                            iecs.logger.LogDebug("Read at " + isoAcse.UserDataIndex);
iecs.ostate = IsoProtocolState.OSI_CONNECTED;
                          ReceiveResult= await iecs.mms.ReceiveData(iecs);
                         
                        }
                    }
                }
                else
                {
                    iecs.ostate = IsoProtocolState.OSI_STATE_SHUTDOWN;
                }
                iecs.msMMS = new MemoryStream();
               
            }
            else if (res == IsoCotp.CotpReceiveResult.INIT)
            {
                iecs.ostate = IsoProtocolState.OSI_CONNECT_PRES;
             
            }
            else if (res == IsoCotp.CotpReceiveResult.ERROR)
            {
                iecs.ostate = IsoProtocolState.OSI_STATE_SHUTDOWN;
            }
            return res;
        }

        //public int Send(Iec61850State iecs)
        //{
        //    if (iecs.ostate == IsoProtocolState.OSI_CONNECT_PRES)
        //    {
        //        iecs.ostate = IsoProtocolState.OSI_CONNECT_PRES_WAIT;
        //        SendPresentationInit(iecs);
        //    }
        //    else
        //        SendData(iecs);
        //    return 0;
        //}

        public async Task<MMSpdu> SendAsync(Iec61850State iecs)
        {
            if (iecs.ostate == IsoProtocolState.OSI_CONNECT_PRES)
            {
                iecs.ostate = IsoProtocolState.OSI_CONNECT_PRES_WAIT;
               return await SendPresentationInitAsync(iecs);
            }
            else
              return await SendData(iecs);
     
        }


    }
}