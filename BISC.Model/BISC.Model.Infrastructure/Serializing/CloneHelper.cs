using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Model.Infrastructure.Serializing
{
    public static class CloneHelper
    {
        public static T DeepClone<T>(this T modelElement) where T : IModelElement
        {
            var serialized = StaticContainer.CurrentContainer.ResolveType<IModelElementsRegistryService>()
                  .SerializeModelElement(modelElement, SerializingType.Extended, true);
            return StaticContainer.CurrentContainer.ResolveType<IModelElementsRegistryService>()
                .DeserializeModelElement<T>(serialized);
        }
    }
}
