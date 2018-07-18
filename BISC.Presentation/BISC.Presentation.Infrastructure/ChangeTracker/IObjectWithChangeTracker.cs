namespace BISC.Presentation.Infrastructure.ChangeTracker
{
    public interface IObjectWithChangeTracker
    {
        IChangeTracker ChangeTracker { get; }
    }
}