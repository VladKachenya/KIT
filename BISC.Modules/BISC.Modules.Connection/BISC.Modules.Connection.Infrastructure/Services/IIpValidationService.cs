using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Connection.Infrastructure.Services
{
    public interface IIpValidationService
    {
        bool IsSimplifiedIpAddress(string Address);

        bool IsExactFormIpAddress(string Address);
    }
}
