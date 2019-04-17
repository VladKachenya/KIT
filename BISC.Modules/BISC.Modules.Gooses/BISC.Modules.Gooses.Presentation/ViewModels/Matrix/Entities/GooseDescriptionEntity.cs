using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Matrix.Entities
{
    public class GooseDescriptionEntity
    {
        public IGooseInputModelInfo GooseInputModelInfo { get; }
        public IDevice ParientDevice { get; }



        public GooseDescriptionEntity(IGooseInputModelInfo gooseInputModelInfo, IDevice parientDevice)
        {
            GooseInputModelInfo = gooseInputModelInfo;
            ParientDevice = parientDevice;
        }

        public override string ToString()
        {
            return $"{GooseInputModelInfo.EmittingDeviceName}.{GooseInputModelInfo.EmittingGooseControl.Value.Name}({GooseInputModelInfo.EmittingGooseControl.Value.ConfRev})";
        }

    }
}
