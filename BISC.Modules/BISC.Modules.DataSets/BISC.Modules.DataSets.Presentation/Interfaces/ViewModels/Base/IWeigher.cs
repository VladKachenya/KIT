namespace BISC.Modules.DataSets.Infrastructure.ViewModels.Base
{
    public interface IWeigher
    {
        int Weight { get; }
        void Weigh();
        int MaxSizeFcdaList { get; }
        bool CanSetWeight(int addedWeight, int removeWeight = 0);
    }
}