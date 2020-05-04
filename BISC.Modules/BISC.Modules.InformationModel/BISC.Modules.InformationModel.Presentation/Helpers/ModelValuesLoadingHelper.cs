using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Connection;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Services;
using BISC.Modules.InformationModel.Presentation.Interfaces;
using BISC.Modules.InformationModel.Presentation.ViewModels.InfoModelTree;
using BISC.Presentation.BaseItems.ViewModels;

namespace BISC.Modules.InformationModel.Presentation.Helpers
{
    public class ModelValuesLoadingHelper
    {
        private readonly ILoggingService _loggingService;
        private readonly IDoiValuesLoadingService _doiValuesLoadingService;


        public ModelValuesLoadingHelper(
            ILoggingService loggingService, 
            IConnectionPoolService connectionPoolService, 
            IDoiValuesLoadingService doiValuesLoadingService)
        {
            _loggingService = loggingService;
            _doiValuesLoadingService = doiValuesLoadingService;
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
                foreach (var doiViewModel in lnodeViewModel.ChildInfoModelItemViewModels)
                {
                    await UpdateDoiViewModelValues(device, doiViewModel);
                }
            }
        }

        public async Task UpdateDoiViewModelValues(IDevice device, IInfoModelItemViewModel doiViewModel)
        {
            if (!(doiViewModel.Model is IDoi)) return;
            var doi = (IDoi) doiViewModel.Model;
            await _doiValuesLoadingService.LoadDoiValues(device.GetFirstParentOfType<ISclModel>() ,device, doi);
            (doiViewModel as DoiInfoModelItemViewModel)?.UpdateChildsValues();
        }


    }
}
