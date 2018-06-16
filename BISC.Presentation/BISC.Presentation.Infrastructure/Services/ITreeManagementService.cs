using BISC.Presentation.Infrastructure.Tree;

namespace BISC.Presentation.Infrastructure.Services
{

    public class TreeItemDetails
    {
        public TreeItemDetails(string detailsViewName, object detailsDataContext)
        {
            DetailsViewName = detailsViewName;
            DetailsDataContext = detailsDataContext;
        }
        public string DetailsViewName { get; }
        public object DetailsDataContext { get; }
    }
    public interface ITreeManagementService
    {
        void AddTreeItem(IMainTreeItem treeItem, TreeItemDetails treeItemDetails);
    }
}