using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Presentation.Interfaces.Factories;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix.Entities;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Presentation.Infrastructure.Factories;

namespace BISC.Modules.Gooses.Presentation.Factories
{
    public class GooseControlBlockAssignmentItemFactory : IGooseControlBlockAssignmentItemFactory
    {
        private readonly IDeviceModelService _deviceModelService;
        private readonly IDatasetModelService _datasetModelService;
        private readonly IBiscProject _biscProject;
        private readonly IGoosesModelService _goosesModelService;
        private readonly ICommandFactory _commandFactory;

        public GooseControlBlockAssignmentItemFactory(IDeviceModelService deviceModelService, IDatasetModelService datasetModelService,
            IBiscProject biscProject, IGoosesModelService goosesModelService, ICommandFactory commandFactory)
        {
            _deviceModelService = deviceModelService;
            _datasetModelService = datasetModelService;
            _biscProject = biscProject;
            _goosesModelService = goosesModelService;
            _commandFactory = commandFactory;
        }


        #region Implementation of IGooseControlBlockAssignmentItemFactory

        public IEnumerable<GooseControlBlockAssignmentItem> CreateGooseControlBlockAssignmentItems(IDevice deviceOfTheItems)
        {
            var resultItems = new List<GooseControlBlockAssignmentItem>();

            var gooseFtpMatrix = _goosesModelService.GetGooseMatrixFtpForDevice(deviceOfTheItems);

            if (gooseFtpMatrix == null)
                FillItemsFromSclFile(deviceOfTheItems, resultItems);
            else
                FillItemsFromFtpFile(deviceOfTheItems, resultItems,gooseFtpMatrix);

            return resultItems;
        }


        private void FillItemsFromFtpFile(IDevice deviceOfTheItems, List<GooseControlBlockAssignmentItem> resultItems,IGooseMatrixFtp gooseMatrixFtp)
        {
            var subscribedGooseControlsForCurrentDevice =
                _goosesModelService.GetGooseControlsSubscribed(deviceOfTheItems, _biscProject.MainSclModel.Value);

            foreach (var goCbFtpEntity in gooseMatrixFtp.GoCbFtpEntities)
            {
                var subscribedRelatedGooseControl = subscribedGooseControlsForCurrentDevice.FirstOrDefault((tuple =>
                    GetIfGooseControlConformReference(goCbFtpEntity, tuple.Item2, tuple.Item1)));

                resultItems.Add(subscribedRelatedGooseControl == null
                    ? BuildNotExistingGooseAssigmentItem(goCbFtpEntity,gooseMatrixFtp)
                    : BuildExistingGooseAssigmentItem(goCbFtpEntity, subscribedRelatedGooseControl,gooseMatrixFtp, deviceOfTheItems));
            }

            foreach (var subscribedGooseControl in subscribedGooseControlsForCurrentDevice)
            {
                if (!gooseMatrixFtp.GoCbFtpEntities.Any((entity =>
                    GetIfGooseControlConformReference(entity, subscribedGooseControl.Item2,
                        subscribedGooseControl.Item1))))
                {
                    resultItems.Add(BuildUnsubscribedGooseControlBlockAssignmentItem(subscribedGooseControl));
                }
            }

        }

        private GooseControlBlockAssignmentItem BuildExistingGooseAssigmentItem(IGoCbFtpEntity goCbFtpEntity, Tuple<IDevice, IGooseControl> subscribedRelatedGooseControl,IGooseMatrixFtp gooseMatrixFtp,IDevice deviceOfTheItems)
        {
            GooseControlBlockAssignmentItem gooseControlBlockAssignmentItem = new GooseInProjectAssignmentItem(_commandFactory,subscribedRelatedGooseControl.Item2);
            gooseControlBlockAssignmentItem.Signature = subscribedRelatedGooseControl.Item2.Name;

            var dataSet = _datasetModelService.GetAllDataSetOfDevice(subscribedRelatedGooseControl.Item1).FirstOrDefault((set => set.Name == subscribedRelatedGooseControl.Item2.DataSet));
      var relatedRows =
                gooseMatrixFtp.GooseRowFtpEntities.Where((row => row.IndexOfGoose == goCbFtpEntity.IndexOfGoose)).ToList();
            relatedRows.AddRange(gooseMatrixFtp.GooseRowQualityFtpEntities.Where((row => row.IndexOfGoose == goCbFtpEntity.IndexOfGoose)).ToList());
            var existingGooseInputsOfDevice = _goosesModelService.GetGooseInputsOfDevice(deviceOfTheItems);


            foreach (var fcda in dataSet.FcdaList)
            {
                bool isSubscribed =
                    relatedRows.Any((row => row.NumberOfFcdaInDataSetOfGoose == dataSet.FcdaList.IndexOf(fcda)))|| existingGooseInputsOfDevice.Any((input =>
                        input.ExternalGooseReferences.Any((extRef => _goosesModelService.CompareFcdaAndExtRef(extRef, fcda)))));
                var fcdaAssignmentItem = BuildFcdaAssignmentItemFromFcda(fcda, dataSet.FcdaList.IndexOf(fcda));
                fcdaAssignmentItem.IsSubscribed = isSubscribed;
                fcdaAssignmentItem.ParentDeviceName = subscribedRelatedGooseControl.Item1.Name;
                gooseControlBlockAssignmentItem.FcdaAssignmentItems.Add(fcdaAssignmentItem);
            }

            return gooseControlBlockAssignmentItem;
        }
        
        private GooseControlBlockAssignmentItem BuildNotExistingGooseAssigmentItem(IGoCbFtpEntity goCbFtpEntity,IGooseMatrixFtp gooseMatrixFtp)
        {
            GooseControlBlockAssignmentItem gooseControlBlockAssignmentItem=new GooseFtpOnlyAssignmentItem(_commandFactory, goCbFtpEntity);
            gooseControlBlockAssignmentItem.Signature = goCbFtpEntity.GoCbReference;
            var relatedRows =
                gooseMatrixFtp.GooseRowFtpEntities.Where((row => row.IndexOfGoose == goCbFtpEntity.IndexOfGoose)).ToList();
            relatedRows.AddRange(gooseMatrixFtp.GooseRowQualityFtpEntities.Where((row => row.IndexOfGoose == goCbFtpEntity.IndexOfGoose)).ToList());

            foreach (var relatedRow in relatedRows)
            {
                if(relatedRow.BitIndex>64)continue;
                FcdaAssignmentItem fcdaAssignmentItem = new FcdaAssignmentItem();
                fcdaAssignmentItem.IndexOfFcdaInDataSet = relatedRow.NumberOfFcdaInDataSetOfGoose;
                fcdaAssignmentItem.Signature = goCbFtpEntity.GoCbReference+"  ["+ relatedRow.NumberOfFcdaInDataSetOfGoose+"]";
                fcdaAssignmentItem.IsSubscribed = true;
                gooseControlBlockAssignmentItem.FcdaAssignmentItems.Add(fcdaAssignmentItem);
            }

            return gooseControlBlockAssignmentItem;
        }

        private bool GetIfGooseControlConformReference(IGoCbFtpEntity goCbFtpEntity, IGooseControl gooseControl,
            IDevice device)
        {
            var referenceParts = goCbFtpEntity.GoCbReference.Split('/', '$');
            if (referenceParts.Last() != gooseControl.Name) return false;
            if (referenceParts.First() != device.Name + gooseControl.GetFirstParentOfType<ILDevice>().Inst)
                return false;
            return true;
        }

        private GooseControlBlockAssignmentItem BuildUnsubscribedGooseControlBlockAssignmentItem(Tuple<IDevice, IGooseControl> subscribedGooseTuple)
        {
            GooseControlBlockAssignmentItem gooseControlBlockAssignmentItem =
                new GooseInProjectAssignmentItem(_commandFactory,subscribedGooseTuple.Item2);
            gooseControlBlockAssignmentItem.Signature =subscribedGooseTuple.Item1.Name+"."+ subscribedGooseTuple.Item2.Name;
            var dataSet = _datasetModelService.GetAllDataSetOfDevice(subscribedGooseTuple.Item1).FirstOrDefault((set => set.Name == subscribedGooseTuple.Item2.DataSet));
            foreach (var fcda in dataSet.FcdaList)
            {
                var fcdaAssignmentItem = BuildFcdaAssignmentItemFromFcda(fcda, dataSet.FcdaList.IndexOf(fcda));
                fcdaAssignmentItem.IsSubscribed = false;
                fcdaAssignmentItem.ParentDeviceName = subscribedGooseTuple.Item1.Name;
                gooseControlBlockAssignmentItem.FcdaAssignmentItems.Add(fcdaAssignmentItem);

            }

            return gooseControlBlockAssignmentItem;
        }


        private void FillItemsFromSclFile(IDevice deviceOfTheItems, List<GooseControlBlockAssignmentItem> resultItems)
        {
            var existingGooseInputsOfDevice = _goosesModelService.GetGooseInputsOfDevice(deviceOfTheItems);
            var subscribedGooseControlsForCurrentDevice =
                _goosesModelService.GetGooseControlsSubscribed(deviceOfTheItems, _biscProject.MainSclModel.Value);

            foreach (var subscribedGooseTuple in subscribedGooseControlsForCurrentDevice)
            {

                var dataSet = _datasetModelService.GetAllDataSetOfDevice(subscribedGooseTuple.Item1).FirstOrDefault((set => set.Name == subscribedGooseTuple.Item2.DataSet));
                if (dataSet != null)
                {
                    GooseControlBlockAssignmentItem gooseControlBlockAssignmentItem =
                        new GooseInProjectAssignmentItem(_commandFactory,subscribedGooseTuple.Item2);
                    gooseControlBlockAssignmentItem.Signature = subscribedGooseTuple.Item1.Name + "." + subscribedGooseTuple.Item2.Name;

                    foreach (var fcda in dataSet.FcdaList)
                    {

                        bool isSubscribed = existingGooseInputsOfDevice.Any((input =>
                            input.ExternalGooseReferences.Any((extRef => _goosesModelService.CompareFcdaAndExtRef(extRef, fcda)))));
                        var fcdaAssignmentItem = BuildFcdaAssignmentItemFromFcda(fcda, dataSet.FcdaList.IndexOf(fcda));
                        fcdaAssignmentItem.IsSubscribed = isSubscribed;
                        fcdaAssignmentItem.ParentDeviceName = subscribedGooseTuple.Item1.Name;
                        gooseControlBlockAssignmentItem.FcdaAssignmentItems.Add(fcdaAssignmentItem);
                    }
                    resultItems.Add(gooseControlBlockAssignmentItem);
                }

            }
        }

        private FcdaAssignmentItem BuildFcdaAssignmentItemFromFcda(IFcda fcda,int indexOfFcda)
        {
            FcdaAssignmentItem fcdaAssignmentItem = new FcdaAssignmentItem();
            fcdaAssignmentItem.IndexOfFcdaInDataSet = indexOfFcda;
            if (string.IsNullOrEmpty(fcda.DaName))
            {
                fcdaAssignmentItem.Signature =
                    fcda.LdInst + "." + fcda.Prefix + fcda.LnClass + fcda.LnInst + "." +
                    fcda.DoName + "[" + fcda.Fc + "]";
            }
            else
            {
                fcdaAssignmentItem.Signature =
                    fcda.LdInst + "." + fcda.Prefix + fcda.LnClass + fcda.LnInst + "." +
                    fcda.DoName + "." + fcda.DaName + ".[" + fcda.Fc + "]";
            }
            return fcdaAssignmentItem;
        }



        #endregion
    }
}
