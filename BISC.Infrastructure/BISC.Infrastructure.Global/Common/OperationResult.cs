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

        public OperationResult()
        {
            IsSucceed = true;
        }
        public OperationResult(string error)
        {
            IsSucceed = false;
            ErrorList.Add(error);
        }

        public OperationResult(List<string> errors)
        {
            IsSucceed = false;
            ErrorList.AddRange(errors);
        }

        public List<string> ErrorList { get; } = new List<string>();

        public string GetFirstError()
        {
            return ErrorList.FirstOrDefault();
        }
        public bool IsSucceed { get; protected set; }
    }



    public class OperationResult<T>:OperationResult
    {
        public T Item { get; set; }
        public OperationResult(T resultItem, bool isSucceed = true,string error=null)
        {
            IsSucceed = isSucceed;
            Item = resultItem;
            ErrorList.Add(error);
        }
        public OperationResult(T resultItem, List<string> errors,bool isSucceed = true)
        {
            IsSucceed = isSucceed;
            Item = resultItem;
            ErrorList.AddRange(errors);
        }
        public OperationResult(string error) : base(error)
        {
        }
    }
}
