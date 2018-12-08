/*
 *  Copyright (C) 2013 Pavel Charvat
 * 
 *  This file is part of IEDExplorer.
 *
 *  IEDExplorer is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  IEDExplorer is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with IEDExplorer.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BISC.Modules.Connection.MMS.MMS_ASN1_Model;

namespace BISC.Modules.Connection.MMS
{
    /// <summary>
    /// TPKT Header parsing according to RFC1006 / OSI (COTP) mapping to TCP/IP
    /// </summary>
    class IsoTpkt
    {
        public const byte TPKT_START = 0x03;
        public const byte TPKT_RES = 0x00;
        public const int TPKT_MAXLEN = 2048;

        public const int TPKT_IDX_START = 0;
        public const int TPKT_IDX_RES = 1;
        public const int TPKT_IDX_LEN = 2;

        public const int TPKT_SIZEOF = 4;

        /// <summary>
        /// Parsing of data from socket into TPKT datagrams
        /// </summary>
        /// <param name="iecs">Global protocol state structure</param>
        public static async Task<MMSpdu> GetAnswer(TcpState tcps)
        {

            if (tcps.receivedBytes == null)
            {
                tcps.receivedBytes=new List<byte>();
            }

            tcps.receivedBytes.AddRange(await TcpRw.ReadBytesFromSocket(tcps));

            Iec61850State iecs = (Iec61850State)tcps;

            for (int i = 0; i < tcps.receivedBytes.Count; i++)
            {
                if (iecs.kstate == IsoTpktState.TPKT_RECEIVE_ERROR)
                {
                    iecs.kstate = IsoTpktState.TPKT_RECEIVE_START;
                    tcps.logger.LogError("iec61850tpktState.IEC61850_RECEIVE_ERROR\n");
                    break;
                }
                switch (iecs.kstate)
                {
                    case IsoTpktState.TPKT_RECEIVE_START:
                        if (tcps.receivedBytes[i] == TPKT_START)
                        {
                            iecs.kstate = IsoTpktState.TPKT_RECEIVE_RES;
                            iecs.dataBufferIndex = 0;
                        }
                        else
                        {
                            tcps.logger.LogError("Synchronization lost: TPKT START / VERSION!\n");
                            iecs.kstate = IsoTpktState.TPKT_RECEIVE_ERROR;
                        }
                        break;
                    case IsoTpktState.TPKT_RECEIVE_RES:
                        if (tcps.receivedBytes[i] == TPKT_RES)
                        {
                            iecs.kstate = IsoTpktState.TPKT_RECEIVE_LEN1;
                        }
                        else
                        {
                            tcps.logger.LogError("Synchronization lost: TPKT RES!\n");
                            iecs.kstate = IsoTpktState.TPKT_RECEIVE_ERROR;
                        }
                        break;
                    case IsoTpktState.TPKT_RECEIVE_LEN1:
                        iecs.TpktLen = tcps.receivedBytes[i] << 8;
                        iecs.kstate = IsoTpktState.TPKT_RECEIVE_LEN2;
                        break;
                    case IsoTpktState.TPKT_RECEIVE_LEN2:
                        iecs.TpktLen |= tcps.receivedBytes[i];
                        if (iecs.TpktLen <= TPKT_MAXLEN)
                        {
                            iecs.kstate = IsoTpktState.TPKT_RECEIVE_DATA_COPY;
                        }
                        else
                        {
                            tcps.logger.LogError("Synchronization lost: TPKT TPDU too long!\n");
                            iecs.kstate = IsoTpktState.TPKT_RECEIVE_ERROR;
                        }
                        break;
                    case IsoTpktState.TPKT_RECEIVE_DATA_COPY:
                        int copylen = Math.Min(tcps.receivedBytes.Count - i, /*wanted*/iecs.TpktLen - TPKT_SIZEOF - iecs.dataBufferIndex);
                        Array.Copy(tcps.receivedBytes.ToArray(), i, iecs.dataBuffer, iecs.dataBufferIndex, copylen);
                        i += copylen - 1; // i will be incremented in 'for' cycle, so we must decrement here
                        iecs.dataBufferIndex += copylen;

                        if (iecs.dataBufferIndex == iecs.TpktLen - TPKT_SIZEOF)
                        {
                            iecs.kstate = IsoTpktState.TPKT_RECEIVE_START;

                            IsoCotp.CotpReceiveResult receiveResult = await iecs.iso.Receive(iecs);


                            if (iecs.ostate == IsoProtocolState.OSI_STATE_SHUTDOWN)
                            {
                                return null;
                            }


                            if (receiveResult != IsoCotp.CotpReceiveResult.WAIT)
                            {

                                if (receiveResult == IsoCotp.CotpReceiveResult.INIT) return null;


                                if (iecs.iso.ReceiveResult == null)
                                {

                                }
                                return iecs.iso.ReceiveResult;

                            }
                            else
                            {
                                if (tcps.receivedBytes.Count == i+1)
                                {
                                    tcps.receivedBytes.AddRange(await TcpRw.ReadBytesFromSocket(tcps));
                                }
                                if (TcpRw.GetIsDataAvailableOnSocket(tcps))
                                {
                                    tcps.receivedBytes.AddRange(await TcpRw.ReadBytesFromSocket(tcps));

                                }
                            }
                        }
                        break;
                    default:
                        tcps.logger.LogError("iecs.tstate: unknown state!\n");
                        break;
                }	// switch
            }	// for
            return null;
        }

        //public static void Send(TcpState tcps)
        //{
        //    // TPKT
        //    tcps.sendBuffer[IsoTpkt.TPKT_IDX_START] = IsoTpkt.TPKT_START;
        //    tcps.sendBuffer[IsoTpkt.TPKT_IDX_RES] = IsoTpkt.TPKT_RES;
        //    Array.Copy(BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)(tcps.sendBytes))), 0, tcps.sendBuffer, IsoTpkt.TPKT_IDX_LEN, 2);

        //    tcps.logger.LogDebugBuffer("Send Tpkt", tcps.sendBuffer, 0, tcps.sendBytes);
        //    TcpRw.Send(tcps);
        //}

        public static async Task<MMSpdu> SendAsync(TcpState tcps,bool isMmsCanBeNull=false)
        {
            // TPKT
            tcps.sendBuffer[IsoTpkt.TPKT_IDX_START] = IsoTpkt.TPKT_START;
            tcps.sendBuffer[IsoTpkt.TPKT_IDX_RES] = IsoTpkt.TPKT_RES;
            Array.Copy(BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)(tcps.sendBytes))), 0, tcps.sendBuffer, IsoTpkt.TPKT_IDX_LEN, 2);

            tcps?.logger?.LogDebugBuffer("Send Tpkt", tcps.sendBuffer, 0, tcps.sendBytes);
            MMSpdu mmSpdu= await TcpRw.GetMmsPduAsync(tcps);
            if (mmSpdu == null&&!isMmsCanBeNull)
            {
                mmSpdu = await TcpRw.GetMmsPduAsync(tcps);
            }
            if (mmSpdu == null && !isMmsCanBeNull)
            {
                mmSpdu = await TcpRw.GetMmsPduAsync(tcps);
            }
            return mmSpdu;
        }

        public static async Task<MMSpdu> SendMmsRequestAsync(TcpState tcps, bool isMmsCanBeNull = false)
        {
            // TPKT
            tcps.sendBuffer[IsoTpkt.TPKT_IDX_START] = IsoTpkt.TPKT_START;
            tcps.sendBuffer[IsoTpkt.TPKT_IDX_RES] = IsoTpkt.TPKT_RES;
            Array.Copy(BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)(tcps.sendBytes))), 0, tcps.sendBuffer, IsoTpkt.TPKT_IDX_LEN, 2);
            tcps?.logger?.LogDebugBuffer("Send Tpkt", tcps.sendBuffer, 0, tcps.sendBytes);
            MMSpdu mmSpdu = await TcpRw.SendMmsAsync(tcps);
            return mmSpdu;
        }
        public static async Task<MMSpdu> GetAnswerMmsAsync(TcpState tcps, bool isMmsCanBeNull = false)
        {
            // TPKT
            tcps.sendBuffer[IsoTpkt.TPKT_IDX_START] = IsoTpkt.TPKT_START;
            tcps.sendBuffer[IsoTpkt.TPKT_IDX_RES] = IsoTpkt.TPKT_RES;
            Array.Copy(BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)(tcps.sendBytes))), 0, tcps.sendBuffer, IsoTpkt.TPKT_IDX_LEN, 2);

            tcps?.logger?.LogDebugBuffer("Send Tpkt", tcps.sendBuffer, 0, tcps.sendBytes);
            MMSpdu mmSpdu = await TcpRw.GetAnswerMmsPduAsync(tcps);
            if (mmSpdu == null && !isMmsCanBeNull)
            {
                mmSpdu = await TcpRw.GetAnswerMmsPduAsync(tcps);
            }
            if (mmSpdu == null && !isMmsCanBeNull)
            {
                mmSpdu = await TcpRw.GetAnswerMmsPduAsync(tcps);
            }
            return mmSpdu;
        }


    }
}
