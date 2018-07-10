using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Infrastructure;
using BISC.Presentation.Infrastructure.Tree;

namespace BISC.Presentation.Infrastructure.Factories
{
    public interface ITreeItemViewModelFactory
    {
        IMainTreeItem CreateTreeItem(IModelElement modelElement);
    }
}