using System.Collections.Generic;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplate.TemplatesBase;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DaType;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.EnumType;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.LNodeType;

namespace BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates
{
    public interface IDataTypeTemplates:IModelElement, IParentOfIds
    {

        ChildModelsList<ILNodeType> LNodeTypes { get; }


        ChildModelsList<IDoType> DoTypes { get; }


        ChildModelsList<IDaType> DaTypes { get; }


        ChildModelsList<IEnumType> EnumTypes { get; }

      

    }
}