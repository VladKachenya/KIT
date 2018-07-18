using System.Collections.Generic;
using System.Linq;
using Prism.Regions;

namespace BISC.Presentation.Infrastructure.Navigation
{

    public class BiscNavigationParameters : List<BiscNavigationParameter>
    {
        public static BiscNavigationParameters FromNavigationParameters(NavigationParameters navigationParameters)
        {
            BiscNavigationParameters biscNavigationParameters = new BiscNavigationParameters();
            foreach (var keyValuePair in navigationParameters)
            {
                biscNavigationParameters.Add(new BiscNavigationParameter(keyValuePair.Key, keyValuePair.Value));
            }

            return biscNavigationParameters;
        }

        public NavigationParameters ToNavigationParameters()
        {
            NavigationParameters navigationParametersToRegion = new NavigationParameters();

            this?.ForEach((parameter =>
            {
                navigationParametersToRegion.Add(parameter.ParameterName, parameter.Parameter);
            }));
            return navigationParametersToRegion;
        }


        public T GetParameterByName<T>(string key)
        {
            var parameterResult =
                this.FirstOrDefault((parameter => parameter.ParameterName == key));
            if (parameterResult != null)
            {
                return (T) parameterResult.Parameter;
            }

            return default(T);
        }

        public BiscNavigationParameters AddParameterByName(string key, object parameter)
        {
            Add(new BiscNavigationParameter(key, parameter));
            return this;
        }
    }

    public class BiscNavigationParameter
    {
        public BiscNavigationParameter(string parameterName, object parameter)
        {
            ParameterName = parameterName;
            Parameter = parameter;
        }

        public string ParameterName { get; }
        public object Parameter { get; }

    }
}