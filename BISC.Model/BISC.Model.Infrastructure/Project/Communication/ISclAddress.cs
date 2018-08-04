using System.Collections.Generic;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Model.Infrastructure.Project.Communication
{
    public static class AddressTypes
    {
        public const string VlanId = "VLAN-ID";
        public const string VlanProirity = "VLAN-PRIORITY";
        public const string MacAddress = "MAC-Address";
        public const string AppId = "APPID";

    }
    public interface ISclAddress : IModelElement
    {
        
        List<IAddressProperty> AddressProperties { get; }
        void SetProperty(string addressPropertyName, string value);
        string GetProperty(string addressPropertyName);
    }
}