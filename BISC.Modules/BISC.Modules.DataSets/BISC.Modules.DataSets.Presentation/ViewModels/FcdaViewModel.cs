using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.ViewModels;
using BISC.Modules.DataSets.Infrastructure.ViewModels.Base;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace BISC.Modules.DataSets.Presentation.ViewModels
{
    public class FcdaViewModel :ComplexViewModelBase, IFcdaViewModel
    {
        #region private filds
        private IFcda _model;


        #endregion

        #region C-tor
        public FcdaViewModel( )
        {
        }
        #endregion

        #region private methods
       
        #endregion

        #region Implementation of IFcdaViewModel
        public string Name
        {
            get => _model.DoName;
        }

        public string ElementName => _model.ElementName;
        public string FullName
        {
            get
            {
                if(_model.DaName == null)
                    return string.Format("{0}/{1}{2}.{3}[{4}]", _model.LnClass, _model.Prefix, _model.LdInst, _model.DoName, _model.Fc);
                else
                    return string.Format("{0}/{1}{2}.{3}.{4}[{5}]", _model.LnClass, _model.Prefix, _model.LdInst, _model.DoName, _model.DaName, _model.Fc);
            }
        }
        public Brush TypeColorBrush => new SolidColorBrush( Color.FromRgb(89, 89, 210));

        public IFcda GetModel()
        {
            return _model;
        }

        public void SetModel(IFcda model)
        {
            _model = model ?? throw new NullReferenceException();
        }

        public bool IsEditeble
        {
            get
            {
                if(_model.ParentModelElement != null)
                    return (_model.ParentModelElement as IDataSet).IsDynamic;
                return true;
            }
        }

        #endregion

            #region Implementation of IFcdaViewModel
        public ICommand DeleteFcdaCommand { get; }
        #endregion


    }
}
