namespace BISC.Model.Infrastructure.Project
{
    public interface ISclHeader
    {
        string Id { get; set; }
        string Version { get; set; }
        string Revision { get; set; }
    }
}