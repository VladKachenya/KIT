using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.DataSets.Infrastructure.Factorys;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Model.Model;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Model.Elements;

namespace BISC.Modules.DataSets.Model.Factorys
{

    public class FcdaFactory : IFcdaFactory
    {
        private IDataTypeTemplatesModelService _dataTypeTemplatesModelService;
        private IBiscProject _biscProject;
        private readonly IFcdaInfoService _fcdaInfoService;


        public FcdaFactory(IDataTypeTemplatesModelService dataTypeTemplatesModelService, IBiscProject biscProject, IFcdaInfoService fcdaInfoService)
        {
            _dataTypeTemplatesModelService = dataTypeTemplatesModelService;
            _biscProject = biscProject;
            _fcdaInfoService = fcdaInfoService;
        }

        public IFcda GetFcda(IDai dai, string fc = null,  ISclModel sclModel = null)
        {
            //string iecAddress = string.Format("{0}/{1}.{2}", GetLdInst(dai), GetLnName(dai), doName);
            //IDa da = _dataTypeTemplatesModelService.GetDaOfDai(dai, _biscProject.MainSclModel.Value);
            //string daName = da.Name;
            //string fc = da.Fc;
            IFcda result = new Fcda();
            result.LdInst = dai.GetFirstParentOfType<ILDevice>().Inst;
            var ln = dai.GetFirstParentOfType<ILogicalNode>();
            result.LnClass = ln.LnClass;
            result.LnInst = ln.Inst;
            var names = GetNamesRecursive(dai);
            result.DoName = names.Item1;
            result.DaName = names.Item2;
            result.Prefix = ln.Prefix;
            result.Fc = fc ?? GetDaFc(dai, sclModel ?? _biscProject.MainSclModel.Value);
            return result;
        }
        public List<IFcda> GetFcdasFromModelElement(IModelElement modelElement, ISclModel sclModel = null)
        {
            var res = new List<IFcda>();
            var fcs = _fcdaInfoService.GetFcsOfModelElement(modelElement.GetFirstParentOfType<IDevice>(), modelElement)
                .Where(el => el != "CO");
            foreach (var fc in fcs)
            {
                res.Add(GetStructFcda(modelElement, fc));
            }
            return res;
        }

        public IFcda GetStructFcda(IModelElement modelElement, string fc, ISclModel sclModel = null)
        {
            IFcda result = new Fcda();
            result.LdInst = modelElement.GetFirstParentOfType<ILDevice>().Inst;
            var ln = modelElement.GetFirstParentOfType<ILogicalNode>();
            result.LnClass = ln.LnClass;
            result.LnInst = ln.Inst;
            var names = GetNamesRecursive(modelElement);
            result.DoName = names.Item1;
            result.DaName = names.Item2;
            result.Prefix = ln.Prefix;
            result.Fc = fc;//_fcdaInfoService.GetFcsOfModelElement(modelElement.GetFirstParentOfType<IDevice>(), modelElement).First(el => el == fc); ;
            return result;
        }



        #region privats methods



        private (string, string) GetNamesRecursive(IModelElement modelElement, string daName = "", int deep = 0)
        {
            deep++;
            if (modelElement is IDoi doi)
            {
                if (daName == "")
                {
                    return (doi.Name, string.Empty);
                }
                else
                {
                    var res = doi.Name + "." + daName;
                    var arr = res.Split('.');
                    if (arr.Length == 2)
                    {
                        return (arr[0], arr[1]);
                    }

                    var daiName = string.Empty;
                    arr.ToList().GetRange(2, arr.Length - 2).ForEach(e => daiName = $"{daiName}.{e}");
                    daiName = daiName.Trim('.');
                    return ($"{arr[0]}.{arr[1]}", daiName);
                }
            }

            if (modelElement is IDai dai)
            {
                daName = dai.Name;
            }
            if (modelElement is ISdi sdi)
            {
                if (!string.IsNullOrEmpty(daName))
                {
                    daName = sdi.Name + "." + daName;
                }
                else
                {
                    daName = sdi.Name;
                }
            }
            return GetNamesRecursive(modelElement.ParentModelElement, daName, deep);
        }


        private string GetDaFc(IDai dai, ISclModel sclModel)
        {
            IDa da = _dataTypeTemplatesModelService.GetDaOfDai(dai, sclModel);
            return da.Fc;
        }




        #endregion
    }
}
