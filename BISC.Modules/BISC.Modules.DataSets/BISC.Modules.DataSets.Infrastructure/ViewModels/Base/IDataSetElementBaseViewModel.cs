using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BISC.Modules.DataSets.Infrastructure.ViewModels.Base
{
    public interface IDataSetElementBaseViewModel <T>
    {
        string ElementName { get;}
        void SetModel(T model);
        T GetModel();
        bool IsEditeble { get; }
        Brush TypeColorBrush { get; }

    }
}
