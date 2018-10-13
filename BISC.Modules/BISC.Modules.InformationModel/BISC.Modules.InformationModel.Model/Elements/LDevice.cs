using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture;
using BISC.Modules.InformationModel.Infrastucture.Elements;

namespace BISC.Modules.InformationModel.Model.Elements
{
    public class LDevice:ModelElement,ILDevice
    {
        public LDevice()
        {
            ElementName = InfoModelKeys.ModelKeys.LDeviceKey;
        }
        public string Inst { get; set; }
        public ChildModelProperty<ILogicalNodeZero> LogicalNodeZero =>new ChildModelProperty<ILogicalNodeZero>(this, InfoModelKeys.ModelKeys.LogicalNodeZeroKey);
        public ChildModelsList<ILogicalNode> LogicalNodes =>new ChildModelsList<ILogicalNode>(this, InfoModelKeys.ModelKeys.LogicalNodeKey);

        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (base.Equals(obj)) return false;
            if (!(obj is ILDevice)) return false;
            var element = obj as ILDevice;
            if (element.Inst != Inst) return false;
            return true;
        }

    }
}
