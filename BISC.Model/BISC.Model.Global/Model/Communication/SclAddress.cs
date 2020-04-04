using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Keys;
using BISC.Model.Infrastructure.Project.Communication;
using System.Linq;

namespace BISC.Model.Global.Model.Communication
{
    public class SclAddress : ModelElement, ISclAddress
    {
        public SclAddress()
        {
            ElementName = InfrastructureKeys.ModelKeys.SclAddressKey;
        }

        public ChildModelsList<IAddressProperty> AddressProperties => new ChildModelsList<IAddressProperty>(this, InfrastructureKeys.ModelKeys.AddressPropertyKey);

        public void SetProperty(string addressPropertyName, string value)
        {
            var property = AddressProperties.FirstOrDefault((element => element.Type == addressPropertyName));
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
        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (!base.ModelElementCompareTo(obj))
            {
                return false;
            }

            if (!(obj is ISclAddress))
            {
                return false;
            }

            var element = obj as ISclAddress;
            return true;
        }


    }
}
