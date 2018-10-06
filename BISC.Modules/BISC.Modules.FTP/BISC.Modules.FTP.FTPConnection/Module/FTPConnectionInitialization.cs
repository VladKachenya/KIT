using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.FTP.Infrastructure.Serviсes;
using BISC.Presentation.Infrastructure.Factories;

namespace BISC.Modules.FTP.FTPConnection.Module
{
    public class FTPConnectionInitialization
    {
        private readonly IInjectionContainer _injectionContainer;

        public FTPConnectionInitialization(IUserInterfaceComposingService userInterfaceComposingService,
            ICommandFactory commandFactory, IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
            userInterfaceComposingService.AddGlobalCommand(commandFactory.CreatePresentationCommand(OnAddingFTPService, null), "FTP Сервис",null,true,false);
        }

        private void OnAddingFTPService()
        {
            _injectionContainer.ResolveType<IFTPAddingServise>().OpenFTPServiceView();
        }
    }
}
