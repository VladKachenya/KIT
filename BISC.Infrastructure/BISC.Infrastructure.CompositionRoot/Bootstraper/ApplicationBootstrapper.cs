using BISC.Infrastructure.CompositionRoot.Ioc;
using BISC.Infrastructure.CompositionRoot.Services;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Global.Module;
using BISC.Model.Iec61850Ed2.Module;
using BISC.Modules.Connection.MMS.Module;
using BISC.Modules.Connection.Model.Module;
using BISC.Modules.Connection.Presentation.Module;
using BISC.Modules.DataSets.Model.Module;
using BISC.Modules.DataSets.Presentation.Module;
using BISC.Modules.Device.Model.Module;
using BISC.Modules.Device.Presentation.Module;
using BISC.Modules.FTP.FTPConnection.Module;
using BISC.Modules.Gooses.Model.Module;
using BISC.Modules.Gooses.Presentation.Module;
using BISC.Modules.InformationModel.Model.Module;
using BISC.Modules.InformationModel.Presentation.Module;
using BISC.Modules.Logging.Infrastructure.Module;
using BISC.Modules.Reports.Model.Module;
using BISC.Modules.Reports.Presentation.Module;
using BISC.Presentation.BaseItems.Module;
using BISC.Presentation.Module;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Unity;


namespace BISC.Infrastructure.CompositionRoot.Bootstraper
{
    public class ApplicationBootstrapper : UnityBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.RegisterType<IInjectionContainer, InjectionContainer>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IGlobalEventsService, GlobalEventsService>(new ContainerControlledLifetimeManager());

            StaticContainer.SetContainer(Container.Resolve<IInjectionContainer>());
            RegisterModules();
            ResolveModules();

        }

   
        private void ResolveModules()
        {
            var modules = Container.ResolveAll(typeof(IAppModule));
            modules.ForEach((o => (o as IAppModule)?.Initialize()));
        }



        private void RegisterModules()
        {
            
            Container.RegisterType(typeof(IAppModule), typeof(GlobalModelModule),nameof(GlobalModelModule));
            Container.RegisterType(typeof(IAppModule), typeof(PresentationBaseItemsModule),nameof(PresentationBaseItemsModule));
            Container.RegisterType(typeof(IAppModule), typeof(LoggingModule), nameof(LoggingModule));

            Container.RegisterType(typeof(IAppModule), typeof(GlobalPresentationModule),nameof(GlobalPresentationModule));
            Container.RegisterType(typeof(IAppModule), typeof(InformationModelModule),nameof(InformationModelModule));

            Container.RegisterType(typeof(IAppModule), typeof(InformationModelPresentationModule),nameof(InformationModelPresentationModule));

            Container.RegisterType(typeof(IAppModule), typeof(DeviceModelModule), nameof(DeviceModelModule));
            Container.RegisterType(typeof(IAppModule), typeof(DevicePresentationModule), nameof(DevicePresentationModule));
            Container.RegisterType(typeof(IAppModule), typeof(ConnectionModelModule), nameof(ConnectionModelModule));
            Container.RegisterType(typeof(IAppModule), typeof(Iec61850Ed2Module), nameof(Iec61850Ed2Module));

            Container.RegisterType(typeof(IAppModule), typeof(ConnectionPresentationModule), nameof(ConnectionPresentationModule));
            Container.RegisterType(typeof(IAppModule), typeof(FTPConnectionModule), nameof(FTPConnectionModule));
            Container.RegisterType(typeof(IAppModule), typeof(ConnectionMmsModule), nameof(ConnectionMmsModule));
            Container.RegisterType(typeof(IAppModule), typeof(GoosesModelModule), nameof(GoosesModelModule));
            Container.RegisterType(typeof(IAppModule), typeof(GoosesPresentationModule), nameof(GoosesPresentationModule));
            Container.RegisterType(typeof(IAppModule), typeof(DatasetsModelModule), nameof(DatasetsModelModule));
            Container.RegisterType(typeof(IAppModule), typeof(DatasetsPresentationModule), nameof(DatasetsPresentationModule));
            Container.RegisterType(typeof(IAppModule), typeof(ReportsModelModule), nameof(ReportsModelModule));
            Container.RegisterType(typeof(IAppModule), typeof(ReportsPresentationModule), nameof(ReportsPresentationModule));
        }
    }
}