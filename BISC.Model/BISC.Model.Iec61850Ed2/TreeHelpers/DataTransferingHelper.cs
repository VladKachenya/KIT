using System;
using System.Collections.Generic;
using BISC.Model.Iec61850Ed2.SclModelTemplates;

namespace BISC.Model.Iec61850Ed2.TreeHelpers
{
    public class DataTransferingHelper
    {
        public static void TransferValuesFromDeviceToLocal(tIED ied)
        {
            foreach (var lDevice in ied.AccessPoint[0].Server.LDevice)
            {
                foreach (var ln in lDevice.LN)
                {
                    foreach (var doi in ln.DOI)
                    {
                        TransferValuesFromDeviceToLocalRecursive(doi.DAI, doi.SDI);
                    }
                }
                foreach (var doi in lDevice.LN0.DOI)
                {
                    TransferValuesFromDeviceToLocalRecursive(doi.DAI, doi.SDI);
                }
            }
        }



        public static void TransferValuesFromDeviceToLocalRecursive(List<tDAI> dais, List<tSDI> sdis)
        {
            try
            {


                if (dais.Count > 0)
                {
                    foreach (var dai in dais)
                    {
                        dai.Val = dai.ValueFromDevice;
                    }
                }
                if (sdis.Count > 0)
                {
                    foreach (var sdi in sdis)
                    {
                        TransferValuesFromDeviceToLocalRecursive(sdi.DAI, sdi.SDI);
                    }
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}
