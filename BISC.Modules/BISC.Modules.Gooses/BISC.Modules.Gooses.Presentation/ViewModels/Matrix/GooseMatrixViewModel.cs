using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.FTP.Infrastructure.Serviсes;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Modules.Gooses.Model.Model.Matrix;
using BISC.Modules.Gooses.Presentation.Events;
using BISC.Modules.Gooses.Presentation.FileParsers;
using BISC.Modules.Gooses.Presentation.Interfaces;
using BISC.Presentation.Infrastructure.Factories;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Matrix
{
    public class GooseMatrixViewModel : NavigationViewModelBase
    {
        private readonly IGoosesModelService _goosesModelService;
        private readonly IBiscProject _biscProject;
        private readonly IDatasetModelService _datasetModelService;
        private readonly Func<GooseControlBlockViewModel> _gooseControlBlockViewModelFunc;
        private readonly IGlobalEventsService _globalEventsService;
        private readonly IUserNotificationService _userNotificationService;
        private readonly IDeviceFileWritingServices _deviceFileWritingServices;
      //  private readonly ResultFileParser _resultFileParser;
        private IDevice _device;

        public ICommand SaveFtpCommand { get; }
        public ObservableCollection<GooseControlBlockViewModel> GooseControlBlockViewModels { get; }

        public GooseMatrixViewModel(IGoosesModelService goosesModelService, IBiscProject biscProject,
            IDatasetModelService datasetModelService, Func<GooseControlBlockViewModel> gooseControlBlockViewModelFunc,
            IGlobalEventsService globalEventsService,IUserNotificationService userNotificationService,ICommandFactory commandFactory,
            IDeviceFileWritingServices deviceFileWritingServices
      //      ,ResultFileParser resultFileParser
            )
        {
            _goosesModelService = goosesModelService;
            _biscProject = biscProject;
            _datasetModelService = datasetModelService;
            _gooseControlBlockViewModelFunc = gooseControlBlockViewModelFunc;
            _globalEventsService = globalEventsService;
            _userNotificationService = userNotificationService;
            _deviceFileWritingServices = deviceFileWritingServices;
         //   _resultFileParser = resultFileParser;
            GooseControlBlockViewModels = new ObservableCollection<GooseControlBlockViewModel>();
            MessagesList = new ObservableCollection<string>();
            _globalEventsService.Subscribe<SelectableBoxEventArgs>(SelectableBoxSelected);
            SaveFtpCommand = commandFactory.CreatePresentationCommand(OnSaveFtp);

        }

        private void OnSaveFtp()
        {
      //    var str=  _resultFileParser.GetFileStringFromGooseModel(GooseControlBlockViewModels);
       //     _deviceFileWritingServices.WriteFileStringInDevice(_device.Ip, new List<string>() {str},
        //        new List<string>() {"GOOSERE.CFG"});
        }

        #region Overrides of NavigationViewModelBase

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _isInitialized = false;
            GooseControlBlockViewModels.Clear();
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>("IED");
            var gooseControlBlocksSubscribed = _goosesModelService.GetGooseControlsSubscribed(_device, _biscProject.MainSclModel);
            IGooseMatrix gooseMatrix = _goosesModelService.GetGooseMatrixForDevice(_device);
            foreach (var gooseControlBlockSubscribed in gooseControlBlocksSubscribed)
            {
                GooseControlBlockViewModel gooseControlBlockViewModel = _gooseControlBlockViewModelFunc();
                gooseControlBlockViewModel.AppId = gooseControlBlockSubscribed.Item2.AppId;
                gooseControlBlockViewModel.Name = gooseControlBlockSubscribed.Item2.Name;
                gooseControlBlockViewModel.DataSetName = gooseControlBlockSubscribed.Item2.DataSet;
            //    gooseControlBlockViewModel.GoCbReference= gooseControlBlockSubscribed.Item1.Name+ gooseControlBlockSubscribed.Item1.
                    
                  //  MR771N127LD0 / LLN0$GO$gcbIn
                      var dataSet = _datasetModelService.GetAllDataSetOfDevice(gooseControlBlockSubscribed.Item1).FirstOrDefault((set => set.Name == gooseControlBlockSubscribed.Item2.DataSet));

                   var input = _goosesModelService.GetGooseInputsOfDevice(_device).FirstOrDefault();
                if (input == null) break;
                List<IGooseRow> rowsForBlock = new List<IGooseRow>();
                foreach (var externalGooseReference in input.ExternalGooseReferences)
                {
                    IGooseRow relatedGooseRow = GetGooseRowForRef(externalGooseReference, gooseMatrix,dataSet);

                    if(relatedGooseRow==null)continue;
                    if (externalGooseReference.DaName == "q" || externalGooseReference.DaName == "stVal")
                    {
                        rowsForBlock.Add(relatedGooseRow);
                    }
                    else
                    {
                        MessagesList.Add($"Элемент GOOSE.Dataset {externalGooseReference.AsString()} не был принят");
                    }
                }

                CheckBlockRows(rowsForBlock);
                if(rowsForBlock.Count==0)continue;
                var validityRowForBlock = GetValidityGooseRow(gooseMatrix, gooseControlBlockSubscribed.Item2.AppId);

                rowsForBlock.Add(validityRowForBlock);
                gooseControlBlockViewModel.SetRows(rowsForBlock);
                
                GooseControlBlockViewModels.Add(gooseControlBlockViewModel);
                InitDictionary();

            }

            _isInitialized = true;
            Validate();
            base.OnNavigatedTo(navigationContext);
        }

        public ObservableCollection<string> MessagesList { get; }


        private void CheckBlockRows(List<IGooseRow> rowsForBlock)
        {
            List<IGooseRow> rowsToRemove = new List<IGooseRow>();
            foreach (var gooseRow in rowsForBlock)
            {
                var part1 = gooseRow.ReferencePath.Split('.').First();
                var part2 = gooseRow.ReferencePath.Split('.').Last();
                switch (part2)
                {
                    case "stVal":
                        if (!rowsForBlock.Any((row =>
                            row.ReferencePath.Split('.').Last() == "q" && row.ReferencePath.Split('.').First() == part1)))
                        {
                            MessagesList.Add($"Элемент состояния GOOSE.Dataset  {gooseRow.Signature} не дублируется качеством");
                            rowsToRemove.Add(gooseRow);
                        }

                        break;
                    case "q":
                        if (!rowsForBlock.Any((row =>
                            row.ReferencePath.Split('.').Last() == "stVal" && row.ReferencePath.Split('.').First() == part1)))
                        {
                            MessagesList.Add($"Элемент качества GOOSE.Dataset  {gooseRow.Signature} не дублируется состоянием");
                            rowsToRemove.Add(gooseRow);
                        }

                        break;
                }
            }

            rowsToRemove.ForEach((row => rowsForBlock.Remove(row)));
        }

        private IGooseRow GetGooseRowForRef(IExternalGooseRef externalGooseRef, IGooseMatrix gooseMatrix,IDataSet dataset)
        {
            foreach (var gooseRow in gooseMatrix.GooseRows)
            {
                if (gooseRow.ReferencePath == externalGooseRef.AsString())
                {
                    return gooseRow;
                }
            }
            int fcdaNum = -1;
            foreach (var fcda in dataset.FcdaList)
            {
                if (CompareFcdaAndExtRef(externalGooseRef, fcda))
                {
                    fcdaNum = dataset.FcdaList.IndexOf(fcda);
                    break;
                }
            }
            if (fcdaNum == -1)
            {
                return null;
            }
            string type = externalGooseRef.DaName == "q" ? "Quality" : externalGooseRef.DaName == "stVal" ? "State" : "Unknown";
            return new GooseRow() {NumberOfFcdaInDataSetOfGoose = fcdaNum,ReferencePath = externalGooseRef.AsString(), Signature = externalGooseRef.AsString(), ValueList = new bool[64].ToList(), GooseRowType = type };

        }

        protected override void OnNavigatedFrom(BiscNavigationContext navigationContext)
        {
            base.OnNavigatedFrom(navigationContext);
        }

        private IGooseRow GetValidityGooseRow(IGooseMatrix gooseMatrix, string gooseBlockName)
        {
            foreach (var gooseRow in gooseMatrix.GooseRows)
            {
                if (gooseRow.ReferencePath == gooseBlockName && gooseRow.GooseRowType == "Validity")
                {
                    return gooseRow;
                }
            }
            return new GooseRow() { ReferencePath = gooseBlockName, Signature = gooseBlockName + ".Validity", ValueList = new bool[64].ToList(), GooseRowType = "Validity" };

        }

        private bool _isInitialized = false;

        private void SelectableBoxSelected(SelectableBoxEventArgs obj)
        {
            if (!_isInitialized) return;
            if (obj.IsFocused) return;
            Validate(obj.SelectableValueViewModel);
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

        private void InitDictionary()
        {
            _columnSelectableValueViewModelsDictionary = new Dictionary<int, List<ISelectableValueViewModel>>();
            for (int i = 0; i < 64; i++)
            {
                _columnSelectableValueViewModelsDictionary.Add(i, new List<ISelectableValueViewModel>());
                foreach (var gooseControlBlock in GooseControlBlockViewModels)
                {
                    foreach (var gooseRowViewModel in gooseControlBlock.GooseRowViewModels)
                    {
                        _columnSelectableValueViewModelsDictionary[i]
                            .Add(gooseRowViewModel.SelectableValueViewModels[i]);
                    }
                }
            }
        }



        private Dictionary<int, List<ISelectableValueViewModel>> _columnSelectableValueViewModelsDictionary =
            new Dictionary<int, List<ISelectableValueViewModel>>();

        public void Validate(ISelectableValueViewModel initiatorSelectableValueViewModel = null)
        {

            if (initiatorSelectableValueViewModel == null)
            {
                ValidateStates();
                ValidateQualities();
            }
            else
            {
                if (initiatorSelectableValueViewModel.Parent.Model.GooseRowType == "State")
                {
                    ValidateStates();
                }
                if (initiatorSelectableValueViewModel.Parent.Model.GooseRowType == "Quality")
                {
                    ValidateQualities();
                }
            }
            foreach (var gooseControlBlock in GooseControlBlockViewModels)
            {
                foreach (var gooseRowViewModel in gooseControlBlock.GooseRowViewModels)
                {
                    if (gooseRowViewModel.Model.GooseRowType == "Validity")
                    {
                        gooseRowViewModel.SelectableValueViewModels.ForEach((model => model.IsSelectingEnabled = true));
                    }
                }
            }
        }
        



        private void ValidateStates()
        {
            foreach (var gooseControlBlock in GooseControlBlockViewModels)
            {


                foreach (var gooseRowViewModel in gooseControlBlock.GooseRowViewModels)
                {
                    if (gooseRowViewModel.Model.GooseRowType == "State")
                    {

                        gooseRowViewModel.SelectableValueViewModels.ForEach(
                            (model => //енаблит все
                            {
                                model.IsSelectingEnabled = true;
                            }));

                        var selectedBox =
                            gooseRowViewModel.SelectableValueViewModels.FirstOrDefault((model => model.SelectedValue));
                        if (selectedBox != null)
                        {
                            gooseRowViewModel.SelectableValueViewModels.ForEach(
                                (model => //дизаблит всю строку кроме выбранного элемента
                                {
                                    if (model != selectedBox)
                                    {
                                        model.IsSelectingEnabled = false;
                                    }
                                }));

                        }
                    }
                }

                for (int i = 0; i < 64; i++)
                {
                    var selectedValueInColumn =
                        _columnSelectableValueViewModelsDictionary[i].FirstOrDefault((model =>
                        {
                            if ((model.Parent.Model.GooseRowType != "State")) return false;
                            return model.SelectedValue;
                        }));

                    if (selectedValueInColumn != null)
                    {
                        _columnSelectableValueViewModelsDictionary[i].ForEach((model =>
                        {
                            if ((model != selectedValueInColumn) && ((model.Parent.Model.GooseRowType == "State")))
                            {
                                model.IsSelectingEnabled = false;
                            }
                        }));

                    }
                }
            }
        }


        private void ValidateQualities()
        {
            foreach (var gooseControlBlock in GooseControlBlockViewModels)
            {


                foreach (var gooseRowViewModel in gooseControlBlock.GooseRowViewModels)
                {
                    if (gooseRowViewModel.Model.GooseRowType == "Quality")
                    {

                        gooseRowViewModel.SelectableValueViewModels.ForEach(
                            (model => //енаблит все
                            {
                                model.IsSelectingEnabled = true;
                            }));

                        var selectedBox =
                            gooseRowViewModel.SelectableValueViewModels.FirstOrDefault((model => model.SelectedValue));
                        if (selectedBox != null)
                        {
                            gooseRowViewModel.SelectableValueViewModels.ForEach(
                                (model => //дизаблит всю строку кроме выбранного элемента
                                {
                                    if (model != selectedBox)
                                    {
                                        model.IsSelectingEnabled = false;
                                    }
                                }));

                        }
                    }
                }

                for (int i = 0; i < 64; i++)
                {
                    var selectedValueInColumn =
                        _columnSelectableValueViewModelsDictionary[i].FirstOrDefault((model =>
                        {
                            if ((model.Parent.Model.GooseRowType != "Quality")) return false;
                            return model.SelectedValue;
                        }));

                    if (selectedValueInColumn != null)
                    {
                        _columnSelectableValueViewModelsDictionary[i].ForEach((model =>
                        {
                            if ((model != selectedValueInColumn) && ((model.Parent.Model.GooseRowType == "Quality")))
                            {
                                model.IsSelectingEnabled = false;
                            }
                        }));

                    }
                }
            }
        }
        

        #region Overrides of ViewModelBase

        

        protected override void OnDisposing()
        {
            foreach (GooseControlBlockViewModel gooseControlBlockViewModel in GooseControlBlockViewModels)
            {
                gooseControlBlockViewModel.Dispose();
            }
            _globalEventsService.Unsubscribe<SelectableBoxEventArgs>(SelectableBoxSelected);

            base.OnDisposing();
        }

        #endregion

        #endregion
    }
}
