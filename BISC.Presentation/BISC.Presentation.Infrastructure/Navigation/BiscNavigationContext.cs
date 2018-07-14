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
            return new NavigationContext(ServiceLocator.Current.GetInstance<IRegionNavigationService>(), Uri, BiscNavigationParameters.ToNavigationParameters());
        }

        public static BiscNavigationContext FromNavigationContext(NavigationContext navigationContext)
        {
            BiscNavigationContext biscNavigationContext = new BiscNavigationContext();
            biscNavigationContext.BiscNavigationParameters = BiscNavigationParameters.FromNavigationParameters(navigationContext.Parameters);
            biscNavigationContext.Uri = navigationContext.Uri;
            return biscNavigationContext;
        }


    }
}