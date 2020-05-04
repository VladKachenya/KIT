using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Connection;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Services;
using BISC.Modules.InformationModel.Model.Elements;

namespace BISC.Modules.InformationModel.Model.Services.LoadingServices
{
    public class DoiValuesLoadingService : IDoiValuesLoadingService
    {
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IInfoModelService _infoModelService;

        public DoiValuesLoadingService(
            IConnectionPoolService connectionPoolService,
            IInfoModelService infoModelService)
        {
            _connectionPoolService = connectionPoolService;
            _infoModelService = infoModelService;
        }
        public async Task LoadDoiValues(ISclModel sclModel, IDevice device, IDoi doi, string requiredFc = null)
        {
            var logicalNode = doi.GetFirstParentOfType<ILogicalNode>();
            var lDevice = doi.GetFirstParentOfType<ILDevice>();
            var variablesDescItem = (await _connectionPoolService.GetConnection(device.Ip).MmsConnection.GetMmsTypeDescription(device.Name + lDevice.Inst, logicalNode.Name, true)).Item;

            var allFcsWithDai = _infoModelService.GetAllFcsWithDai(doi.DaiCollection.ToList(), doi.SdiCollection.ToList(), sclModel).ToList();
            var allFcs = allFcsWithDai.Select((tuple => tuple.Item1)).Distinct();
            if (requiredFc != null)
            {
                allFcs = allFcs.Where(f => string.Equals(f, requiredFc, StringComparison.CurrentCultureIgnoreCase));
            }
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