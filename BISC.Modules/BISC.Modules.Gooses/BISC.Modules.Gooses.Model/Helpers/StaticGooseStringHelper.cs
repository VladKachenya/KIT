using BISC.Model.Infrastructure.Common;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.Elements;

namespace BISC.Modules.Gooses.Model.Helpers
{
    internal static class StaticGooseStringHelper
    {
        static object _getGooseControlReferenceLocker = new object();
        public static string GetGooseControlReference(IGooseControl gooseControl)
        {
            string str;
            lock (_getGooseControlReferenceLocker)
            {
                str = $"{gooseControl.GetFirstParentOfType<IDevice>().Name}" +
                      $"{gooseControl.GetFirstParentOfType<ILDevice>().Inst}/" +
                      $"{gooseControl.GetFirstParentOfType<ILogicalNode>().LnClass}$" +
                      $"GO${gooseControl.Name}";
            }
            return str;
        }
    }
}