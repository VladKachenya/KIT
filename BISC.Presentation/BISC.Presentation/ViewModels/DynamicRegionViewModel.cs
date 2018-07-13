using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Presentation.BaseItems.ViewModels;

namespace BISC.Presentation.ViewModels
{
    public class DynamicRegionViewModel:ViewModelBase
    {
        private Guid _dynamicRegionId;

        public Guid DynamicRegionId
        {
            get => _dynamicRegionId;
            set
            {
                _dynamicRegionId = value;
                OnPropertyChanged();
            }
        }
    }
}
