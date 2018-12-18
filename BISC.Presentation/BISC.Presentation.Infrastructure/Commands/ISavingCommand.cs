using BISC.Infrastructure.Global.Common;
using System.Threading.Tasks;

namespace BISC.Presentation.Infrastructure.Commands
{
	public interface ISavingCommand
	{
	
		Task<OperationResult<SavingCommandResultEnum>> SaveAsync();
		Task<bool> IsSavingByFtpNeeded();
		Task<OperationResult> ValidateBeforeSave();
	}


}