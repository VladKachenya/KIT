using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.DataSets.Presentation.ViewModels
{
    public class FcdaViewModel : IFcdaViewModel
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
        public string FullName => _model.Fc + _model.DoName + _model.DaName;
    }
}
