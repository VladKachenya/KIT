using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Model.Infrastructure.Common
{
   public static class Extensions
    {
        public static bool TryGetFirstChildOfType<T>(this IModelElement modelElement, out T findedChild)
        {
            if (modelElement is T result)
            {
                findedChild = result;
                return true;
            }

            foreach (var childModelElement in modelElement.ChildModelElements)
            {
                if (childModelElement.TryGetFirstChildOfType(out findedChild))
                {
                    return true;
                }
            }

            findedChild = default(T);
            return false;
        }

        public static T GetFirstParentOfType<T>(this IModelElement modelElement, string elementName = null)
        {
            if (modelElement.ParentModelElement == null) return default(T);
            var currentModelElement = modelElement.ParentModelElement;
            if (!(currentModelElement is T))
            {
                return currentModelElement.GetFirstParentOfType<T>(elementName);
            }
            if (elementName != null&&currentModelElement.ElementName!=elementName)
            {
                return currentModelElement.GetFirstParentOfType<T>(elementName);
            }

            return (T)currentModelElement;
        }

        public static void GetAllChildrenOfType<T>(this IModelElement modelElement, ref List<T> findedChild)
        {
            
            if (modelElement is T result)
            {
                findedChild.Add(result);
            }

            foreach (var childModelElement in modelElement.ChildModelElements)
            {
                childModelElement.GetAllChildrenOfType<T>(ref findedChild);
            }
        }
    }
}
