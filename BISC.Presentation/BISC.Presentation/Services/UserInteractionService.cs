using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Infrastructure.Global.Services;
using BISC.Presentation.Infrastructure.Keys;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Presentation.Services
{
    public class UserInteractionService : IUserInteractionService
    {
        private readonly INavigationService _navigationService;

        public UserInteractionService(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        #region Implementation of IUserInteractionService

        public async Task<int> ShowOptionToUser(string title, string message, List<string> options)
        {
            OperationResult<int> operationResult = new OperationResult<int>(1);

            await _navigationService.NavigateViewToGlobalRegion(
                KeysForNavigation.ViewNames.UserInteractionOptionsViewName,
                new BiscNavigationParameters().AddParameterByName("result", operationResult)
                    .AddParameterByName("message", message).AddParameterByName("title", title).AddParameterByName("options", options));


            return operationResult.Item;
        }

        #endregion
    }
}