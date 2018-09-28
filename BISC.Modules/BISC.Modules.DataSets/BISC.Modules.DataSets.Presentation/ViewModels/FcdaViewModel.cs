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
        public string FullName => _model.ToString();
        public Brush TypeColorBrush => Brushes.Tan;

        public IFcda GetModel()
        {
            return _model;
        }

        public void SetModel(IFcda model)
        {
            _model = model;
        }
       
    }
}
