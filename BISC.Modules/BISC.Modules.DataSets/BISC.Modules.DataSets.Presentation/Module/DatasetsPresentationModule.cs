using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;

namespace BISC.Modules.DataSets.Presentation.Module
{
   public class DatasetsPresentationModule:IAppModule
    {
        private readonly IInjectionContainer _injectionContainer;

        public DatasetsPresentationModule(IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
        }


        #region Implementation of IAppModule

        public void Initialize()
        {
           
        }

        #endregion
    }
}
