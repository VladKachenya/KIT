using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project.Communication;
using BISC.Modules.DataSets.Infrastructure.Model;

namespace BISC.Modules.Gooses.Infrastructure.Model.FTP
{
	public interface IGooseInputModelInfo:IModelElement
	{
		string EmittingDeviceName { get; set; }
	    string GocbRef { get; set; }

        ChildModelProperty<IGooseControl> EmittingGooseControl { get; }
		ChildModelProperty<IGse> EmittingGse { get; }
		ChildModelProperty<IDataSet> EmittingDataSet { get;  }

	}
}