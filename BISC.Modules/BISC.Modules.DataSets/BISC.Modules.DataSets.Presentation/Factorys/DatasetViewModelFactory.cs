using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.ViewModels;
using BISC.Modules.DataSets.Infrastructure.ViewModels.Factorys;
using BISC.Modules.DataSets.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Infrastructure.Global.IoC;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Services;

namespace BISC.Modules.DataSets.Presentation.Factorys
{
    public class DatasetViewModelFactory : IDatasetViewModelFactory
    {
        private readonly IInjectionContainer _injectionContainer;
        private readonly Func<IDataSetViewModel> _datasetViewModelCreator;
        private readonly IInfoModelService _infoModelService;

        public DatasetViewModelFactory(IInjectionContainer injectionContainer,Func<IDataSetViewModel> datasetViewModelCreator,IInfoModelService infoModelService)
        {
            _injectionContainer = injectionContainer;
            _datasetViewModelCreator = datasetViewModelCreator;
            _infoModelService = infoModelService;
        }

        public IDataSetViewModel CreateDataSetViewModel(List<string> existingNames, IModelElement device)
        {
            var name = GetUniqueNameOfDataSet(existingNames);
            IDataSetViewModel newDataSetViewModel = _datasetViewModelCreator();
            newDataSetViewModel.SetParentDevice(device);
            newDataSetViewModel.EditableNamePart = name;
            newDataSetViewModel.ParentLdList = _infoModelService.GetLDevicesFromDevices(device).Select((ldevice => ldevice.Inst)).ToList();
            newDataSetViewModel.SelectedParentLd = newDataSetViewModel.ParentLdList.First();
            newDataSetViewModel.IsEditeble = true;
            newDataSetViewModel.ChangeTracker.SetNew();
            return newDataSetViewModel;
        }
        private string GetUniqueNameOfDataSet(List<string> existingNames)
        {
            string nameBody = "NewDataSet";
            string result;
            int i = 0;
            bool isFind;
            do
            {
                i++;
                result = nameBody + i.ToString();
                isFind = false;
                foreach (var element in existingNames)
                {
                    if (result ==  element)
                        isFind = true;
                }
            } while (isFind);

            return result;
        }

        public ObservableCollection<IDataSetViewModel> GetDataSetsViewModel(List<IDataSet> dataSets)
        {
            ObservableCollection<IDataSetViewModel> result = new ObservableCollection<IDataSetViewModel>();
            foreach (var element in dataSets)
                result.Add(GetDataSetViewModel(element));
            return result;
        }

        public IDataSetViewModel GetDataSetViewModel(IDataSet dataSet)
        {
            IDataSetViewModel result = _injectionContainer.ResolveType<IDataSetViewModel>();
            result.SetModel(dataSet);

            return result;
        }
    }
}
