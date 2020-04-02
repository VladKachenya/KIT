using System;
using System.Collections.Generic;
using System.Linq;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Presentation.Interfaces.Factories;
using BISC.Modules.Gooses.Presentation.Interfaces.GooseSubscriptionLight;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix;

namespace BISC.Modules.Gooses.Presentation.Factories
{
    public class GooseMatrixLightViewModelFactory : IGooseMatrixLightViewModelFactory
    {
        private readonly Func<IGoInViewModel> _goInViewModelFunc;
        private readonly Func<IGooseDataReferenceViewModel> _gooseDataReferenceViewModelFunc;

        public GooseMatrixLightViewModelFactory(Func<IGoInViewModel> goInViewModelFunc, Func<IGooseDataReferenceViewModel> gooseDataReferenceViewModelFunc)
        {
            _goInViewModelFunc = goInViewModelFunc;
            _gooseDataReferenceViewModelFunc = gooseDataReferenceViewModelFunc;
        }


        public Tuple<List<IGoInViewModel>, List<IGooseDataReferenceViewModel>> CreateGooseMatrixLightViewModel(List<GooseControlBlockViewModel> gooseMatrix)
        {
            var goIns = new List<IGoInViewModel>();
            var gooseDataReferences = new List<IGooseDataReferenceViewModel>();
            var res = new Tuple<List<IGoInViewModel>, List<IGooseDataReferenceViewModel>>(goIns, gooseDataReferences);
            List<IGoInViewModel> result = new List<IGoInViewModel>();

            var defaultGooseDataRef = _gooseDataReferenceViewModelFunc();
            defaultGooseDataRef.DoiDataReference = "Нет";
            defaultGooseDataRef.DataSetReferenceQuality = "Нет";
            defaultGooseDataRef.DataSetReferenceState = "Нет";
            defaultGooseDataRef.DeviceName = "Нет";
            gooseDataReferences.Add(defaultGooseDataRef);
            int i = 0;
            var firstGooseControl = gooseMatrix.FirstOrDefault();
            if (firstGooseControl?.ColumnsName != null)
            {
                foreach (var columnName in firstGooseControl.ColumnsName)
                {
                    var goInViewModel = _goInViewModelFunc();
                    goInViewModel.Name = $"GoIn: {columnName}";
                    goInViewModel.Number = i;
                    goInViewModel.GooseDataReferenceViewModel = defaultGooseDataRef;
                    goIns.Add(goInViewModel);
                    i++;
                }
            }

            foreach (var gooseControlBlockViewModel in gooseMatrix)
            {
                foreach (var gooseRowViewModel in gooseControlBlockViewModel.GooseRowViewModels)
                {
                    if (gooseRowViewModel.GooseRowType == GooseKeys.GooseSubscriptionPresentationKeys.State)
                    {
                        var gooseDataReference = _gooseDataReferenceViewModelFunc();
                        gooseDataReference.DeviceName = gooseControlBlockViewModel.DeviceName;
                        gooseDataReference.GooseName = gooseControlBlockViewModel.GoCbReference.GoCbReference;
                        gooseDataReference.DoiDataReference = gooseRowViewModel.DoiDataRef;
                        gooseDataReference.DataSetReferenceState = gooseRowViewModel.RowName;
                        //Set value
                        var selectedValue =
                            gooseRowViewModel.SelectableValueViewModels.FirstOrDefault(sv => sv.SelectedValue);
                        if (selectedValue != null)
                        {
                            goIns[selectedValue.ColumnNumber].GooseDataReferenceViewModel = gooseDataReference;
                            goIns[selectedValue.ColumnNumber].EnableState = true;
                        }


                        var qualityRef = gooseControlBlockViewModel.GooseRowViewModels
                            .FirstOrDefault(row =>
                                row.DoiDataRef == gooseDataReference.DoiDataReference &&
                                row.GooseRowType == GooseKeys.GooseSubscriptionPresentationKeys.Quality);
                        if (qualityRef != null)
                        {
                            gooseDataReference.DataSetReferenceQuality = qualityRef.RowName;
                            if (selectedValue != null &&
                                qualityRef.SelectableValueViewModels[selectedValue.ColumnNumber].SelectedValue)
                            {
                                goIns[selectedValue.ColumnNumber].EnableQuality = true;
                                var enableGooseMonitoringRow =
                                    gooseControlBlockViewModel.GooseRowViewModels.FirstOrDefault(r =>
                                        r.GooseRowType == GooseKeys.GooseSubscriptionPresentationKeys.Validity);
                                if (enableGooseMonitoringRow != null && enableGooseMonitoringRow.SelectableValueViewModels[selectedValue.ColumnNumber].SelectedValue)
                                {
                                    goIns[selectedValue.ColumnNumber].EnableGooseMonitoring = true;
                                }
                            }
                        }
                        gooseDataReferences.Add(gooseDataReference);
                    }
                }
            }

            return res;
        }
    }
}