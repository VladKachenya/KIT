using BISC.Modules.FTP.Infrastructure.ViewModels;
using BISC.Presentation.BaseItems.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.FTP.FTPConnection.ViewModels
{
    public class FTPActionMassage : IFTPActionMessage
    {

        #region private filds
        #endregion

        public FTPActionMassage ()
        {
            CreationDateTime = DateTime.Now.ToString() + ": ";
        }

        #region Implementation of IFTPActionViewModel
        public bool? Status { get; set ; }
        public string Message { get; set; }
        public string CreationDateTime { get; }
        #endregion
    }
}
