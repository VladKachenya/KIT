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
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Connection.MMS.MMS_ASN1_Model;

namespace BISC.Modules.Connection.MMS
{
    public class TcpRw
    {
        public static async Task StartClientAsync(TcpState tcps)
        {
            // Connect to a remote device.
            try
            {
                // Establish the remote endpoint for the socket.
                IPHostEntry ipHostInfo;
                //if (tcps.hostname == "localhost" || tcps.hostname == "127.0.0.1")
                //{
                //    ipHostInfo = Dns.GetHostEntry("");
                //}
                //else
                IPAddress ipAddress = null;

                if (!IPAddress.TryParse(tcps.hostname, out ipAddress))
                {
                    ipHostInfo = Dns.GetHostEntry(tcps.hostname);
                    for (int i = 0; i < ipHostInfo.AddressList.Length; i++)
                    {
                        if (ipHostInfo.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                        {
                            ipAddress = ipHostInfo.AddressList[i];
                            break;
                        }
                    }
                }
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, tcps.port);

                // Create a TCP/IP socket.
                tcps.workSocket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                // KeepAlive`
                if (tcps.keepalive_time > 0 && tcps.keepalive_interval > 0)
                    SetKeepAlive(tcps.workSocket, tcps.keepalive_time, tcps.keepalive_interval);

                await Task.Run((() =>
                {
                    tcps.workSocket.Connect(remoteEP);
                }));

            }
            catch (Exception e)
            {
                StopClient(tcps);
                tcps.logger.LogError(e.ToString());
                throw;
            }
        }

        public static bool CheckConnection(Iec61850State iecs)
        {
            try
            {
                if (iecs.workSocket == null) return false;
                if (iecs.workSocket.Poll(5000, SelectMode.SelectRead) && iecs.workSocket.Available == 0)
                {
                    return false;
                }
                return true;
            }
            catch
            {

            }
            return false;
        }

        public static void StopClient(TcpState tcps)
        {
            // Connect to a remote device.
            //tcps.logger.LogInfo("StopClient: Socket shutdowned.");
            try
            {

                // Release the socket.
                if (tcps.workSocket != null)
                {
                    if (tcps.workSocket.Connected)
                    {
                        tcps.workSocket.Shutdown(SocketShutdown.Both);
                        //tcps.receiveDone.WaitOne(15000);
                    }
                    tcps.workSocket.Close();
                    tcps.workSocket.Dispose();
                    tcps.workSocket = null;
                }
            }
            catch (Exception e)
            {
                tcps.logger.LogError("Closing: " + e.ToString());
            }
        }



        //public static async Task<MMSpdu> GetMmsPduAsync(TcpState tcps)
        //{
        //    if (tcps.workSocket == null) return null;
        //    try
        //    {
        //        await Task.Factory.FromAsync(
        //             tcps.workSocket.BeginSend(tcps.sendBuffer, 0, tcps.sendBytes, SocketFlags.None, null, tcps.workSocket),
        //             tcps.workSocket.EndSend);
        //        tcps.NumberOfReceivedBytes = await Task.Factory.FromAsync(
        //            tcps.workSocket.BeginReceive(tcps.recvBuffer, 0, Iec61850State.recvBufferSize, SocketFlags.None, null, tcps.workSocket),
        //            tcps.workSocket.EndReceive);

        //        return await IsoTpkt.GetAnswer(tcps);
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //    return null;
        //}

        public static async Task<MMSpdu> SendMmsAsync(TcpState tcps)
        {
            if (tcps.workSocket == null) return null;
            await Task.Delay(StaticContainer.CurrentContainer.ResolveType<IConfigurationService>().MmsQueryDelay);
            try
            {
                await Task.Run((() =>
                {
                    lock (tcps)
                    {
                        if (tcps.workSocket.Available > 0)
                        {
                            var bytes = new byte[tcps.workSocket.Available];
                            tcps.workSocket.Receive(bytes);
                        }
                        tcps.workSocket.Send(tcps.sendBuffer, 0, tcps.sendBytes, SocketFlags.None);
                    }

                }));
            }
            catch (Exception e)
            {

            }
            return null;
        }
        public static async Task<MMSpdu> GetAnswerMmsPduAsync(TcpState tcps)
        {
            if (tcps.workSocket == null) return null;
            await Task.Delay(StaticContainer.CurrentContainer.ResolveType<IConfigurationService>().MmsQueryDelay);
            try
            {
                tcps.receivedBytes = null;
                var mmsPdu = await IsoTpkt.GetAnswer(tcps);
                // if ((tcps as Iec61850State).ostate == IsoProtocolState.OSI_STATE_SHUTDOWN)
                // {
                //StopClient(tcps);
                // }
                return mmsPdu;
            }
            catch (Exception e)
            {

            }
            return null;
        }


        public static async Task<MMSpdu> GetMmsPduAsync(TcpState tcps)
        {
            if (tcps.workSocket == null) return null;
            await Task.Delay(StaticContainer.CurrentContainer.ResolveType<IConfigurationService>().MmsQueryDelay);
            try
            {


                await Task.Run((() =>
                {

                    lock (tcps)
                    {
                        if (tcps.workSocket.Available > 0)
                        {
                            var bytes = new byte[tcps.workSocket.Available];
                            tcps.workSocket.Receive(bytes);
                        }
                        tcps.workSocket.Send(tcps.sendBuffer, 0, tcps.sendBytes, SocketFlags.None);
                    }

                }));

                tcps.receivedBytes = null;

                var mmsPdu = await IsoTpkt.GetAnswer(tcps);
                return mmsPdu;

            }
            catch (Exception e)
            {

            }
            return null;
        }

        public static bool GetIsDataAvailableOnSocket(TcpState tcpState)
        {
            return tcpState.workSocket.Available > 0;
        }

        public static async Task<byte[]> ReadBytesFromSocket(TcpState tcps)
        {
            var buffer = new List<byte>();
            await Task.Run((() =>
            {
                lock (tcps)
                {
                    var configurationServer = StaticContainer.CurrentContainer.ResolveType<IConfigurationService>();
                    int millSec = 0;
                    do
                    {
                        Thread.Sleep(1);
                        millSec++;
                        if (millSec > configurationServer.MaxResponseTime) throw new SocketException();
                    } while (tcps.workSocket.Available == 0);
                    Thread.Sleep(1);

                    while (tcps.workSocket.Available > 0)
                    {
                        var availableBytes = tcps.workSocket.Available;
                        var currByte = new Byte[availableBytes];
                        var byteCounter = tcps.workSocket.Receive(currByte, currByte.Length, SocketFlags.None);
                        if (byteCounter.Equals(availableBytes))
                        {
                            buffer.AddRange(currByte);
                        }
                        Thread.Sleep(5);
                    }
                }
            }));
            return buffer.ToArray();
        }



        /// <summary>
        /// Sets socket to keepalive mode
        /// </summary>
        /// <param name="s">Socket</param>
        /// <param name="keepalive_time">time</param>
        /// <param name="keepalive_interval">interval</param>
        public static void SetKeepAlive(Socket s, ulong keepalive_time, ulong keepalive_interval)
        {
            int bytes_per_long = 32 / 8;
            byte[] keep_alive = new byte[3 * bytes_per_long];
            ulong[] input_params = new ulong[3];
            int i1;
            int bits_per_byte = 8;

            if (keepalive_time == 0 || keepalive_interval == 0)
                input_params[0] = 0;
            else
                input_params[0] = 1;
            input_params[1] = keepalive_time;
            input_params[2] = keepalive_interval;
            for (i1 = 0; i1 < input_params.Length; i1++)
            {
                keep_alive[i1 * bytes_per_long + 3] = (byte)(input_params[i1] >> ((bytes_per_long - 1) * bits_per_byte) & 0xff);
                keep_alive[i1 * bytes_per_long + 2] = (byte)(input_params[i1] >> ((bytes_per_long - 2) * bits_per_byte) & 0xff);
                keep_alive[i1 * bytes_per_long + 1] = (byte)(input_params[i1] >> ((bytes_per_long - 3) * bits_per_byte) & 0xff);
                keep_alive[i1 * bytes_per_long + 0] = (byte)(input_params[i1] >> ((bytes_per_long - 4) * bits_per_byte) & 0xff);
            }
            s.IOControl(IOControlCode.KeepAliveValues, keep_alive, null);
        } /* method AsyncSocket SetKeepAlive */


        public static async Task Reconnect(TcpState tcps)
        {
            StopClient(tcps);
            await StartClientAsync(tcps);
        }
    }
}
