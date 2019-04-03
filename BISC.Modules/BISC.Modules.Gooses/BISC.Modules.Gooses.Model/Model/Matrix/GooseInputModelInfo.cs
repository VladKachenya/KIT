using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project.Communication;
using BISC.Modules.DataSets.Infrastructure.Keys;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;

namespace BISC.Modules.Gooses.Model.Model.Matrix
{
	public class GooseInputModelInfo: ModelElement,IGooseInputModelInfo
	{
	    public GooseInputModelInfo()
	    {
	        ElementName = GooseKeys.GooseModelKeys.GooseInputModelInfoKey;

        }
        public string EmittingDeviceName { get; set; }
	    public string GocbRef { get; set; }
        public ChildModelProperty<IGooseControl> EmittingGooseControl =>new ChildModelProperty<IGooseControl>(this,GooseKeys.GooseModelKeys.GooseControlKey);
		public ChildModelProperty<IGse> EmittingGse=> new ChildModelProperty<IGse>(this,"GSE");
		public ChildModelProperty<IDataSet> EmittingDataSet=>new ChildModelProperty<IDataSet>(this,DatasetKeys.DatasetModelKeys.DataSetModelKey);

	    public override bool ModelElementCompareTo(IModelElement obj)
	    {
	        if (!base.ModelElementCompareTo(obj)) return false;
	        if (!(obj is IGooseInputModelInfo)) return false;
	        var element = obj as IGooseInputModelInfo;
	        if (element.EmittingDeviceName != EmittingDeviceName) return false;
	        if (element.GocbRef != GocbRef) return false;
            return true;
	    }
    }
}
