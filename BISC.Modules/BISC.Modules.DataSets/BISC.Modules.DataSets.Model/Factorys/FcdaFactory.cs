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
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Common;

namespace BISC.Modules.DataSets.Model.Factorys
{
 
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






        #region privats methods



        private string GetDoNameRecursive(IModelElement modelElement,string daName)
        {
            if (modelElement is IDoi doi)
            {
                return doi.Name+"."+daName;
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
