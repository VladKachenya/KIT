using BISC.Infrastructure.Global.Common;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Model.Model;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix.Entities;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Presentation.Infrastructure.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BISC.Modules.Gooses.Presentation.Commands
{
    public class GooseControlAssignmentSavingCommand : ISavingCommand
    {
        private readonly IGoosesModelService _goosesModelService;
        private readonly IDatasetModelService _datasetModelService;
        private readonly ISclCommunicationModelService _sclCommunicationModelService;
        private readonly IBiscProject _biscProject;
        private readonly IProjectService _projectService;
        private IDevice _device;
        private ObservableCollection<GooseControlBlockAssignmentItem> _gooseControlBlockAssignmentItems;

        public GooseControlAssignmentSavingCommand(IGoosesModelService goosesModelService, IDatasetModelService datasetModelService,
            ISclCommunicationModelService sclCommunicationModelService, IBiscProject biscProject, IProjectService projectService)
        {
            _goosesModelService = goosesModelService;
            _datasetModelService = datasetModelService;
            _sclCommunicationModelService = sclCommunicationModelService;
            _biscProject = biscProject;
            _projectService = projectService;
        }

        public void Initialize(ObservableCollection<GooseControlBlockAssignmentItem> gooseControlBlockAssignmentItems,
            IDevice device)
        {
            _device = device;
            _gooseControlBlockAssignmentItems = gooseControlBlockAssignmentItems;
        }

        #region Implementation of ISavingCommand

        public async Task<OperationResult<SavingCommandResultEnum>> SaveAsync()
        {
            var existingGooseMatrix = _goosesModelService.GetGooseMatrixFtpForDevice(_device);
            var prevoiusMatrixGoCbEntities = existingGooseMatrix.GoCbFtpEntities.ToList();
            existingGooseMatrix.GoCbFtpEntities.Clear();
            var indexesOfGooseToPreventDeleting = new List<int>();
            foreach (var gooseControlBlockAssignmentItem in _gooseControlBlockAssignmentItems)
            {
                if (gooseControlBlockAssignmentItem is GooseFtpOnlyAssignmentItem ftpOnlyGooseAssignmentItem)
                {
                    SaveGooseAssignmentWithNoRelatedDevice(ftpOnlyGooseAssignmentItem, prevoiusMatrixGoCbEntities, indexesOfGooseToPreventDeleting, existingGooseMatrix);
                }

                if (gooseControlBlockAssignmentItem is GooseInProjectAssignmentItem gooseInProjectAssignmentItem)
                {
                    SaveExistingGooseAssignment(gooseInProjectAssignmentItem, prevoiusMatrixGoCbEntities, indexesOfGooseToPreventDeleting, existingGooseMatrix);
                }



            }
            UpdateMatrixRows(prevoiusMatrixGoCbEntities, indexesOfGooseToPreventDeleting, existingGooseMatrix);
            _projectService.SaveCurrentProject();
            return new OperationResult<SavingCommandResultEnum>(SavingCommandResultEnum.SavedOk);
        }

        private void UpdateMatrixRows(List<IGoCbFtpEntity> prevoiusMatrixGoCbEntities,
            List<int> indexesOfGooseToPreventDeleting, IGooseMatrixFtp gooseMatrixFtp)
        {
            var prevRows = gooseMatrixFtp.GooseRowFtpEntities.ToList();
            var prevRowsQuality = gooseMatrixFtp.GooseRowQualityFtpEntities.ToList();

            gooseMatrixFtp.GooseRowFtpEntities.Clear();
            foreach (var index in indexesOfGooseToPreventDeleting)
            {
                var relatedRows = prevRows.Where((entity => entity.IndexOfGoose == index)).ToList();
                var relatedPrevEntity =
                    prevoiusMatrixGoCbEntities.FirstOrDefault((entity => entity.IndexOfGoose == index));
                var relatedEntity = gooseMatrixFtp.GoCbFtpEntities.FirstOrDefault((entity =>
                    entity.GoCbReference == relatedPrevEntity.GoCbReference));
                relatedRows.ForEach((entity => entity.IndexOfGoose = relatedEntity.IndexOfGoose));
                gooseMatrixFtp.GooseRowFtpEntities.AddRange(relatedRows);
            }

            gooseMatrixFtp.GooseRowQualityFtpEntities.Clear();

            foreach (var index in indexesOfGooseToPreventDeleting)
            {
                var relatedRows = prevRowsQuality.Where((entity => entity.IndexOfGoose == index)).ToList();
                var relatedPrevEntity =
                    prevoiusMatrixGoCbEntities.FirstOrDefault((entity => entity.IndexOfGoose == index));
                var relatedEntity = gooseMatrixFtp.GoCbFtpEntities.FirstOrDefault((entity =>
                    entity.GoCbReference == relatedPrevEntity.GoCbReference));
                relatedRows.ForEach((entity => entity.IndexOfGoose = relatedEntity.IndexOfGoose));
                gooseMatrixFtp.GooseRowQualityFtpEntities.AddRange(relatedRows);
            }

        }


        private void SaveGooseAssignmentWithNoRelatedDevice(GooseFtpOnlyAssignmentItem ftpOnlyGooseAssignmentItem, List<IGoCbFtpEntity> prevoiusMatrixGoCbEntities,
            List<int> indexesOfGooseToPreventDeleting, IGooseMatrixFtp gooseMatrixFtp)
        {
            if (ftpOnlyGooseAssignmentItem.FcdaAssignmentItems.Count != 0)
            {
                gooseMatrixFtp.GoCbFtpEntities.Add(ftpOnlyGooseAssignmentItem.Model);
                if (prevoiusMatrixGoCbEntities.Contains(ftpOnlyGooseAssignmentItem.Model))
                {
                    indexesOfGooseToPreventDeleting.Add(ftpOnlyGooseAssignmentItem.Model.IndexOfGoose);
                    TryAddGocbEntityToMatrix(gooseMatrixFtp, ftpOnlyGooseAssignmentItem.Model);

                }
            }
        }

        private void TryAddGocbEntityToMatrix(IGooseMatrixFtp gooseMatrixFtp, IGoCbFtpEntity goCbFtpEntity)
        {
            if (!gooseMatrixFtp.GoCbFtpEntities.Any((entity => entity.GoCbReference == goCbFtpEntity.GoCbReference)))
            {
                gooseMatrixFtp.GoCbFtpEntities.Add(goCbFtpEntity);
            }
        }
        private void TryAddMacAddressToMatrix(IGooseMatrixFtp gooseMatrixFtp, IMacAddressEntity macAddressEntity)
        {
            if (!gooseMatrixFtp.MacAddressList.Any((entity => entity.MacAddress == macAddressEntity.MacAddress)))
            {
                gooseMatrixFtp.MacAddressList.Add(macAddressEntity);
            }
        }
        private void SaveExistingGooseAssignment(GooseInProjectAssignmentItem gooseInProjectAssignmentItem, List<IGoCbFtpEntity> prevoiusMatrixGoCbEntities,
            List<int> indexesOfGooseToPreventDeleting, IGooseMatrixFtp gooseMatrixFtp)
        {

            var gocbReferenceString = gooseInProjectAssignmentItem.Model.GetFirstParentOfType<IDevice>().Name + 
                                      gooseInProjectAssignmentItem.Model.GetFirstParentOfType<ILDevice>().Inst + 
                                      "/" + gooseInProjectAssignmentItem.Model.GetFirstParentOfType<ILogicalNode>().Name + 
                                      "$GO$" + 
                                      gooseInProjectAssignmentItem.Model.Name;

            var existingGocbReference =
                prevoiusMatrixGoCbEntities.FirstOrDefault((entity => entity.GoCbReference == gocbReferenceString));
            if (existingGocbReference != null)
            {
                indexesOfGooseToPreventDeleting.Add(existingGocbReference.IndexOfGoose);
                TryAddGocbEntityToMatrix(gooseMatrixFtp, existingGocbReference);

            }
            else
            {
                var newGocbEntity = new GoCbFtpEntity();
                newGocbEntity.GoCbReference = gocbReferenceString;
                var gseRelated = _sclCommunicationModelService
                    .GetGsesForDevice(gooseInProjectAssignmentItem.Model.GetFirstParentOfType<IDevice>().Name,
                        _biscProject.MainSclModel.Value)
                    .FirstOrDefault((gse => gse.CbName == gooseInProjectAssignmentItem.Model.Name));
                newGocbEntity.AppId = gseRelated.AppId;
                newGocbEntity.IndexOfGoose = gooseMatrixFtp.GoCbFtpEntities.Count+1;
                TryAddMacAddressToMatrix(gooseMatrixFtp,new MacAddressEntity(){MacAddress= gseRelated.MacAddress});
               TryAddGocbEntityToMatrix(gooseMatrixFtp,newGocbEntity);
            }

            var existingGooseInputsOfDevice = _goosesModelService.GetGooseInputsOfDevice(_device);
            var dataset =
                _datasetModelService.GetAllDataSetOfDevice(gooseInProjectAssignmentItem.Model
                    .GetFirstParentOfType<IDevice>()).FirstOrDefault((set => set.Name == gooseInProjectAssignmentItem.Model.DataSet));
            foreach (var fcdaAssignmentItem in gooseInProjectAssignmentItem.FcdaAssignmentItems)
            {


                var inputExisting = existingGooseInputsOfDevice.FirstOrDefault((input =>
                     input.ExternalGooseReferences.Any((extRef => _goosesModelService.CompareFcdaAndExtRef(extRef, dataset.FcdaList[fcdaAssignmentItem.IndexOfFcdaInDataSet])))));

                var extRefExisting = inputExisting?.ExternalGooseReferences.FirstOrDefault((extRef =>
                    _goosesModelService.CompareFcdaAndExtRef(extRef, dataset.FcdaList[fcdaAssignmentItem.IndexOfFcdaInDataSet])));

                bool isSubscribed = extRefExisting != null;


                if (isSubscribed)
                {
                    if (!fcdaAssignmentItem.IsSubscribed)
                    {
                        inputExisting.ExternalGooseReferences.Remove(extRefExisting);
                    }
                }
                else
                {
                    if (fcdaAssignmentItem.IsSubscribed)
                    {
                        _goosesModelService.AddGooseExternalReferenceToDevice(dataset.FcdaList[fcdaAssignmentItem.IndexOfFcdaInDataSet], _device, fcdaAssignmentItem.ParentDeviceName);
                    }
                }
            }
        }

        public Task<bool> IsSavingByFtpNeeded()
        {
            return Task.FromResult(false);
        }

        public Task<OperationResult> ValidateBeforeSave()
        {
            return Task.FromResult(OperationResult.SucceedResult);
        }

        #endregion


    }
}
