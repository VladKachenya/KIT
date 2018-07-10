namespace BISC.Modules.Device.Presentation.Interfaces.Factories
{
    public interface IFileViewModelFactory
    {
        IFileViewModel CreateFileViewModel(string fullFilePath);

    }
}