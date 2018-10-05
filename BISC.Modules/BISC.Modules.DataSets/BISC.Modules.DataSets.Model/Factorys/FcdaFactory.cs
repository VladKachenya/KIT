using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.DataSets.Infrastructure.Factorys;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Model.Model;
using BISC.Modules.InformationModel.Infrastucture;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.DataSets.Model.Factorys
{
    static class LnInfo
    {
        public const string classInfo = "classInfo";
        public const string instInfo = "instInfo";
    }
    static class DaInfo
    {
        public const string daName = "daName";
        public const string daFc = "daFc";
    }
    public class FcdaFactory : IFcdaFactory
    {
        IDataTypeTemplatesModelService _dataTypeTemplatesModelService;
        IBiscProject _biscProject;


        public FcdaFactory(IDataTypeTemplatesModelService dataTypeTemplatesModelService, IBiscProject biscProject)
        {
            _dataTypeTemplatesModelService = dataTypeTemplatesModelService;
            _biscProject = biscProject;
        }

        public IFcda GetFcda(IDai dai)
        {
            //string iecAddress = string.Format("{0}/{1}.{2}", GetLdInst(dai), GetLnName(dai), doName);
            //IDa da = _dataTypeTemplatesModelService.GetDaOfDai(dai, _biscProject.MainSclModel.Value);
            //string daName = da.Name;
            //string fc = da.Fc;
            IFcda result = new Fcda();
            result.LdInst = GetLdInst(dai);
            result.LnClass = GetLnInfo(dai, LnInfo.classInfo);
            result.LnInst= GetLnInfo(dai, LnInfo.instInfo);
            result.DoName = GetDoiName(dai);
            result.DaName = GetDaInfo(dai, DaInfo.daName);
            result.Fc = GetDaInfo(dai, DaInfo.daFc);
            return result;
        }


        #region privats methods
        private string GetDaInfo(IDai dai, string daInfoType)
        {
            IDa da = _dataTypeTemplatesModelService.GetDaOfDai(dai, _biscProject.MainSclModel.Value);
            if(daInfoType == DaInfo.daName)
                return da.Name;
            if (daInfoType == DaInfo.daFc)
                return da.Fc;
            return string.Empty;
        }

        private string GetLnInfo(IModelElement modelElement, string lnInfoType)
        {
            if (modelElement == null)
            {
                return string.Empty;
            }
            else if (modelElement.ElementName == InfoModelKeys.ModelKeys.LogicalNodeKey)
            {
                if (lnInfoType == LnInfo.classInfo)
                    return (modelElement as ILogicalNode).LnClass;
                if (lnInfoType == LnInfo.instInfo)
                    return (modelElement as ILogicalNode).Inst;
                return string.Empty;
            }
            else
            {
                return GetLdInst(modelElement.ParentModelElement);
            }
        }

        private string GetLdInst(IModelElement modelElement)
        {
            if (modelElement == null)
            {
                return string.Empty;
            }
            else if (modelElement.ElementName == InfoModelKeys.ModelKeys.LDeviceKey)
            {
                return (modelElement as ILDevice).Inst;
            }
            else
            {
                return GetLdInst(modelElement.ParentModelElement);
            }
        }

        private string GetDoiName(IModelElement modelElement)
        {
            if (modelElement == null)
            {
                return string.Empty;
            }
            else if (modelElement.ElementName == InfoModelKeys.ModelKeys.DoiKey)
            {
                return (modelElement as IDoi).Name;
            }
            else
            {
                return GetLdInst(modelElement.ParentModelElement);
            }
        }
        #endregion
    }
}
