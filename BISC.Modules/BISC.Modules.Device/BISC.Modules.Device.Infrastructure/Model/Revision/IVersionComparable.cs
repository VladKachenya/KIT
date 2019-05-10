namespace BISC.Modules.Device.Infrastructure.Model.Revision
{
    public interface IVersionComparable
    {
        int CompareVersionTo(int version, int subversion);
        int CompareVersionTo(IRevision revision);
    }
}