using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Presentation.Infrastructure.Parameters;
using BISC.Presentation.Infrastructure.Services;
using Prism.Regions;

namespace BISC.Presentation.Services
{
   public class NavigationService: INavigationService
    {
        private readonly IRegionManager _regionManager;

        public NavigationService(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }


        #region Implementation of INavigationService

        public void NavigateViewToRegion(string viewName, string regionName, List<NavigationParameter> navigationParameters=null)
        {
            NavigationParameters navigationParametersToRegion=new NavigationParameters();
            navigationParameters?.ForEach((parameter =>
            {
                navigationParametersToRegion.Add(parameter.ParameterName,parameter.Parameter);
            }));
           _regionManager.RequestNavigate(regionName,new Uri(viewName,UriKind.Relative),navigationParametersToRegion);
        }

        #endregion
    }
}
