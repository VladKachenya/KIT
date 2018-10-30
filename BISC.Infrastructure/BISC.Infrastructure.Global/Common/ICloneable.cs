﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Infrastructure.Global.Common
{
   public interface ICloneable<out T>
   {
       T Clone();
   }
}
