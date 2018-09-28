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
        string Name { get; set; }

        string FullName { get;}

        string ElementName { get;}

        void SetModel(T model);

        T GetModel();

        Brush TypeColorBrush { get; }

    }
}
