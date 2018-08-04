using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Infrastructure.Global.IoC
{
   public class StaticContainer
    {
        private static IInjectionContainer _currentContainer;

        public static void SetContainer(IInjectionContainer injectionContainer)
        {
            _currentContainer = injectionContainer;
        }

        public static IInjectionContainer CurrentContainer => _currentContainer;
    }
}
