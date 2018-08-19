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
using BISC.Modules.InformationModel.Model.Serializers.Model;

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
            var modelElementsRegistryService =_injectionContainer.ResolveType<IModelElementsRegistryService>();
            modelElementsRegistryService.RegisterModelElement(new DataTypeTemplatesSerializer(), InfoModelKeys.DataTypeTemplateKeys.DataTypeTemplatesModelItemKey);
            modelElementsRegistryService.RegisterModelElement(new LNodeTypeSerializer(), InfoModelKeys.DataTypeTemplateKeys.LNodeTypeModelItemKey);
            modelElementsRegistryService.RegisterModelElement(new BdaSerializer(), InfoModelKeys.DataTypeTemplateKeys.BDAItemKey);
            modelElementsRegistryService.RegisterModelElement(new DaSerializer(), InfoModelKeys.DataTypeTemplateKeys.DaItemKey);
            modelElementsRegistryService.RegisterModelElement(new DaTypeSerializer(), InfoModelKeys.DataTypeTemplateKeys.DaTypeItemKey);
            modelElementsRegistryService.RegisterModelElement(new DoTypeSerializer(), InfoModelKeys.DataTypeTemplateKeys.DOTypeModelItemKey);
            modelElementsRegistryService.RegisterModelElement(new DoSerializer(), InfoModelKeys.DataTypeTemplateKeys.DoItemKey);
            modelElementsRegistryService.RegisterModelElement(new EnumTypeSerializer(),  InfoModelKeys.DataTypeTemplateKeys.EnumTypeModelItemKey);
            modelElementsRegistryService.RegisterModelElement(new EnumValSerializer(), InfoModelKeys.DataTypeTemplateKeys.EnumValItemKey);
            modelElementsRegistryService.RegisterModelElement(new SdoSerializer(), InfoModelKeys.DataTypeTemplateKeys.SdoItemKey);

            modelElementsRegistryService.RegisterModelElement(new LDeviceSerializer(), InfoModelKeys.ModelKeys.LDeviceKey);
            modelElementsRegistryService.RegisterModelElement(new LogicalNodeSerializer(), InfoModelKeys.ModelKeys.LogicalNodeKey);
            modelElementsRegistryService.RegisterModelElement(new LogicalNodeZeroSerializer(), InfoModelKeys.ModelKeys.LogicalNodeZeroKey);
            modelElementsRegistryService.RegisterModelElement(new SdiSerializer(), InfoModelKeys.ModelKeys.SdiKey);
            modelElementsRegistryService.RegisterModelElement(new DaiSerializer(), InfoModelKeys.ModelKeys.DaiKey);
            modelElementsRegistryService.RegisterModelElement(new DoiSerializer(), InfoModelKeys.ModelKeys.DoiKey);
            modelElementsRegistryService.RegisterModelElement(new ValSerializer(), InfoModelKeys.ModelKeys.ValKey);

        }
    }
}