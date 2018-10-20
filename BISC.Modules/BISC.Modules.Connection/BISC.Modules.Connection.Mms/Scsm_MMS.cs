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
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BISC.Modules.Connection.Infrastructure.Connection;
using BISC.Modules.Connection.MMS.Dto;
using BISC.Modules.Connection.MMS.MMS_ASN1_Model;
using BISC.Modules.Connection.MMS.org.bn;
using BISC.Modules.Connection.MMS.org.bn.types;
using DataSetDto = BISC.Modules.Connection.MMS.Dto.DataSetDto;

namespace BISC.Modules.Connection.MMS
{
    public class Scsm_MMS
    {
        private readonly Iec61850State _state;

        public Scsm_MMS(Iec61850State state)
        {
            _state = state;
        }

        // Protocol IEC6850 - definitions
        // OptFlds - report Optional Fields
        // 1st Byte
        public const byte OptFldsReserved = 0x80; // bit "0" in MMS interpretation
        public const byte OptFldsSeqNum = 0x40;
        public const byte OptFldsTimeOfEntry = 0x20;
        public const byte OptFldsReasonCode = 0x10;
        public const byte OptFldsDataSet = 0x08;
        public const byte OptFldsDataReference = 0x04;
        public const byte OptFldsOvfl = 0x02;
        public const byte OptFldsEntryID = 0x01;
        // 2nd Byte
        public const byte OptFldsConfRev = 0x80;
        public const byte OptFldsMoreSegments = 0x40; // bit "10" in MMS interpretation

        // TrgOps - report Trigger Options
        public const byte TrgOpsReserved = 0x80;	// bit "0" in MMS interpretation
        public const byte TrgOpsDataChange = 0x40;
        public const byte TrgOpsQualChange = 0x20;
        public const byte TrgOpsDataActual = 0x10;
        public const byte TrgOpsIntegrity = 0x08;
        public const byte TrgOpsGI = 0x04; // bit "6" in MMS interpretation

        // DatQual - Data Quality Codes
        public const byte DatQualValidity0 = 0x80; // bit "0" in MMS interpretation
        public const byte DatQualValidity1 = 0x40;
        public const byte DatQualOverflow = 0x20;
        public const byte DatQualOutOfRange = 0x10;
        public const byte DatQualBadReference = 0x08;
        public const byte DatQualOscillatory = 0x04;
        public const byte DatQualFailure = 0x02;
        public const byte DatQualOldData = 0x01;
        // 2nd Byte
        public const byte DatQualInconsistent = 0x80;
        public const byte DatQualInaccurate = 0x40;
        public const byte DatQualSource = 0x20;
        public const byte DatQualTest = 0x10;
        public const byte DatQualOperatorBlocked = 0x08; // bit "12" in MMS interpretation

        // TimQual - Time Quality Codes
        public const byte TimQualExtraSeconds = 0x80; // bit "0" in MMS interpretation
        public const byte TimQualTimeBaseErr = 0x40;
        public const byte TimQualNotSynchronized = 0x20; // bit "2". Bits 3-7=time precision encoding

        // Report Reading Phases
        const int phsRptID = 0;
        const int phsOptFlds = 1;
        const int phsSeqNum = 2;
        const int phsTimeOfEntry = 3;
        const int phsDatSet = 4;
        const int phsBufOvfl = 5;
        const int phsEntryID = 6;
        const int phsConfRev = 7;
        const int phsSubSeqNr = 8;
        const int phsMoreSegmentsFollow = 9;
        const int phsInclusionBitstring = 10;
        const int phsDataReferences = 11;
        const int phsValues = 12;
        const int phsReasonCodes = 13;

        IDecoder decoder = CoderFactory.getInstance().newDecoder("BER");
        IEncoder encoder = CoderFactory.getInstance().newEncoder("BER");

        int InvokeID = 0;


   

        /*///////////////////////////////////////////////////////////////////////////////////////////////////////////////////*/

        public async Task<MMSpdu> ReceiveData(Iec61850State iecs)
        {
            if (iecs == null)
                return null;

            iecs.logger.LogDebugBuffer("mms.ReceiveData", iecs.msMMS.GetBuffer(), iecs.msMMS.Position, iecs.msMMS.Length - iecs.msMMS.Position);

            MMSpdu mymmspdu = null;
            try
            {
                MMSCapture cap = null;
                byte[] pkt = iecs.msMMS.ToArray();
                if (iecs.CaptureDb.CaptureActive) cap = new MMSCapture(pkt, iecs.msMMS.Position, pkt.Length, MMSCapture.CaptureDirection.In);
                ////////////////// Decoding
                mymmspdu = decoder.decode<MMSpdu>(iecs.msMMS);
                ////////////////// Decoding
                if (iecs.CaptureDb.CaptureActive && mymmspdu != null)
                {
                    cap.MMSPdu = mymmspdu;
                    iecs.CaptureDb.AddPacket(cap);
                }
            }
            catch (Exception e)
            {
                iecs.logger.LogError("mms.ReceiveData: Malformed MMS Packet received!!!: " + e.Message);
            }
            return mymmspdu;
        }



        public async Task<int> SendReadVLAsync(Iec61850State iecs)
        {
            MMSpdu mymmspdu = new MMSpdu();
            iecs.msMMSout = new MemoryStream();

            Confirmed_RequestPDU crreq = new Confirmed_RequestPDU();
            ConfirmedServiceRequest csrreq = new ConfirmedServiceRequest();
            Read_Request rreq = new Read_Request();

            //NodeBase b = el.Data[0];    // Must be NodeVL

            ObjectName on = new ObjectName();
            ObjectName.Domain_specificSequenceType dst = new ObjectName.Domain_specificSequenceType();

            // dst.DomainID = new Identifier(b.CommAddress.Domain);
            //  dst.ItemID = new Identifier(b.CommAddress.Variable);
            iecs.logger.LogDebug("SendRead: Reading with NVL: " + dst.ItemID.Value);
            on.selectDomain_specific(dst);

            rreq.VariableAccessSpecification = new VariableAccessSpecification();
            rreq.VariableAccessSpecification.selectVariableListName(on);
            rreq.SpecificationWithResult = true;

            csrreq.selectRead(rreq);

            crreq.InvokeID = new Unsigned32(InvokeID++);

            crreq.Service = csrreq;

            mymmspdu.selectConfirmed_RequestPDU(crreq);

            encoder.encode<MMSpdu>(mymmspdu, iecs.msMMSout);

            if (iecs.msMMSout.Length == 0)
            {
                iecs.logger.LogError("mms.SendRead: Encoding Error!");
                return -1;
            }

            await this.SendAsync(iecs, mymmspdu);

            return 0;
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
                    dat.selectUnsigned((long)(uint)valueToSave);
                    break;
            }
            datl.Add(dat);

            _state.logger.LogDebug("SendWrite: Writing: " + dst.ItemID.Value);

            wreq.VariableAccessSpecification = new VariableAccessSpecification();
            wreq.VariableAccessSpecification.selectListOfVariable(vasl);
            wreq.ListOfData = datl;

            csrreq.selectWrite(wreq);

            crreq.InvokeID = new Unsigned32(InvokeID++);

            crreq.Service = csrreq;

            mymmspdu.selectConfirmed_RequestPDU(crreq);

            encoder.encode<MMSpdu>(mymmspdu, _state.msMMSout);


            return await this.SendAsync(_state, mymmspdu);


        }



        public async Task<MMSpdu> SendWriteAsync(tBasicTypeEnum basicType, string ldFullPath, string lnName, string fcName, string newValueString, List<string> customItemPathParts = null)
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

            vas.VariableSpecification = new VariableSpecification();
            vas.VariableSpecification.selectName(on);

            vasl.Add(vas);

            switch (basicType)
            {
                case tBasicTypeEnum.BOOLEAN:
                    // dat.selectBoolean((bool)d.DataValue);
                    break;
                case tBasicTypeEnum.Enum:
              //      dat.selectUnsigned(uint.Parse(dai.ValEnumDictionary.First((pair => pair.Value == newValueString)).Key));

                    break;
                case tBasicTypeEnum.VisString129:
                case tBasicTypeEnum.VisString255:
                case tBasicTypeEnum.VisString32:
                case tBasicTypeEnum.VisString64:
                case tBasicTypeEnum.VisString65:
                    dat.selectVisible_string(newValueString);
                    break;
                case tBasicTypeEnum.Octet64:

                    //     dat.selectOctet_string((byte[])newValueString);
                    break;
                //case scsm_MMS_TypeEnum.utc_time:
                //    UtcTime val = new UtcTime((byte[])d.DataValue);
                //    dat.selectUtc_time(val);
                //    break;
                //case scsm_MMS_TypeEnum.bit_string:
                //    dat.selectBit_string(new BitString((byte[])d.DataValue, (int)d.DataParam));
                //    break;
                case tBasicTypeEnum.INT16U:
                case tBasicTypeEnum.INT24U:
                case tBasicTypeEnum.INT32U:
                case tBasicTypeEnum.INT8U:
                    dat.selectUnsigned(uint.Parse(newValueString));
                    break;
                case tBasicTypeEnum.INT128:
                case tBasicTypeEnum.INT8:
                case tBasicTypeEnum.INT16:
                case tBasicTypeEnum.INT32:
                case tBasicTypeEnum.INT24:
                    dat.selectInteger(int.Parse(newValueString));
                    break;
                case tBasicTypeEnum.FLOAT64:
                case tBasicTypeEnum.FLOAT32:
                    byte[] byteval;
                    byte[] tmp;
                    float f = float.Parse(newValueString);
                    // if (d.DataValue is float)
                    //{
                    byteval = new byte[5];
                    tmp = BitConverter.GetBytes(f);
                    byteval[4] = tmp[0];
                    byteval[3] = tmp[1];
                    byteval[2] = tmp[2];
                    byteval[1] = tmp[3];
                    byteval[0] = 0x08;
                    //   }
                    //else
                    //{
                    //    byteval = new byte[9];
                    //    tmp = BitConverter.GetBytes((float)d.DataValue);
                    //    byteval[8] = tmp[0];
                    //    byteval[7] = tmp[1];
                    //    byteval[6] = tmp[2];
                    //    byteval[5] = tmp[3];
                    //    byteval[4] = tmp[4];
                    //    byteval[3] = tmp[5];
                    //    byteval[2] = tmp[6];
                    //    byteval[1] = tmp[7];
                    //    byteval[0] = 0x08;      // ???????????? TEST
                    //}
                    FloatingPoint fpval = new FloatingPoint(byteval);
                    dat.selectFloating_point(fpval);
                    break;
            }
            datl.Add(dat);

            _state.logger.LogDebug("SendWrite: Writing: " + dst.ItemID.Value);

            wreq.VariableAccessSpecification = new VariableAccessSpecification();
            wreq.VariableAccessSpecification.selectListOfVariable(vasl);
            wreq.ListOfData = datl;

            csrreq.selectWrite(wreq);

            crreq.InvokeID = new Unsigned32(InvokeID++);

            crreq.Service = csrreq;

            mymmspdu.selectConfirmed_RequestPDU(crreq);

            encoder.encode<MMSpdu>(mymmspdu, _state.msMMSout);


            return await this.SendAsync(_state, mymmspdu);

        }

        public async Task<int> SendWriteAsStructureAsync(Iec61850State iecs)
        {
            MMSpdu mymmspdu = new MMSpdu();
            iecs.msMMSout = new MemoryStream();

            Confirmed_RequestPDU crreq = new Confirmed_RequestPDU();
            ConfirmedServiceRequest csrreq = new ConfirmedServiceRequest();
            Write_Request wreq = new Write_Request();

            List<VariableAccessSpecification.ListOfVariableSequenceType> vasl = new List<VariableAccessSpecification.ListOfVariableSequenceType>();
            List<Data> datList_Seq = new List<Data>();
            List<Data> datList_Struct = new List<Data>();

            VariableAccessSpecification.ListOfVariableSequenceType vas = new VariableAccessSpecification.ListOfVariableSequenceType();
            Data dat_Seq = new Data();
            ObjectName on = new ObjectName();
            ObjectName.Domain_specificSequenceType dst = new ObjectName.Domain_specificSequenceType();
            //  dst.DomainID = new Identifier(el.Address.Domain);
            // dst.ItemID = new Identifier(el.Address.Variable);   // until Oper
            on.selectDomain_specific(dst);
            vas.VariableSpecification = new VariableSpecification();
            vas.VariableSpecification.selectName(on);
            vasl.Add(vas);

            //   MakeStruct(iecs, el.Data, datList_Struct);
            iecs.logger.LogDebug("SendWrite: Writing Command Structure: " + dst.ItemID.Value);

            dat_Seq.selectStructure(datList_Struct);
            datList_Seq.Add(dat_Seq);

            wreq.VariableAccessSpecification = new VariableAccessSpecification();
            wreq.VariableAccessSpecification.selectListOfVariable(vasl);
            wreq.ListOfData = datList_Seq;

            csrreq.selectWrite(wreq);

            crreq.InvokeID = new Unsigned32(InvokeID++);

            crreq.Service = csrreq;

            mymmspdu.selectConfirmed_RequestPDU(crreq);

            encoder.encode<MMSpdu>(mymmspdu, iecs.msMMSout);

            if (iecs.msMMSout.Length == 0)
            {
                iecs.logger.LogError("mms.SendWriteAsStructure: Encoding Error!");
                return -1;
            }

            await this.SendAsync(iecs, mymmspdu);
            return 0;
        }

        //private static void MakeStruct(Iec61850State iecs, NodeBase[] data, List<Data> datList_Struct)
        //{
        //    foreach (NodeData d in data)
        //    {
        //        Data dat_Struct = new Data();

        //        switch (d.DataType)
        //        {
        //            case scsm_MMS_TypeEnum.boolean:
        //                dat_Struct.selectBoolean((bool)d.DataValue);
        //                break;
        //            case scsm_MMS_TypeEnum.visible_string:
        //                dat_Struct.selectVisible_string((string)d.DataValue);
        //                break;
        //            case scsm_MMS_TypeEnum.octet_string:
        //                dat_Struct.selectOctet_string((byte[])d.DataValue);
        //                break;
        //            case scsm_MMS_TypeEnum.utc_time:
        //                UtcTime val = new UtcTime((byte[])d.DataValue);
        //                dat_Struct.selectUtc_time(val);
        //                break;
        //            case scsm_MMS_TypeEnum.bit_string:
        //                dat_Struct.selectBit_string(new BitString((byte[])d.DataValue, (int)d.DataParam));
        //                break;
        //            case scsm_MMS_TypeEnum.unsigned:
        //                dat_Struct.selectUnsigned((long)d.DataValue);
        //                break;
        //            case scsm_MMS_TypeEnum.integer:
        //                dat_Struct.selectInteger((long)d.DataValue);
        //                break;
        //            case scsm_MMS_TypeEnum.structure:
        //                List<Data> datList_Struct2 = new List<Data>();
        //                MakeStruct(iecs, d.GetChildNodes(), datList_Struct2);          // Recursive call
        //                dat_Struct.selectStructure(datList_Struct2);
        //                break;
        //            default:
        //                iecs.logger.LogError("mms.SendWrite: Cannot send unknown datatype!");
        //                //return 1;
        //                break;
        //        }
        //        datList_Struct.Add(dat_Struct);

        //    }
        //}

  
        public async Task<int> SendFileDirectoryAsync(Iec61850State iecs)
        {
            MMSpdu mymmspdu = new MMSpdu();
            iecs.msMMSout = new MemoryStream();

            Confirmed_RequestPDU crreq = new Confirmed_RequestPDU();
            ConfirmedServiceRequest csrreq = new ConfirmedServiceRequest();
            FileDirectory_Request filedreq = new FileDirectory_Request();
            FileName filename = new FileName();
            FileName conafter = new FileName();

            filename.initValue();
            //  if (el.Data[0] is NodeFile)
            //      filename.Add((el.Data[0] as NodeFile).FullName);
            //   else
            //      filename.Add(el.Address.Variable);
            filedreq.FileSpecification = filename;
            if (iecs.continueAfterFileDirectory != null && iecs.continueAfterFileDirectory.Value.Count == 1)
            {
                conafter.initValue();
                string[] name = new string[1];
                iecs.continueAfterFileDirectory.Value.CopyTo(name, 0);
                conafter.Add(name[0]);
                filedreq.ContinueAfter = conafter;
            }

            // iecs.lastFileOperationData = el.Data;

            csrreq.selectFileDirectory(filedreq);

            crreq.InvokeID = new Unsigned32(InvokeID++);

            crreq.Service = csrreq;

            mymmspdu.selectConfirmed_RequestPDU(crreq);

            encoder.encode<MMSpdu>(mymmspdu, iecs.msMMSout);

            if (iecs.msMMSout.Length == 0)
            {
                iecs.logger.LogError("mms.SendFileDirectory: Encoding Error!");
                return -1;
            }

            await this.SendAsync(iecs, mymmspdu);
            return 0;
        }

        public async Task<int> SendFileOpenAsync(Iec61850State iecs)
        {
            MMSpdu mymmspdu = new MMSpdu();
            iecs.msMMSout = new MemoryStream();

            Confirmed_RequestPDU crreq = new Confirmed_RequestPDU();
            ConfirmedServiceRequest csrreq = new ConfirmedServiceRequest();
            FileOpen_Request fileoreq = new FileOpen_Request();
            FileName filename = new FileName();

            filename.initValue();
            //    if (el.Data[0] is NodeFile)
            //      filename.Add((el.Data[0] as NodeFile).FullName);
            //  else
            {
                iecs.logger.LogError("mms.SendFileOpen: Request not a file!");
                return -1;
            }
            fileoreq.FileName = filename;
            fileoreq.InitialPosition = new Unsigned32(0);

            //   iecs.lastFileOperationData[0] = el.Data[0];

            csrreq.selectFileOpen(fileoreq);

            crreq.InvokeID = new Unsigned32(InvokeID++);

            crreq.Service = csrreq;

            mymmspdu.selectConfirmed_RequestPDU(crreq);

            encoder.encode<MMSpdu>(mymmspdu, iecs.msMMSout);

            if (iecs.msMMSout.Length == 0)
            {
                iecs.logger.LogError("mms.SendFileOpen: Encoding Error!");
                return -1;
            }

            await this.SendAsync(iecs, mymmspdu);
            return 0;
        }

        public async Task<int> SendFileReadAsync(Iec61850State iecs)
        {
            MMSpdu mymmspdu = new MMSpdu();
            iecs.msMMSout = new MemoryStream();

            Confirmed_RequestPDU crreq = new Confirmed_RequestPDU();
            ConfirmedServiceRequest csrreq = new ConfirmedServiceRequest();
            FileRead_Request filerreq = new FileRead_Request();
            //if (el.Data[0] is NodeFile)
            //    filerreq.Value = new Integer32((el.Data[0] as NodeFile).frsmId);
            //else
            {
                iecs.logger.LogError("mms.SendReadFile: Request not a file!");
                return -1;
            }

            //  iecs.lastFileOperationData[0] = el.Data[0];

            csrreq.selectFileRead(filerreq);

            crreq.InvokeID = new Unsigned32(InvokeID++);

            crreq.Service = csrreq;

            mymmspdu.selectConfirmed_RequestPDU(crreq);

            encoder.encode<MMSpdu>(mymmspdu, iecs.msMMSout);

            if (iecs.msMMSout.Length == 0)
            {
                iecs.logger.LogError("mms.SendFileRead: Encoding Error!");
                return -1;
            }

            await this.SendAsync(iecs, mymmspdu);
            return 0;
        }

        public async Task<int> SendFileCloseAsync(Iec61850State iecs)
        {
            MMSpdu mymmspdu = new MMSpdu();
            iecs.msMMSout = new MemoryStream();

            Confirmed_RequestPDU crreq = new Confirmed_RequestPDU();
            ConfirmedServiceRequest csrreq = new ConfirmedServiceRequest();
            FileClose_Request filecreq = new FileClose_Request();

            //if (el.Data[0] is NodeFile)
            //    filecreq.Value = new Integer32((el.Data[0] as NodeFile).frsmId);
            //else
            {
                iecs.logger.LogError("mms.SendCloseFile: Request not a file!");
                return -1;
            }

            //  iecs.lastFileOperationData[0] = el.Data[0];

            csrreq.selectFileClose(filecreq);

            crreq.InvokeID = new Unsigned32(InvokeID++);

            crreq.Service = csrreq;

            mymmspdu.selectConfirmed_RequestPDU(crreq);

            encoder.encode<MMSpdu>(mymmspdu, iecs.msMMSout);

            if (iecs.msMMSout.Length == 0)
            {
                iecs.logger.LogError("mms.SendCloseFile: Encoding Error!");
                return -1;
            }

            await this.SendAsync(iecs, mymmspdu);
            return 0;
        }




        private async Task<MMSpdu> SendAsync(Iec61850State iecs, MMSpdu pdu)
        {
            if (iecs.CaptureDb.CaptureActive)
            {
                MMSCapture cap;
                iecs.msMMSout.Seek(0, SeekOrigin.Begin);
                iecs.msMMSout.Read(iecs.sendBuffer, 0, (int)iecs.msMMSout.Length);
                cap = new MMSCapture(iecs.sendBuffer, 0, iecs.msMMSout.Length, MMSCapture.CaptureDirection.Out);
                cap.MMSPdu = pdu;
                iecs.CaptureDb.AddPacket(cap);
            }
            return await iecs.iso.SendAsync(iecs);
        }



        public static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return origin.AddSeconds(timestamp);
        }

        public static long ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return (long)Math.Floor(diff.TotalSeconds);
        }



    }
}
