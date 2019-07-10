using BISC.Modules.Gooses.Infrastructure.Factorys;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Model.Model;
using System.Globalization;

namespace BISC.Modules.Gooses.Model.Factorys
{
    public class GoCbFtpEntityFactory : IGoCbFtpEntityFactory
    {
        public IGoCbFtpEntity GetIGoCbFtpEntityFromGooseInputModelInfo(IGooseInputModelInfo gooseInputModelInfo)
        {
            IGoCbFtpEntity res = new GoCbFtpEntity();
            res.GoCbReference = gooseInputModelInfo.GocbRef;
            res.AppId = uint.Parse(gooseInputModelInfo.EmittingGse.Value.AppId, NumberStyles.HexNumber).ToString("D");
            res.ConfRev = gooseInputModelInfo.EmittingGooseControl.Value.ConfRev;
            return res;
        }

        public IGoCbFtpEntity GetGoCbFtpEntity(int indexOfGoose, string goCdRef, uint appId, int? confRev)
        {
            return new GoCbFtpEntity()
            {
                IndexOfGoose = indexOfGoose,
                GoCbReference = goCdRef,
                AppId = appId.ToString("D"),
                ConfRev = confRev
            };
        }

    }
}