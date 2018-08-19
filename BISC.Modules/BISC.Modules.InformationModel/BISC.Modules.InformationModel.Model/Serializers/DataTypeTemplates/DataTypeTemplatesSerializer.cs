using System;
using System.Collections.Generic;
using System.Xml.Linq;
using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Global.Common;
using BISC.Model.Global.Model;
using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates;
using BISC.Modules.InformationModel.Model.DataTypeTemplates.DaType;
using BISC.Modules.InformationModel.Model.DataTypeTemplates.DoType;
using BISC.Modules.InformationModel.Model.DataTypeTemplates.EnumType;
using BISC.Modules.InformationModel.Model.DataTypeTemplates.LNodeType;

namespace BISC.Modules.InformationModel.Model.Serializers.DataTypeTemplates
{
    public class DataTypeTemplatesSerializer : DefaultModelElementSerializer<IDataTypeTemplates>
    {

        public DataTypeTemplatesSerializer( )
        {
            this.RegisterModelElementCollection(typeof(LNodeType));
            this.RegisterModelElementCollection(typeof(DoType));
            this.RegisterModelElementCollection(typeof(DaType));
            this.RegisterModelElementCollection(typeof(EnumType));
        }
        public override IModelElement GetConcreteObject()
        {
            return new InformationModel.Model.DataTypeTemplates.DataTypeTemplates();
        }


    }
}
