using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.ViewModels;
using BISC.Presentation.BaseItems.ViewModels;
using System;
using System.Windows.Input;
using System.Windows.Media;

namespace BISC.Modules.DataSets.Presentation.ViewModels
{
    public class FcdaViewModel : ComplexViewModelBase, IFcdaViewModel
    {
        #region private filds
        private IFcda _model;


        #endregion

        #region C-tor
        public FcdaViewModel()
        {
        }
        #endregion

        #region private methods

        #endregion

        #region Implementation of IFcdaViewModel
        public int Weight { get; protected set; }
        public string Name
        {
            get => _model.DoName;
        }

        public string ElementName => _model.ElementName;
        public string FullName
        {
            get
            {
                if (_model.DaName == null)
                {
                    return $"{_model.LdInst}/{_model.Prefix + _model.LnClass + _model.LnInst }.{_model.DoName} [{_model.Fc}]";
                }
                else
                {
                    return $"{_model.LdInst}/{_model.Prefix + _model.LnClass + _model.LnInst }.{_model.DoName}.{_model.DaName} [{_model.Fc}]";
                }
            }
        }
        public Brush TypeColorBrush => new SolidColorBrush(Color.FromRgb(89, 89, 210));

        public void SetModel(IFcda model)
        {
            _model = model ?? throw new NullReferenceException();
        }

        public bool IsEditeble
        {
            get
            {
                if (_model.ParentModelElement != null)
                {
                    return (_model.ParentModelElement as IDataSet).IsDynamic;
                }

                return true;
            }
            set
            {

            }
        }

        public void SetWeight(int weight)
        {
            Weight = weight;
        }

        #endregion

        #region Implementation of IFcdaViewModel
        public ICommand DeleteFcdaCommand { get; }
        public IFcda GetFcda()
        {
            return _model;
        }

        #endregion


    }
}
