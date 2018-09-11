using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.FTP.Infrastructure.Model.Loaders
{
    public interface IFileLoader
    {
        byte[] LoadFileData(string filePath);
        void SetDataProvider(object dataprovider);
    }
}
