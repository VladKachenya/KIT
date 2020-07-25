using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using BISC.Modules.InformationModel.Model.DataTypeTemplates.LNodeType;

namespace BISC.Modules.DataSets.Model.Services
{
    public class FcdaInfoService : IFcdaInfoService
    {
        private readonly IDataTypeTemplatesModelService _dataTypeTemplatesModelService;
        private readonly IBiscProject _biscProject;

        public FcdaInfoService(IDataTypeTemplatesModelService dataTypeTemplatesModelService, IBiscProject biscProject)
        {
            _dataTypeTemplatesModelService = dataTypeTemplatesModelService;
            _biscProject = biscProject;
        }

        #region Implementation of IFcdaInfoService

        public IModelElement GetModelElementFromFcda(IDevice device, IFcda fcda)
        {
            var lDevices = new List<ILDevice>();
            device.GetAllChildrenOfType<ILDevice>(ref lDevices);
            var lDevice = lDevices.Find(ld => ld.Inst == fcda.LdInst);

            if (lDevice == null || string.IsNullOrEmpty(fcda.LnClass))
            {
                return null;
            }

            List<ILogicalNode> lNodes = new List<ILogicalNode>();
            lDevice.GetAllChildrenOfType<ILogicalNode>(ref lNodes);
            var lNode = lNodes.Find(ln => ln.Prefix + ln.LnClass + ln.Inst == fcda.Prefix + fcda.LnClass + fcda.LnInst);

            if (lNode == null || string.IsNullOrEmpty(fcda.DoName))
            {
                return null;
            }

            if (fcda.DoName == null)
            {
                return null;
            }

            List<string> Dos = $"{fcda.DoName}.{fcda.DaName}".Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            //if (!string.IsNullOrEmpty(fcda.DaName))
            //{
            //    Dos.RemoveAt(Dos.Count - 1);
            //}

            IModelElement daParint = lNode;

            for (int i = 0; i < Dos.Count; i++)
            {
                if (i == 0)
                {
                    List<IDoi> doDois = new List<IDoi>();
                    daParint.GetAllChildrenOfType<IDoi>(ref doDois);
                    daParint = doDois.Find(element => element.Name == Dos[i]);
                }
                else
                {
                    List<ISdi> doSdis = new List<ISdi>();
                    daParint.GetAllChildrenOfType<ISdi>(ref doSdis);
                    var par = doSdis.FirstOrDefault(element => (element.Name == Dos[i]));
                    if(par == null) continue;
                    daParint = par;
                }
            }


            if (daParint == null)
            {
                return null;
            }

            if (fcda.DaName == null)
            {
                return daParint;
            }

            List<IDai> dais = new List<IDai>();
            daParint.GetAllChildrenOfType<IDai>(ref dais);

            var resDai = dais.FirstOrDefault(dai => fcda.DaName.Split('.').Contains(dai.Name));

            if (resDai == null)
            {
                return daParint;
            }

            return resDai;
        }

        public int GetFcdaWeight(IDevice device, IFcda fcda)
        {
            var modelElement = GetModelElementFromFcda(device, fcda);
            if (modelElement == null)
            { }
            return GetModelElementWeight(device, modelElement, fcda.Fc);
        }

        public int GetModelElementWeight(IDevice device, IModelElement modelElement, string fc)
        {
            List<IDai> daisOfModelEment = new List<IDai>();
            modelElement.GetAllChildrenOfType(ref daisOfModelEment);


            int weight = 0;
            foreach (var dai in daisOfModelEment)
            {
                IDa da = _dataTypeTemplatesModelService.GetDaOfDai(dai, device.GetFirstParentOfType<ISclModel>());
                if (da.Fc == fc)
                {
                    weight++;
                }
            }
            return weight;
        }

        public List<string> GetFcsOfModelElement(IDevice device, IModelElement modelElement)
        {
            var res = new List<string>();
            var daisOfModelEment = new List<IDai>();
            if (modelElement is IDai)
            {
                daisOfModelEment.Add(modelElement as IDai);
            }
            else
            {
                modelElement.GetAllChildrenOfType<IDai>(ref daisOfModelEment);
            }
            foreach (var dai in daisOfModelEment)
            {
                IDa da = _dataTypeTemplatesModelService.GetDaOfDai(dai, _biscProject.MainSclModel.Value);
                if (da != null && res.All(el => el != da.Fc))
                {
                    res.Add(da.Fc);
                }
            }

            return res;
        }


        #endregion
    }
}