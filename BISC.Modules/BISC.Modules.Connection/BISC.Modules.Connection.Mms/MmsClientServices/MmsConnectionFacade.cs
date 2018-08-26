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
            var listIdent=new List<string>();
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
            OperationResult<List<string>> operationResult=new OperationResult<List<string>>(listIdent);
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
    }
}