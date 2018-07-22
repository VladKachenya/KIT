using System.Collections.Generic;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DaType;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.EnumType;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.LNodeType;

namespace BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates
{
    public interface IDataTypeTemplates:IModelElement
    {

        List<ILNodeType> LNodeTypes { get; }


        List<IDoType> DoTypes { get; }


        List<IDaType> DaTypes { get; }


        List<IEnumType> EnumTypes { get; }

      

    }
}