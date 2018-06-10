using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Presentation.Infrastructure.Parameters
{
    public class NavigationParameter
    {
        public NavigationParameter(string parameterName, object parameter)
        {
            ParameterName = parameterName;
            Parameter = parameter;
        }
        public string ParameterName { get; }
        public object Parameter { get; }

    }
}
