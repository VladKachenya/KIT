using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.ViewModels;
using BISC.Modules.DataSets.Infrastructure.ViewModels.Base;
using BISC.Presentation.BaseItems.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BISC.Modules.DataSets.Presentation.ViewModels
{
    public class FcdaViewModel :ViewModelBase, IFcdaViewModel
    {
        #region private filds
        private IFcda _model;

        #endregion

        #region C-tor
        public FcdaViewModel(IFcda model)
        {
            _model = model ?? throw new Exception("Null referens exeption!");
        }
        #endregion

        #region Implementation of IFcdaViewModel
        public string Name
        {
            get => _model.DoName;
            set
            {
                _model.DoName= value;
                OnPropertyChanged();
            }
        }

        public string ElementName => _model.ElementName;
        public string FullName
        {
            get
            {
                if(_model.DaName == null)
                    return string.Format("{0}/{1}{2}.{3}", _model.LnClass, _model.Prefix, _model.LdInst, _model.DoName);
                else
                    return string.Format("{0}/{1}{2}.{3}.{4}", _model.LnClass, _model.Prefix, _model.LdInst, _model.DoName, _model.DaName);
            }
        }
        public Brush TypeColorBrush => new SolidColorBrush( Color.FromRgb(89, 89, 210));

        public IFcda GetModel()
        {
            return _model;
        }

        public void SetModel(IFcda model)
        {
            _model = model;
        }
        #endregion



    }
}
