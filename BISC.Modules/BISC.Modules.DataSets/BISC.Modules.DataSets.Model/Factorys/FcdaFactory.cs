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
using System.Linq;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;

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

        public IFcda GetFcda(IDai dai)
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
            result.DoName = GetDoNameRecursive(dai, String.Empty);
            result.DaName = dai.Name;
            result.Prefix = ln.Prefix;
            result.Fc = GetDaFc(dai);
            return result;
        }

        public IFcda GetStructFcda(IDoi doiParent, string fc)
        {
            IFcda result = new Fcda();
            result.LdInst = doiParent.GetFirstParentOfType<ILDevice>().Inst;
            var ln = doiParent.GetFirstParentOfType<ILogicalNode>();
            result.LnClass = ln.LnClass;
            result.LnInst = ln.Inst;
            result.DoName = doiParent.Name;
            result.Prefix = ln.Prefix;
            result.Fc = fc;
            return result;
        }

        public IFcda GetStructFcda(IModelElement element)
        {
            IFcda result = new Fcda();
            result.LdInst = element.GetFirstParentOfType<ILDevice>().Inst;
            var ln = element.GetFirstParentOfType<ILogicalNode>();
            result.LnClass = ln.LnClass;
            result.LnInst = ln.Inst;
            result.DoName = GetDoNameRecursive(element, String.Empty);
            result.Prefix = ln.Prefix;
            // Запрет добавления CO
            result.Fc = _fcdaInfoService.GetFcsOfModelElement(element.GetFirstParentOfType<IDevice>(), element).First(el => el != "CO");
            return result;
        }




        #region privats methods



        private string GetDoNameRecursive(IModelElement modelElement, string daName)
        {
            if (modelElement is IDoi doi)
            {
                if (daName == "")
                {
                    return doi.Name;
                }
                else
                {
                    return doi.Name + "." + daName;
                }
            }

            if (modelElement is IDai dai)
            {

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
            return GetDoNameRecursive(modelElement.ParentModelElement, daName);
        }


        private string GetDaFc(IDai dai)
        {
            IDa da = _dataTypeTemplatesModelService.GetDaOfDai(dai, _biscProject.MainSclModel.Value);
            return da.Fc;
        }




        #endregion
    }
}
