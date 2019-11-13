using BISC.Infrastructure.Global.IoC;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Presentation.Interfaces;
using BISC.Modules.Gooses.Presentation.Interfaces.Factories;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix.Rows;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<List<IGooseRowViewModel>> BuildGooseRowViewModels(GooseControlBlockViewModel parent, IGooseInputModelInfo gooseModelInfo, IGooseMatrixFtp gooseMatrix)
        {
            var res = CreateRowsViewModel(parent, gooseModelInfo);
            var relatedGoCb = gooseMatrix.GoCbFtpEntities.FirstOrDefault((entity =>
                entity.GoCbReference == gooseModelInfo.GocbRef));
            if (relatedGoCb != null)
            {
                var rows =
                    gooseMatrix.GooseRowFtpEntities.Where(entity => entity.IndexOfGoose == relatedGoCb.IndexOfGoose).ToList();
                rows.AddRange(gooseMatrix.GooseRowQualityFtpEntities.Where(entit => entit.IndexOfGoose == relatedGoCb.IndexOfGoose));
                await FillGooseRowViewModel(res, rows);

            }

            return res;
        }

        private async Task FillGooseRowViewModel(List<IGooseRowViewModel> parent, List<IGooseRowFtpEntity> gooseRowFtpEntities)
        {

            Task[] tasks = gooseRowFtpEntities.Select((entity => new Task((() =>
            {
                var s = Stopwatch.StartNew();

                parent.Find(el => el.NumberOfFcdaInDataSet == entity.NumberOfFcdaInDataSetOfGoose - 1).SelectableValueViewModels[entity.BitIndex - 1].SetValue(true);
                s.Stop();
                if (s.ElapsedMilliseconds > 500)
                {

                }
                if (entity is IGooseRowQualityFtpEntity validity && validity.IsValiditySelected)
                {
                    parent.First(el => el.GooseRowType == GooseKeys.GooseSubscriptionPresentationKeys.Validity).
                        SelectableValueViewModels[entity.BitIndex - 1].SetValue(true);
                }


            })))).ToArray();
            foreach (var task in tasks)
            {
                task.Start();
            }
            await Task.WhenAll(tasks);
        }


        public List<IGooseRowViewModel> CreateRowsViewModel(GooseControlBlockViewModel parent, IGooseInputModelInfo gooseInput)
        {
            List<IGooseRowViewModel> gooseRowViewModels = new List<IGooseRowViewModel>();

            foreach (var fcda in gooseInput.EmittingDataSet.Value.FcdaList)
            {
                var indexOfFcda = gooseInput.EmittingDataSet.Value.FcdaList.IndexOf(fcda);
                IGooseRowViewModel gooseRowViewModel = new GooseRowViewModel();
                gooseRowViewModel.DoiDataRef = $"{parent.GoCbReference.GoCbReference}/{fcda.LdInst}.{fcda.Prefix + fcda.LnClass + fcda.LnInst}.{fcda.DoName}";
                if (fcda.DaName == "q" && parent.IsConsigerTheQuality)
                {
                    gooseRowViewModel.GooseRowType = GooseKeys.GooseSubscriptionPresentationKeys.Quality;
                    gooseRowViewModel.NumberOfFcdaInDataSet = indexOfFcda;
                    gooseRowViewModel.RowName = $"{parent.DeviceName}{fcda.FullName}";
                    InitailizeColumns(gooseRowViewModel, parent.ColumnsName.Count);
                    gooseRowViewModel.RelatedDataSet = gooseInput.EmittingDataSet.Value;

                    gooseRowViewModels.Add(gooseRowViewModel);

                }
                else if (fcda.DaName == "stVal")
                {
                    gooseRowViewModel.GooseRowType = GooseKeys.GooseSubscriptionPresentationKeys.State;
                    gooseRowViewModel.NumberOfFcdaInDataSet = indexOfFcda;
                    gooseRowViewModel.RelatedDataSet = gooseInput.EmittingDataSet.Value;
                    gooseRowViewModel.RowName = $"{parent.DeviceName}{fcda.FullName}";
                    InitailizeColumns(gooseRowViewModel, parent.ColumnsName.Count);
                    gooseRowViewModels.Add(gooseRowViewModel);
                }
            }

            if (parent.IsConsigerTheQuality)
            {
                IGooseRowViewModel validityGooseRowViewModel = new GooseRowViewModel();
                validityGooseRowViewModel.RowName = "Enable goose monitoring";
                InitailizeColumns(validityGooseRowViewModel, parent.ColumnsName.Count);
                validityGooseRowViewModel.GooseRowType = GooseKeys.GooseSubscriptionPresentationKeys.Validity;
                gooseRowViewModels.Add(validityGooseRowViewModel);
                gooseRowViewModels.ForEach((model => model.Parent = parent));
            }
            return gooseRowViewModels;
        }



        private void InitailizeColumns(IGooseRowViewModel gooseRowViewModel, int columnsCount)
        {
            for (int i = 0; i < columnsCount; i++)
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
