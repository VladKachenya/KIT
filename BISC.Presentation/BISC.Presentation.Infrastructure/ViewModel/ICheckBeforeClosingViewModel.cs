using System.Threading.Tasks;

namespace BISC.Presentation.Infrastructure.ViewModel
{
    public interface ICheckBeforeClosingViewModel
    {
        bool GetIsClosingAllowed();
    }
}