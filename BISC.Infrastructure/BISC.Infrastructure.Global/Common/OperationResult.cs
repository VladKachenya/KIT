using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Infrastructure.Global.Common
{
   public class OperationResult
    {
        public static OperationResult SucceedResult
        {
            get
            {
                return new OperationResult();
            }
        }

        private OperationResult()
        {
            IsSucceed = true;
        }
        public OperationResult(string error)
        {
            IsSucceed = false;
            ErrorList.Add(error);
        }

        public List<string> ErrorList { get; } = new List<string>();

        public string GetFirstError()
        {
            return ErrorList.FirstOrDefault();
        }
        public bool IsSucceed { get; }
    }
}
