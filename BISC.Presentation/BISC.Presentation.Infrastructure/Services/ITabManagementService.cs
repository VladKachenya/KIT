using System.Collections.Generic;

namespace BISC.Presentation.Infrastructure.Services
{
    public class NavigationParameter
    {
        public NavigationParameter(string parameterName, object parameter)
        {
            ParameterName = parameterName;
            Parameter = parameter;
        }
        public string ParameterName { get;  }
        public object Parameter { get; }

    }
    public interface ITabManagementService
    {
        void NavigateToTab(string viewName,List<NavigationParameter> navigationParameters,object owner);
        void CloseTabs(object owner);
    }
}