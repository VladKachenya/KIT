using BISC.Infrastructure.Global.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Model.Model;
using BISC.Modules.Gooses.Model.Model.Matrix;
using BISC.Modules.Gooses.Presentation.Interfaces;
using BISC.Modules.Gooses.Presentation.Interfaces.Factories;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix.Rows;

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

        public List<IGooseRowViewModel> CreateGooseFtpOnlyRowsViewModel(List<IGooseRowFtpEntity> gooseRowFtpEntities, GooseControlBlockViewModel parent)
        {
            List<IGooseRowViewModel> gooseRowViewModels = new List<IGooseRowViewModel>();
            IGooseRowViewModel validityGooseRowViewModel = new GooseRowViewModel();
            InitailizeColumns(validityGooseRowViewModel);

            validityGooseRowViewModel.GooseRowType = "Validity";

            foreach (var gooseRowFtpEntity in gooseRowFtpEntities)
            {
                if (gooseRowFtpEntity is IGooseRowQualityFtpEntity qualityFtpEntity)
                {
                    if (qualityFtpEntity.IsValiditySelected)
                    {
                        validityGooseRowViewModel.SelectableValueViewModels[qualityFtpEntity.BitIndex - 1]
                            .SelectedValue = true;
                    }
                    gooseRowViewModels.Add(BuildQualityRowViewModel(qualityFtpEntity, parent, true));
                }
                else
                {
                    IGooseRowViewModel stateGooseRowViewModel = new GooseRowViewModel();
                    stateGooseRowViewModel.GooseRowType = "State";
                    InitailizeColumns(stateGooseRowViewModel);

                    stateGooseRowViewModel.SelectableValueViewModels[gooseRowFtpEntity.BitIndex - 1]
                        .SelectedValue = true;
                    stateGooseRowViewModel.RowName =
                        parent.AppId + " [" + gooseRowFtpEntity.NumberOfFcdaInDataSetOfGoose + "] (" + stateGooseRowViewModel.GooseRowType + ")";
                    gooseRowViewModels.Add(stateGooseRowViewModel);
                    stateGooseRowViewModel.NumberOfFcdaInDataSet = gooseRowFtpEntity.NumberOfFcdaInDataSetOfGoose;
                    //CheckQualityRowExisting(gooseRowFtpEntity, gooseRowFtpEntities,gooseRowViewModels,parent);
                }
            }

            gooseRowViewModels.Add(validityGooseRowViewModel);

            gooseRowViewModels.ForEach((model => model.Parent = parent));
            return gooseRowViewModels;
        }

        public List<IGooseRowViewModel> CreateGooseProjectRowsViewModel(List<IGooseRowFtpEntity> gooseRowFtpEntities, IDataSet relatedDataset,
            GooseControlBlockViewModel parent, IGooseInput gooseInput)
        {
            List<IGooseRowViewModel> gooseRowViewModels = new List<IGooseRowViewModel>();
           // CheckBlockRows(gooseRowFtpEntities, relatedDataset, new List<string>());
            IGooseRowViewModel validityGooseRowViewModel = new GooseRowViewModel();
            InitailizeColumns(validityGooseRowViewModel);
            validityGooseRowViewModel.GooseRowType = "Validity";
            
            foreach (var gooseRowFtpEntity in gooseRowFtpEntities)
            {
                if (gooseRowFtpEntity is IGooseRowQualityFtpEntity qualityFtpEntity)
                {
                    if (qualityFtpEntity.IsValiditySelected)
                    {
                        validityGooseRowViewModel.SelectableValueViewModels[qualityFtpEntity.BitIndex - 1]
                            .SelectedValue = true;
                    }

                    var qualityRowViewModel = BuildQualityRowViewModel(qualityFtpEntity, parent, true);
                    qualityRowViewModel.RelatedDataSet = relatedDataset;
                    gooseRowViewModels.Add(qualityRowViewModel);
                }
                else
                {
                    IGooseRowViewModel stateGooseRowViewModel = new GooseRowViewModel();
                    stateGooseRowViewModel.GooseRowType = "State";
                    InitailizeColumns(stateGooseRowViewModel);
                    stateGooseRowViewModel.NumberOfFcdaInDataSet = gooseRowFtpEntity.NumberOfFcdaInDataSetOfGoose;
                    stateGooseRowViewModel.SelectableValueViewModels[gooseRowFtpEntity.BitIndex - 1]
                        .SelectedValue = true;
                    stateGooseRowViewModel.RelatedDataSet = relatedDataset;
                    stateGooseRowViewModel.RowName =
                        parent.AppId + " [" + gooseRowFtpEntity.NumberOfFcdaInDataSetOfGoose + "] (" + stateGooseRowViewModel.GooseRowType + ")";
                    gooseRowViewModels.Add(stateGooseRowViewModel);

                    //CheckQualityRowExisting(gooseRowFtpEntity, gooseRowFtpEntities,gooseRowViewModels,parent);
                }
            }


            foreach (var externalGooseReference in gooseInput.ExternalGooseReferences)
            {
                var fcda = relatedDataset.FcdaList.FirstOrDefault((fcda1 =>
                    CompareFcdaAndExtRef(externalGooseReference, fcda1)));

                var indexOfFcda = relatedDataset.FcdaList.IndexOf(fcda);
                if (!gooseRowFtpEntities.Any((entity => entity.NumberOfFcdaInDataSetOfGoose == indexOfFcda)))
                {
                    if (externalGooseReference.DaName=="q")
                    {
                        IGooseRowViewModel qualityGooseRowViewModel = new GooseRowViewModel();
                        qualityGooseRowViewModel.GooseRowType = "Quality";
                        qualityGooseRowViewModel.NumberOfFcdaInDataSet = relatedDataset.FcdaList.IndexOf(fcda);
                            qualityGooseRowViewModel.RowName =
                            parent.AppId + " [" +fcda.DoName + "." + fcda.DaName + "] (" + qualityGooseRowViewModel.GooseRowType + ")";
                        InitailizeColumns(qualityGooseRowViewModel);
                        qualityGooseRowViewModel.RelatedDataSet = relatedDataset;

                        gooseRowViewModels.Add(qualityGooseRowViewModel);

                    }
                    else if(externalGooseReference.DaName == "stVal")
                    {
                        IGooseRowViewModel stateGooseRowViewModel = new GooseRowViewModel();
                        stateGooseRowViewModel.GooseRowType = "State";
                        InitailizeColumns(stateGooseRowViewModel);
                        stateGooseRowViewModel.NumberOfFcdaInDataSet = relatedDataset.FcdaList.IndexOf(fcda);
                        stateGooseRowViewModel.RelatedDataSet = relatedDataset;

                        stateGooseRowViewModel.RowName =
                            parent.AppId + " [" + fcda.DoName + "." + fcda.DaName + "] (" + stateGooseRowViewModel.GooseRowType + ")";
                        gooseRowViewModels.Add(stateGooseRowViewModel);

                        //CheckQualityRowExisting(gooseRowFtpEntity, gooseRowFtpEntities,gooseRowViewModels,parent);
                    }

                }

            }


            gooseRowViewModels.Add(validityGooseRowViewModel);
            gooseRowViewModels.ForEach((model => model.Parent = parent));
            return gooseRowViewModels;
        }

        private bool CompareFcdaAndExtRef(IExternalGooseRef externalGooseRef, IFcda fcda)
        {
            if (externalGooseRef.Prefix != fcda.Prefix) return false;
            if (externalGooseRef.DaName != fcda.DaName) return false;
            if (externalGooseRef.DoName != fcda.DoName) return false;
            if (externalGooseRef.LdInst != fcda.LdInst) return false;
            if (externalGooseRef.LnInst != fcda.LnInst) return false;
            if (externalGooseRef.LnClass != fcda.LnClass) return false;
            return true;
        }

        private void CheckBlockRows(List<IGooseRowFtpEntity> rowsForBlock, IDataSet relatedDataset, List<string> messagesList)
        {
            List<IGooseRowFtpEntity> rowsToRemove = new List<IGooseRowFtpEntity>();
            foreach (var gooseRow in rowsForBlock)
            {
                var relatedFcda = relatedDataset.FcdaList[gooseRow.NumberOfFcdaInDataSetOfGoose];


                switch (relatedFcda.DaName)
                {
                    case "stVal":
                        if (!rowsForBlock.Any((row =>
                        {
                            var relatedFcdaForRow = relatedDataset.FcdaList[gooseRow.NumberOfFcdaInDataSetOfGoose];
                            return (relatedFcdaForRow.DaName == "q") && row.IndexOfGoose == gooseRow.IndexOfGoose;
                        })))
                        {
                            messagesList.Add(
                                $"Элемент состояния GOOSE.Dataset  {relatedFcda.DoName + "." + relatedFcda.DaName} не дублируется качеством");
                            rowsToRemove.Add(gooseRow);
                        }

                        break;
                    case "q":
                        if (!rowsForBlock.Any((row =>
                        {
                            var relatedFcdaForRow = relatedDataset.FcdaList[gooseRow.NumberOfFcdaInDataSetOfGoose];
                            return (relatedFcdaForRow.DaName == "stVal") && row.IndexOfGoose == gooseRow.IndexOfGoose;
                        })))
                        {
                            messagesList.Add(
                                $"Элемент качества GOOSE.Dataset {relatedFcda.DoName + "." + relatedFcda.DaName} не дублируется состоянием");
                            rowsToRemove.Add(gooseRow);
                        }

                        break;
                }
            }

            rowsToRemove.ForEach((row => rowsForBlock.Remove(row)));
        }


        private IGooseRowViewModel BuildQualityRowViewModel(IGooseRowQualityFtpEntity qualityFtpEntity, GooseControlBlockViewModel parent, bool value)
        {
            IGooseRowViewModel qualityGooseRowViewModel = new GooseRowViewModel();
            qualityGooseRowViewModel.GooseRowType = "Quality";

            qualityGooseRowViewModel.RowName =
                parent.AppId + " [" + qualityFtpEntity.NumberOfFcdaInDataSetOfGoose + "] (" + qualityGooseRowViewModel.GooseRowType + ")";
            InitailizeColumns(qualityGooseRowViewModel);
            qualityGooseRowViewModel.NumberOfFcdaInDataSet = qualityFtpEntity.NumberOfFcdaInDataSetOfGoose;
            qualityGooseRowViewModel.SelectableValueViewModels[qualityFtpEntity.BitIndex - 1]
                .SelectedValue = value;
            return qualityGooseRowViewModel;
        }


        //private void CheckQualityRowExisting(IGooseRowFtpEntity stateRowFtpEntity,
        //    List<IGooseRowFtpEntity> gooseRowFtpEntities, List<IGooseRowViewModel> gooseRowViewModels,
        //    GooseControlBlockViewModel parent)
        //{
        //    var entitiesWithSameBit =
        //        gooseRowFtpEntities.Where((entity => entity.BitIndex == stateRowFtpEntity.BitIndex)).ToList();
        //    if (!entitiesWithSameBit.Any((entity => entity is IGooseRowQualityFtpEntity)))
        //    {
        //        gooseRowViewModels.Add(BuildQualityRowViewModel(
        //            new GooseRowQualityFtpEntity()
        //            {
        //                BitIndex = stateRowFtpEntity.BitIndex,
        //                IndexOfGoose = stateRowFtpEntity.IndexOfGoose,
        //                IsValiditySelected = false
        //            }, parent,false));
        //    }
        //}



        private void InitailizeColumns(IGooseRowViewModel gooseRowViewModel)
        {
            for (int i = 0; i < 64; i++)
            {
                ISelectableValueViewModel selectableValueViewModel = StaticContainer.CurrentContainer.ResolveType<ISelectableValueViewModel>();
                // selectableValueViewModel.SelectedValue = columnIndexes.Any((columnIndex => columnIndex == i));
                selectableValueViewModel.ColumnNumber = i;
                selectableValueViewModel.Parent = gooseRowViewModel;
                selectableValueViewModel.ToolTip = gooseRowViewModel.RowName + "     " + (i + 1);
                gooseRowViewModel.SelectableValueViewModels.Add(selectableValueViewModel);


            }
        }
        #endregion
    }
}
