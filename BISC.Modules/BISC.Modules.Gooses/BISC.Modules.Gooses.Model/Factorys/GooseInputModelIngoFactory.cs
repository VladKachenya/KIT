using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.Gooses.Infrastructure.Factorys;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Model.Model.Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Serializing;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Model.Helpers;
using BISC.Modules.InformationModel.Infrastucture.Elements;

namespace BISC.Modules.Gooses.Model.Factorys
{
    public class GooseInputModelIngoFactory : IGooseInputModelIngoFactory
    {

        private readonly ISclCommunicationModelService _sclCommunicationModelService;
        private readonly IBiscProject _biscProject;
        private readonly IDatasetModelService _datasetModelService;

        public GooseInputModelIngoFactory(ISclCommunicationModelService sclCommunicationModelService, 
            IBiscProject biscProject, IDatasetModelService datasetModelService)
        {
            _sclCommunicationModelService = sclCommunicationModelService;
            _biscProject = biscProject;
            _datasetModelService = datasetModelService;
        }

        public IGooseInputModelInfo CreateGooseInputModelInfo(IDevice parientDevice, IGooseControl gooseControl)
        {

            var res = new GooseInputModelInfo() { EmittingDeviceName = parientDevice.Name };
            res.GocbRef = StaticGooseStringHelper.GetGooseControlReference(gooseControl);
            res.EmittingGooseControl.Value = gooseControl.DeepClone();
            res.EmittingGse.Value = _sclCommunicationModelService
                .GetGsesForDevice(parientDevice.Name, _biscProject.MainSclModel.Value)
                .First(gse => gse.CbName == gooseControl.Name).DeepClone();
            res.EmittingDataSet.Value = _datasetModelService.GetDataSetOfDevice(parientDevice, gooseControl.DataSet).DeepClone();
            return res;
        }

        public List<IGooseInputModelInfo> CreateGooseInputModelInfoList(List<Tuple<IDevice, IGooseControl>> gooseControls)
        {
            List<IGooseInputModelInfo> res = new List<IGooseInputModelInfo>();
            foreach (var gooseControl in gooseControls)
            {
                res.Add(CreateGooseInputModelInfo(gooseControl.Item1, gooseControl.Item2));
            }
            return res;
        }
    }
}
