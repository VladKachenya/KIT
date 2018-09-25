using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Keys;
using BISC.Model.Infrastructure.Project.Communication;

namespace BISC.Model.Global.Model.Communication
{
   public class SclAddress:ModelElement, ISclAddress
    {
        public SclAddress()
        {
            ElementName = ModelKeys.SclAddressKey;
        }

        public ChildModelsList<IAddressProperty> AddressProperties=>new ChildModelsList<IAddressProperty>(this, ModelKeys.AddressPropertyKey);

        public void SetProperty(string addressPropertyName, string value)
        {
            var property=AddressProperties.FirstOrDefault((element => element.Type == addressPropertyName));
            if (property != null)
            {
                property.Value = value;
            }
            else
            {
                AddressProperties.Add(new AddressProperty()
                {
                    Type = addressPropertyName,
                    Value = value
                });
            }
        }

        public string GetProperty(string addressPropertyName)
        {
            var property = AddressProperties.FirstOrDefault((element => element.Type == addressPropertyName));
            if (property != null)
            {
                return property.Value;
            }
            return null;
        }
    }
}
