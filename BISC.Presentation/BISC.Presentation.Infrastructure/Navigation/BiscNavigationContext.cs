using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using Prism.Regions;

namespace BISC.Presentation.Infrastructure.Navigation
{
    public class BiscNavigationContext
    {
        public Uri Uri { get; set; }
        public BiscNavigationParameters BiscNavigationParameters { get; set; }

        public NavigationContext ToNavigationContext()
        {
            var result = new NavigationContext(ServiceLocator.Current.GetInstance<IRegionNavigationService>(), Uri,null);
            BiscNavigationParameters?.ForEach((parameter =>
            {
                result.Parameters.Add(parameter.ParameterName,parameter.Parameter);
            } ));
            return result;
        }

        public static BiscNavigationContext FromNavigationContext(NavigationContext navigationContext)
        {
            BiscNavigationContext biscNavigationContext = new BiscNavigationContext();
            if (navigationContext == null)
            {
                return null;
            }
            biscNavigationContext.BiscNavigationParameters = BiscNavigationParameters.FromNavigationParameters(navigationContext.Parameters);
            biscNavigationContext.Uri = navigationContext.Uri;
            return biscNavigationContext;
        }


    }
}