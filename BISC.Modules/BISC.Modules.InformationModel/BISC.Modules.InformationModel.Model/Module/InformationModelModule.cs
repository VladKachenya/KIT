using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Infrastructure;
using BISC.Modules.InformationModel.Infrastucture;
using BISC.Modules.InformationModel.Model.Serializers.DataTypeTemplates;

namespace BISC.Modules.InformationModel.Model.Module
{
    public class InformationModelModule : IAppModule
    {
        private readonly IInjectionContainer _injectionContainer;

        public InformationModelModule(IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
        }

        public void Initialize()
        {

            _injectionContainer.RegisterType<DataTypeTemplatesSerializer>();

            _injectionContainer.ResolveType<IModelElementsRegistryService>()
                .RegisterModelElement(_injectionContainer.ResolveType<DataTypeTemplatesSerializer>(),InfoModelKeys.DataTypeTemplateKeys.DataTypeTemplatesModelItemKey);
        }
    }
}