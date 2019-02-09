using BISC.Presentation.BaseItems.ViewModels;

namespace BISC.Modules.DataSets.Presentation.HelperEntites
{
    public class FcHelperEntity: ViewModelBase
    {
        private bool _isSellecteble;

        public FcHelperEntity(string fc, int fcWeight)
        {
            Fc = fc;
            FcWeight = fcWeight;
        }
        public string Fc { get;}
        public int FcWeight { get; }

        public bool IsSellecteble
        {
            get => _isSellecteble;
            set => SetProperty(ref _isSellecteble, value, true);
        }
    }
}