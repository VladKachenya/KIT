using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Tests.Model.Helpers
{
   public static class Extensions
    {
        public static T RunSync<T>(this Task<T> task)
        {
            task.Wait();
            return task.Result;
        }

    }
}
