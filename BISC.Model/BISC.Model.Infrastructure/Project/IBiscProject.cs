namespace BISC.Model.Infrastructure.Project
{
    public interface IBiscProject:IModelElement
    {
        ISclModel MainSclModel { get; set; }
    }
}