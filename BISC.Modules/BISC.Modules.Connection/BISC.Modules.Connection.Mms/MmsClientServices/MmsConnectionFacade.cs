using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Modules.Connection.Infrastructure.Connection;
using BISC.Modules.Connection.MMS.MMS_ASN1_Model;

namespace BISC.Modules.Connection.MMS.MmsClientServices
{
    public class MmsConnectionFacade : IMmsConnectionFacade
    {
        private string _ip;
        private readonly Iec61850State _state;

        public MmsConnectionFacade()
        {
            _state = new Iec61850State();
        }

        public Task<bool> TryOpenConnection(string ip)
        {
            _ip = ip;
            return new InitService(_state).TryOpenConnection(_ip);
        }

        public bool CheckConnection()
        {
            if (_ip == null) return false;
            return TcpRw.CheckConnection(_state);
        }

        public void StopConnection()
        {
            TcpRw.StopClient(_state);
        }

        public async Task<OperationResult<List<string>>> IdentifyAsync()
        {
            var identResult = await (new InitService(_state)).SendIdentifyAsync();
            var listIdent = new List<string>();
            if (!identResult.isRejectPDUSelected())
            {
                if (identResult?.Confirmed_ResponsePDU.Service.Identify != null)
                {
                    listIdent = new List<string>()
                    {
                        identResult.Confirmed_ResponsePDU.Service.Identify.VendorName.Value,
                        identResult.Confirmed_ResponsePDU.Service.Identify.ModelName.Value,
                        identResult.Confirmed_ResponsePDU.Service.Identify.Revision.Value,

                    };
                }
                else
                {
                    return new OperationResult<List<string>>("");
                }
            }
            OperationResult<List<string>> operationResult = new OperationResult<List<string>>(listIdent);
            return operationResult;
        }

        public async Task<OperationResult<List<string>>> GetLdListAsync()
        {
            var receivedPdu = await new InfoModelClientService(_state).SendGetNameListDomainAsync();

            return new OperationResult<List<string>>(receivedPdu.Confirmed_ResponsePDU.Service.GetNameList
                .ListOfIdentifier
                .Select((identifier => identifier.Value.ToString())).ToList());
        }

        public async Task<OperationResult<List<string>>> GetListValiablesAsync(string ldInst)
        {
            MMSpdu receivedPdu;
            List<string> ldIdentifiersList = new List<string>();
            do
            {
                receivedPdu = await new InfoModelClientService(_state).SendGetNameListVariablesAsync(ldInst,
                    ldIdentifiersList.LastOrDefault());
                if (receivedPdu == null)
                {
                    await Task.Delay(2000);
                    receivedPdu = await new InfoModelClientService(_state).SendGetNameListVariablesAsync(ldInst,
                        ldIdentifiersList.LastOrDefault());
                    if (receivedPdu == null)
                    {

                    }
                }
                if (receivedPdu?.Confirmed_ResponsePDU?.Service?.GetNameList != null)
                {
                    ldIdentifiersList.AddRange(
                        receivedPdu.Confirmed_ResponsePDU.Service.GetNameList.ListOfIdentifier.Select((identifier =>
                            identifier.Value)));
                }
                else
                {
                    break;
                }


            } while (receivedPdu.Confirmed_ResponsePDU.Service.GetNameList.MoreFollows);
            return new OperationResult<List<string>>(ldIdentifiersList);
        }

        public async Task<OperationResult<MmsTypeDescription>> GetMmsTypeDescription(string ldName, string lnName)
        {
            var typeDescription = await new InfoModelClientService(_state).SendGetVariableAccessAttributesAsync(ldName, lnName);

            var response = typeDescription.Confirmed_ResponsePDU.Service.GetVariableAccessAttributes;
            MmsTypeDescription mmsTypeDescription = GetMmsTypeDescription(response.TypeDescription, "");
            return new OperationResult<MmsTypeDescription>(mmsTypeDescription);

        }

        private MmsTypeDescription GetMmsTypeDescription(TypeDescription responseTypeDescription, string name)
        {
            MmsTypeDescription mmsTypeDescription = new MmsTypeDescription();
            mmsTypeDescription.Name = name;
            mmsTypeDescription.IsArray = responseTypeDescription.isArraySelected();
            mmsTypeDescription.IsStructure = responseTypeDescription.isStructureSelected();
            if (responseTypeDescription.isBit_stringSelected())
            {

                mmsTypeDescription.BasicType = tBasicTypeEnum.bit_string;

            }
            else if (responseTypeDescription.isArraySelected())
            {
                mmsTypeDescription.BasicType = tBasicTypeEnum.Struct;
            }
            else if (responseTypeDescription.Integer != null)
            {
                switch (responseTypeDescription.Integer.Value)
                {
                    case 8:
                        mmsTypeDescription.BasicType = tBasicTypeEnum.INT8;
                        break;
                    case 16:
                        mmsTypeDescription.BasicType = tBasicTypeEnum.INT16;
                        break;
                    case 24:
                        mmsTypeDescription.BasicType = tBasicTypeEnum.INT24;
                        break;
                    case 32:
                        mmsTypeDescription.BasicType = tBasicTypeEnum.INT32;
                        break;
                }
            }
            else if (responseTypeDescription.isBcdSelected())
            {
            }
            else if (responseTypeDescription.isBooleanSelected())
            {
                mmsTypeDescription.BasicType = tBasicTypeEnum.BOOLEAN;
            }
            else if (responseTypeDescription.isFloating_pointSelected())
            {
                switch (responseTypeDescription.Floating_point.Format_width.Value)
                {
                    case 32:
                        mmsTypeDescription.BasicType = tBasicTypeEnum.FLOAT32;
                        break;
                    case 64:
                        mmsTypeDescription.BasicType = tBasicTypeEnum.FLOAT64;
                        break;
                }
            }
            else if (responseTypeDescription.isGeneralized_timeSelected() || responseTypeDescription.isUtc_timeSelected() || responseTypeDescription.isBinary_timeSelected())
            {
                mmsTypeDescription.BasicType = tBasicTypeEnum.Timestamp;
            }
            else if (responseTypeDescription.isOctet_stringSelected())
            {
                mmsTypeDescription.BasicType = tBasicTypeEnum.Octet64;
            }
            else if (responseTypeDescription.isVisible_stringSelected())
            {
                switch (responseTypeDescription.Visible_string.Value)
                {
                    case 255:
                    case -255:
                        mmsTypeDescription.BasicType = tBasicTypeEnum.VisString255;
                        break;
                    case 129:
                    case -129:
                        mmsTypeDescription.BasicType = tBasicTypeEnum.VisString129;
                        break;
                    case 64:
                    case -64:
                        mmsTypeDescription.BasicType = tBasicTypeEnum.VisString64;
                        break;
                    case 32:
                    case -32:
                        mmsTypeDescription.BasicType = tBasicTypeEnum.VisString32;
                        break;
                    case 65:
                    case -65:
                        mmsTypeDescription.BasicType = tBasicTypeEnum.VisString65;
                        break;
                }
            }
            if (mmsTypeDescription.IsStructure)
            {
                foreach (var component in responseTypeDescription.Structure.Components)
                {
                    mmsTypeDescription.Components.Add(GetMmsTypeDescription(component.ComponentType.TypeDescription, component.ComponentName.Value));
                }
            }
            return mmsTypeDescription;
        }
    }
}