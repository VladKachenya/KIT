using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Presentation.Infrastructure.Commands;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Presentation.Services
{
    public class UiCommandService:IUiCommandService
    {
        #region Implementation of IUiCommandService

        public void OnCommandExecute(ITrackableCommand trackableCommand)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
