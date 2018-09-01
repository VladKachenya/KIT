using BISC.Model.Iec61850Ed2.DataTypeTemplates;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DaType;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.EnumType;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.LNodeType;
using BISC.Modules.InformationModel.Model.DataTypeTemplates.DaType;
using BISC.Modules.InformationModel.Model.DataTypeTemplates.DoType;
using BISC.Modules.InformationModel.Model.DataTypeTemplates.EnumType;
using BISC.Modules.InformationModel.Model.DataTypeTemplates.LNodeType;

namespace BISC.Modules.InformationModel.Model.Extensions
{
   public static class MappingExtensions
    {
        public static IEnumType MapEnumType(this tEnumType enumType)
        {

            var result= new EnumType(){};
            result.Id = enumType.id;
            foreach (var enumVal in enumType.EnumVal)
            {
                result.EnumValList.Add(enumVal.MapEnumVal());
            }
            return result;
        }
        public static IEnumVal MapEnumVal(this tEnumVal enumVal)
        {
            return new EnumVal(){Ord=int.Parse(enumVal.ord),Value = enumVal.Value};
        }



        public static IDaType MapDaType(this tDAType daType)
        {
            var result = new DaType();
            result.Id = daType.id;
            foreach (var bda in daType.BDA)
            {
                result.Bdas.Add(bda.MapBda());
            }

            return result;
        }

        public static IBda MapBda(this tBDA bda)
        {
            return new Bda(){BType = bda.bType.ToString(),Type = bda.type,Name = bda.name};
        }

        public static IDoType MapDoType(this tDOType doType)
        {
            var result = new DoType();
            result.Cdc = doType.cdc.ToString();
            result.Id = doType.id;
            foreach (var da in doType.DA)
            { 
               result.DaList.Add(da.MapDa());
            }
            foreach (var sdo in doType.SDO)
            {
                result.SdoList.Add(sdo.MapSdo());
            }
            return result;
        }

        public static IDa MapDa(this tDA da)
        {
            return new Da() { BType = da.bType.ToString(), Type = da.type, Name = da.name ,Fc=da.fc.ToString()};
        }
        public static ISdo MapSdo(this tSDO sdo)
        {
            return new Sdo() { Type = sdo.type, Name = sdo.name };
        }


        public static ILNodeType MapLNodeType(this tLNodeType lNodeType)
        {
            var result = new LNodeType();
            result.Id = lNodeType.id;
            result.LnClass = lNodeType.lnClass;
            foreach (var tDo in lNodeType.DO)
            {
                result.DoList.Add(tDo.MapDo());
            }
            return result;
        }


        public static IDo MapDo(this tDO tDo)
        {
            var result = new Do();
            result.Type = tDo.type;
            result.Name = tDo.name;
            return result;
        }

    }
}
