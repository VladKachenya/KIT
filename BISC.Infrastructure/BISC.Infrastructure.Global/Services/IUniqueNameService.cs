using System.Collections.Generic;

namespace BISC.Infrastructure.Global.Services
{
    public interface IUniqueNameService
    {
        string GetUniqueName(List<string> existingNames, string bodyOfName);

    }
}