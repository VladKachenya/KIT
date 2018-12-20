using System.Collections.Generic;
using System.Threading.Tasks;

namespace BISC.Infrastructure.Global.Services
{
    public interface IUserInteractionService
    {
        Task<int> ShowOptionToUser(string title, string message, List<string> options, List<string> warnings = null);
        Task<int> ShowOptionToUser(string title, string message, List<string> options,string idOfDiologHost);

    }


}