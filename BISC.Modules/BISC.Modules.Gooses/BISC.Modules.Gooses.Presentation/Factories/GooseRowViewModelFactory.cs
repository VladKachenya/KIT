using BISC.Infrastructure.Global.IoC;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Presentation.Interfaces;
using BISC.Modules.Gooses.Presentation.Interfaces.Factories;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix.Rows;
using System.Collections.Generic;
using System.Linq;
using BISC.Modules.Gooses.Infrastructure.Keys;

namespace BISC.Modules.Gooses.Presentation.Factories
{
    public class GooseRowViewModelFactory : IGooseRowViewModelFactory
    {
        private readonly IInjectionContainer _container;

        public GooseRowViewModelFactory(IInjectionContainer container)
        {
            _container = container;
        }


        #region Implementation of IGooseRowViewModelFactory

        public List<IGooseRowViewModel> BuildGooseRowViewModels(GooseControlBlockViewModel parent, IGooseInputModelInfo gooseModelInfo, IGooseMatrixFtp gooseMatrix)
        {
            var res = CreateRowsViewModel(parent, gooseModelInfo);
            var relatedGoCb = gooseMatrix.GoCbFtpEntities.FirstOrDefault((entity =>
                entity.GoCbReference == gooseModelInfo.GocbRef));
            if (relatedGoCb != null)
            {
                var rows =
                    gooseMatrix.GooseRowFtpEntities.Where(entity => entity.IndexOfGoose == relatedGoCb.IndexOfGoose).ToList();
                rows.AddRange(gooseMatrix.GooseRowQualityFtpEntities.Where(entit => entit.IndexOfGoose == relatedGoCb.IndexOfGoose));
                FillGooseRowViewModel(ref res, rows);


            }

            return res;
        }

        private void FillGooseRowViewModel(ref List<IGooseRowViewModel> parent, List<IGooseRowFtpEntity> gooseRowFtpEntities)
        {
            foreach (var gooseRowFtpEntitie in gooseRowFtpEntities)
            {
                parent.First(el => el.NumberOfFcdaInDataSet == gooseRowFtpEntitie.NumberOfFcdaInDataSetOfGoose - 1).
                    SelectableValueViewModels[gooseRowFtpEntitie.BitIndex - 1].SelectedValue = true;
                if (gooseRowFtpEntitie is IGooseRowQualityFtpEntity validity && validity.IsValiditySelected)
                {
                    parent.First(el => el.GooseRowType == GooseKeys.GooseSubscriptionPresentationKeys.ValidityKey).
                        SelectableValueViewModels[gooseRowFtpEntitie.BitIndex - 1].
                        SelectedValue = true;
                }
            }
        }

       
        public List<IGooseRowViewModel> CreateRowsViewModel(GooseControlBlockViewModel parent, IGooseInputModelInfo gooseInput)
        {
            List<IGooseRowViewModel> gooseRowViewModels = new List<IGooseRowViewModel>();
            IGooseRowViewModel validityGooseRowViewModel = new GooseRowViewModel();
            validityGooseRowViewModel.RowName = $"{gooseInput.EmittingGooseControl.Value.Name} {GooseKeys.GooseSubscriptionPresentationKeys.ValidityKey}";

            InitailizeColumns(validityGooseRowViewModel);
            validityGooseRowViewModel.GooseRowType = GooseKeys.GooseSubscriptionPresentationKeys.ValidityKey;

            foreach (var fcda in gooseInput.EmittingDataSet.Value.FcdaList)
            {
                var indexOfFcda = gooseInput.EmittingDataSet.Value.FcdaList.IndexOf(fcda);

                if (fcda.DaName == "q")
                {
                    IGooseRowViewModel qualityGooseRowViewModel = new GooseRowViewModel();
                    qualityGooseRowViewModel.GooseRowType = GooseKeys.GooseSubscriptionPresentationKeys.QualityKey;
                    qualityGooseRowViewModel.NumberOfFcdaInDataSet = indexOfFcda;
                    qualityGooseRowViewModel.RowName =
                    parent.AppId + " [" + fcda.DoName + "." + fcda.DaName + "] (" + qualityGooseRowViewModel.GooseRowType + ")";
                    InitailizeColumns(qualityGooseRowViewModel);
                    qualityGooseRowViewModel.RelatedDataSet = gooseInput.EmittingDataSet.Value;

                    gooseRowViewModels.Add(qualityGooseRowViewModel);

                }
                else if (fcda.DaName == "stVal")
                {
                    IGooseRowViewModel stateGooseRowViewModel = new GooseRowViewModel();
                    stateGooseRowViewModel.GooseRowType = GooseKeys.GooseSubscriptionPresentationKeys.StateKey;
                    stateGooseRowViewModel.NumberOfFcdaInDataSet = indexOfFcda;
                    stateGooseRowViewModel.RelatedDataSet = gooseInput.EmittingDataSet.Value;

                    stateGooseRowViewModel.RowName =
                        parent.AppId + " [" + fcda.DoName + "." + fcda.DaName + "] (" + stateGooseRowViewModel.GooseRowType + ")";
                    InitailizeColumns(stateGooseRowViewModel);
                    gooseRowViewModels.Add(stateGooseRowViewModel);
                }
            }

            gooseRowViewModels.Add(validityGooseRowViewModel);
            gooseRowViewModels.ForEach((model => model.Parent = parent));
            return gooseRowViewModels;
        }

       

        private void InitailizeColumns(IGooseRowViewModel gooseRowViewModel)
        {
            for (int i = 0; i < 64; i++)
            {
                ISelectableValueViewModel selectableValueViewModel = StaticContainer.CurrentContainer.ResolveType<ISelectableValueViewModel>();
                selectableValueViewModel.ColumnNumber = i;
                selectableValueViewModel.Parent = gooseRowViewModel;
                selectableValueViewModel.ToolTip = gooseRowViewModel.RowName + "     " + (i + 1);
                gooseRowViewModel.SelectableValueViewModels.Add(selectableValueViewModel);


            }
        }
        #endregion
    }
}
