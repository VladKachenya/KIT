using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Connection;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Loading;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Keys;
using BISC.Modules.InformationModel.Infrastucture.Services;
using BISC.Modules.InformationModel.Model.Elements;

namespace BISC.Modules.InformationModel.Model.Services.LoadingServices
{
    public class InfoModelValuesLoadingService : IDeviceElementLoadingService
    {
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IInfoModelService _infoModelService;

        public InfoModelValuesLoadingService(
            IConnectionPoolService connectionPoolService,
            IInfoModelService infoModelService)
        {
            _connectionPoolService = connectionPoolService;
            _infoModelService = infoModelService;
        }
        public void Dispose()
        {
            
        }

        public async Task<int> EstimateProgress(IDevice device)
        {
            var resTack = new Task<int>(() => 1);
            resTack.Start();
            return await resTack;
        }

        public async Task Load(IDevice device, IProgress<object> deviceLoadingProgress, ISclModel sclModel, CancellationToken cancellationToken)
        {
            //TODO
            var dois = _infoModelService.GetAllDoiWithDbRecursive(device);
            //foreach (var doi in dois)
            //{
            //    var lDevice = _infoModelService.GetParentLDevice(doi);
            //    var logicalNode = _infoModelService.GetParentLogicalNode(doi);

            //    var variablesDescItem = (await _connectionPoolService.GetConnection(device.Ip).MmsConnection.GetMmsTypeDescription(device.Name + lDevice.Inst, logicalNode.Name, true)).Item;
            //    var valuesRes = await _connectionPoolService.
            //        GetConnection(device.Ip).MmsConnection.ReadValuesAsync(InformationModelKeys.DataAttributeHeaderKeys.dbFc, 
            //            device.Name, logicalNode.Name, lDevice.Inst, new List<string>() { doi.Name });
            //    var daiDbsOfDoi = _infoModelService.GetAllFcsWithDai(doi.DaiCollection.ToList(), doi.SdiCollection.ToList())
            //        .Where(e => e.Item1 == InformationModelKeys.DataAttributeHeaderKeys.dbFc).Select(e => e.Item2);
            //    ApplyValueToDais(daiDbsOfDoi, valuesRes.Item, variablesDescItem);
            //}
        }

        public int Priority => 15;


        /// <summary>
        /// Bad bad bad, for it needs remove duplicates
        /// </summary>
        /// <param name="daisOfFc"></param>
        /// <param name="valuesResItem"></param>
        /// <param name="varDescForFc"></param>
        private void ApplyValueToDais(IEnumerable<IDai> daisOfFc, ValueDescription valuesResItem, MmsTypeDescription varDescForFc)
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
                        var mmsTypeDesc = varDescForFc.Components.FirstOrDefault((description => description.Name == InformationModelKeys.DataAttributeHeaderKeys.dbFc));
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