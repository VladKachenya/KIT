using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Connection.Infrastructure.Connection;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Services;
using BISC.Modules.InformationModel.Model.Elements;
using BISC.Modules.InformationModel.Presentation.Interfaces;
using BISC.Modules.InformationModel.Presentation.ViewModels.InfoModelTree;
using BISC.Presentation.BaseItems.ViewModels;

namespace BISC.Modules.InformationModel.Presentation.Helpers
{
    public class ModelValuesLoadingHelper
    {
        private readonly ILoggingService _loggingService;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IInfoModelService _infoModelService;

        public ModelValuesLoadingHelper(
            ILoggingService loggingService, 
            IConnectionPoolService connectionPoolService, 
            IInfoModelService infoModelService)
        {
            _loggingService = loggingService;
            _connectionPoolService = connectionPoolService;
            _infoModelService = infoModelService;
        }

        public async Task LoadValues(IEnumerable<IInfoModelItemViewModel> collectionToUpdate, IDevice device)
        {
            foreach (var itemViewModel in collectionToUpdate)
            {
                try
                {
                    (itemViewModel as ComplexViewModelBase)?.BlockViewModelBehavior?.SetLightBlock();
                    if (itemViewModel is LDeviceInfoModelItemViewModel)
                    {
                        await UpdateLNodeValues(itemViewModel.ChildInfoModelItemViewModels, device);
                    }

                    if (itemViewModel is LogicalNodeInfoModelItemViewModel)
                    {
                        await UpdateLNodeValues(
                            new List<IInfoModelItemViewModel>(new[] { itemViewModel }), device);
                    }
                }
                catch (Exception e)
                {
                    _loggingService.LogException(e);
                }
                finally
                {
                    (itemViewModel as ComplexViewModelBase)?.BlockViewModelBehavior?.Unlock();
                }
            }
        }

        public async Task UpdateLNodeValues(IEnumerable<IInfoModelItemViewModel> lnodeViewModels, IDevice device)
        {
            foreach (var lnodeViewModel in lnodeViewModels)
            {
                ILogicalNode logicalNode = lnodeViewModel.Model as ILogicalNode;
                ILDevice lDevice = logicalNode.GetFirstParentOfType<ILDevice>();
                var variablesDesc = await _connectionPoolService.GetConnection(device.Ip).MmsConnection.GetMmsTypeDescription(device.Name + lDevice.Inst, logicalNode.Name, true);

                foreach (var doiViewModel in lnodeViewModel.ChildInfoModelItemViewModels)
                {
                    await UpdateDoiViewModelValues(device, doiViewModel);
                }
            }
        }

        public async Task UpdateDoiViewModelValues(IDevice device, IInfoModelItemViewModel doiViewModel, ILogicalNode logicalNode = null, ILDevice lDevice = null, MmsTypeDescription variablesDescItem = null)
        {
            if (!(doiViewModel.Model is IDoi)) return;
            var doi = (IDoi) doiViewModel.Model;
            await UpdateDoiValues(device, doi, logicalNode, lDevice, variablesDescItem);
            (doiViewModel as DoiInfoModelItemViewModel)?.UpdateChildsValues();
        }

        private async Task UpdateDoiValues(IDevice device, IDoi doi, ILogicalNode logicalNode = null, ILDevice lDevice = null, MmsTypeDescription variablesDescItem = null)
        {
            logicalNode = logicalNode ?? doi.GetFirstParentOfType<ILogicalNode>();
            lDevice = lDevice ?? doi.GetFirstParentOfType<ILDevice>();
            variablesDescItem = variablesDescItem ?? 
                                (await _connectionPoolService.GetConnection(device.Ip).MmsConnection.GetMmsTypeDescription(device.Name + lDevice.Inst, logicalNode.Name, true)).Item;

            var allFcsWithDai = _infoModelService.GetAllFcsWithDai(doi.DaiCollection.ToList(), doi.SdiCollection.ToList()).ToList();
            var allFcs = allFcsWithDai.Select((tuple => tuple.Item1)).Distinct().ToList();
            foreach (var fc in allFcs)
            {
                var varDescForFc =
                    variablesDescItem.Components.FirstOrDefault((description => description.Name == fc));

                var valuesRes = await _connectionPoolService.GetConnection(device.Ip).MmsConnection.ReadValuesAsync(fc, device.Name, logicalNode.Name, lDevice.Inst, new List<string>() { doi.Name });
                if (valuesRes.IsSucceed)
                {
                    var daisOfFc = allFcsWithDai.Where((tuple => tuple.Item1 == fc)).Select((tuple => tuple.Item2)).ToList();
                    try
                    {
                        ApplyValueToDais(daisOfFc, valuesRes.Item, varDescForFc);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            }
        }

        private void ApplyValueToDais(List<IDai> daisOfFc, ValueDescription valuesResItem, MmsTypeDescription varDescForFc)
        {
            foreach (var dai in daisOfFc)
            {
                try
                {
                    if (dai.ParentModelElement is IDoi doi)
                    {
                        var mmsTypeDescriptionForDoi = varDescForFc.Components.FirstOrDefault((description => description.Name == doi.Name));
                        var mmsTypeDescriptionForDai = mmsTypeDescriptionForDoi.Components.FirstOrDefault((description => description.Name == dai.Name));
                        var indexOfDai = mmsTypeDescriptionForDoi.Components.IndexOf(mmsTypeDescriptionForDai);
                        dai.Value.Value = new Val();
                        dai.Value.Value.Value = valuesResItem.Components[indexOfDai].Value;
                    }
                    else
                    {

                        var pathToDai = new List<string>();
                        GetPathToDai(dai, pathToDai);
                        pathToDai.Reverse();

                        var doiName = pathToDai.First();
                        pathToDai.Remove(doiName);
                        var mmsTypeDesc = varDescForFc.Components.FirstOrDefault((description => description.Name == doiName));
                        var valueDesc = valuesResItem;

                        foreach (var pathElement in pathToDai)
                        {
                            var innerMmsTypeDesc =
                                mmsTypeDesc.Components.FirstOrDefault((description => description.Name == pathElement));
                            if (innerMmsTypeDesc != null)
                            {
                                var index = mmsTypeDesc.Components.IndexOf(innerMmsTypeDesc);
                                mmsTypeDesc = innerMmsTypeDesc;
                                valueDesc = valueDesc.Components[index];
                            }
                        }
                        dai.Value.Value = new Val();
                        dai.Value.Value.Value = valueDesc.Value;

                        //TODO
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        private void GetPathToDai(IModelElement modelElement, List<string> initialPath)
        {
            if (modelElement is IDai dai)
            {
                initialPath.Add(dai.Name);
                GetPathToDai(dai.ParentModelElement, initialPath);
            }
            else if (modelElement is ISdi sdi)
            {
                initialPath.Add(sdi.Name);
                GetPathToDai(sdi.ParentModelElement, initialPath);
            }
            else if (modelElement is IDoi doi)
            {
                initialPath.Add(doi.Name);
            }
        }
    }
}
