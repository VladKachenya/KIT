using BISC.Presentation.Infrastructure.Factories;

namespace BISC.Presentation.Infrastructure.Services
{
    public interface ITreeItemViewModelFactoryRegistryService
    {
        bool GetIsFactoryRegistered(string key);
        void RegisterFactory(ITreeItemViewModelFactory treeItemViewModelFactory, string key);
        ITreeItemViewModelFactory GetFactoryByKey(string key);
    }
}