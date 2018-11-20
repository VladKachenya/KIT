using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Connection.Infrastructure.Connection;
using BISC.Modules.DataSets.Infrastructure.Model;

namespace BISC.Modules.DataSets.Model.Mappers
{
   public static class FcdaMapper
    {
        public static List<FcdaDto> ToDtos(this IEnumerable<IFcda> fcdaList,string deviceName)
        {
            List<FcdaDto> fcdaDtos = new List<FcdaDto>();
            foreach (var fcda in fcdaList)
            {
                FcdaDto fcdaDto = new FcdaDto();
                List<string> pathList = new List<string>();
                if (fcda.DoName.Contains("."))
                {
                    pathList.AddRange(fcda.DoName.Split('.'));
                }
                else
                {
                    pathList.Add(fcda.DoName);
                }
                if (fcda.DaName != null)
                {
                    pathList.Add(fcda.DaName);
                }
                fcdaDto.DaDoPathParts = pathList.ToArray();
                fcdaDto.Fc = fcda.Fc.ToString();
                fcdaDto.Ied = deviceName;
                fcdaDto.Ld = fcda.LdInst;
                fcdaDto.Ln = fcda.Prefix + fcda.LnClass + fcda.LnInst;
                fcdaDtos.Add(fcdaDto);
            }

            return fcdaDtos;
        }
    }
}
