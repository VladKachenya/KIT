using System;

namespace BISC.Infrastructure.Global.Exceptions
{
    public class BadResponseByMmsException : Exception
    {
        public BadResponseByMmsException(string message) 
            : base(message)
        { }
    }
}