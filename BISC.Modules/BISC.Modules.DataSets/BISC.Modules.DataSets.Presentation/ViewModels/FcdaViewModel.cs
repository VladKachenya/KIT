using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.ViewModels;
using BISC.Modules.DataSets.Infrastructure.ViewModels.Base;
using BISC.Modules.DataSets.Presentation.HelperEntites;
using BISC.Presentation.BaseItems.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace BISC.Modules.DataSets.Presentation.ViewModels
{
    public class FcdaViewModel : ComplexViewModelBase, IFcdaViewModel
    {
        #region private string constants


        #endregion

        #region private filds

        private IFcda _model;
        private FcHelperEntity _sellectedFc;

        #endregion

        #region C-tor

        public FcdaViewModel()
        {
        }

        #endregion

        #region private methods

        #endregion

        #region Implementation of IDataSetElementBaseViewModel<IFcda>

        public string ElementName => _model.ElementName;

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
            set { }
        }

        #endregion

        #region Implementation of IFunctionalConstrainter


        public ObservableCollection<FcHelperEntity> FcCollection { get; protected set; }

        public void SetFcCollection(List<FcHelperEntity> fcCollection)
        {
            FcCollection = new ObservableCollection<FcHelperEntity>(fcCollection);
            _sellectedFc = fcCollection.FirstOrDefault(el => el.Fc == _model.Fc);
        }

        public FcHelperEntity SellectedFc
        {
            get => _sellectedFc;
            set
            {
                SetProperty(ref _sellectedFc, value);
                ParentWeiger.Weigh();
            }
        }

        #endregion


        #region Implementation of IFcdaViewModel

        public IFcda GetFcda()
        {
            _model.Fc = SellectedFc.Fc;
            return _model;
        }

        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(_model.DaName))
                {
                    return $"{_model.LdInst}/{_model.Prefix + _model.LnClass + _model.LnInst}.{_model.DoName}";
                }
                else
                {
                    return
                        $"{_model.LdInst}/{_model.Prefix + _model.LnClass + _model.LnInst}.{_model.DoName}.{_model.DaName}";
                }
            }
        }

        public IWeigher ParentWeiger { get; set; }

        #endregion
    }
}