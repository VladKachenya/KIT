using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BISC.Modules.Connection.Infrastructure.Connection;
using BISC.Modules.Connection.Infrastructure.Connection.Dto;
using BISC.Modules.Connection.MMS.Dto;
using BISC.Modules.Connection.MMS.MMS_ASN1_Model;

namespace BISC.Modules.Connection.MMS.MmsClientServices
{
    public class InfoModelClientService : MmsClientServiceBase
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


        public async Task<MMSpdu> SendGetVariableAccessAttributesAsync(string ldName, string lnName, CancellationToken? cancellationToken = null)
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

            crreq.InvokeID = new Unsigned32(_state.InvokeId++);

            crreq.Service = csrreq;

            mymmspdu.selectConfirmed_RequestPDU(crreq);

            encoder.encode<MMSpdu>(mymmspdu, _state.msMMSout);


            return await this.SendAsync(mymmspdu);

        }




        public async Task<SettingsControlDto> ReadSettingsControls(MmsTypeDescription lnMmsTypeDescription, string fc, string iedName, string lnName, string ldName)
        {
            try
            {
                var typeDescriptionForFc = lnMmsTypeDescription.Components
                    .FirstOrDefault((type => type.Name == fc));
                if (typeDescriptionForFc == null) return null;
                MMSpdu recievedMmSpdu = await (new ReadingValuesClientService(_state)).SendReadAsync(iedName + ldName, lnName, fc);
                if (recievedMmSpdu.Confirmed_ResponsePDU.Service.Read == null) return null;
                var sgcbTypeDescr = typeDescriptionForFc.Components.ToArray()[0];
                AccessResult accessResult = recievedMmSpdu.Confirmed_ResponsePDU.Service.Read.ListOfAccessResult.First();

                if (accessResult.Success == null && !accessResult.Success.isStructureSelected()) return null;
                var sgcbData = accessResult.Success.Structure.ToArray()[0];


                SettingsControlDto settingControl = new SettingsControlDto();


                int index = Array.FindIndex(sgcbTypeDescr.Components.ToArray(),
                    (type =>
                        type.Name == "NumOfSG"));
                settingControl.NumOfSGs = (uint)sgcbData.Structure.ToArray()[index].Unsigned;

                index = Array.FindIndex(sgcbTypeDescr.Components.ToArray(),
                    (type =>
                        type.Name == "ActSG"));
                settingControl.ActSG = (uint)sgcbData.Structure.ToArray()[index].Unsigned;

                return settingControl;
            }
            catch (Exception e)
            {
                return null;
            }
         
        }

    }
}