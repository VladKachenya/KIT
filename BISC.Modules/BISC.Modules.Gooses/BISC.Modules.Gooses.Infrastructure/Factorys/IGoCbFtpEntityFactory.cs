﻿using BISC.Modules.Gooses.Infrastructure.Model.FTP;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;

namespace BISC.Modules.Gooses.Infrastructure.Factorys
{
    public interface IGoCbFtpEntityFactory
    {
        IGoCbFtpEntity GetIGoCbFtpEntityFromGooseInputModelInfo(IGooseInputModelInfo gooseInputModelInfo);
        IGoCbFtpEntity GetGoCbFtpEntity(int indexOfGoose, string goCdRef, uint appId, int? confRev);

    }
}