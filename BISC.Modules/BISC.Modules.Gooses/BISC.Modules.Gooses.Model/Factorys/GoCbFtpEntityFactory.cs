using BISC.Modules.Gooses.Infrastructure.Factorys;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Model.Model;

namespace BISC.Modules.Gooses.Model.Factorys
{
    public class GoCbFtpEntityFactory : IGoCbFtpEntityFactory
    {
        public IGoCbFtpEntity GetIGoCbFtpEntityFromGooseInputModelInfo(IGooseInputModelInfo gooseInputModelInfo)
        {
            IGoCbFtpEntity res = new GoCbFtpEntity();
            res.GoCbReference = gooseInputModelInfo.GocbRef;
            res.AppId = gooseInputModelInfo.EmittingGse.Value.AppId;
            res.ConfRev = gooseInputModelInfo.EmittingGooseControl.Value.ConfRev;
            return res;
        }
    }
}