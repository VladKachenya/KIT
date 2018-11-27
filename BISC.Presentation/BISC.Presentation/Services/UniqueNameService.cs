using System.Collections.Generic;
using BISC.Infrastructure.Global.Services;

namespace BISC.Presentation.Services
{
    public class UniqueNameService : IUniqueNameService
    {
        public string GetUniqueName(List<string> existingNames, string bodyOfName)
        {
            string nameBody = bodyOfName;
            string result;
            int i = 0;
            bool isFind;
            do
            {
                i++;
                result = nameBody + i.ToString();
                isFind = false;
                foreach (var element in existingNames)
                {
                    if (result == element)
                        isFind = true;
                }
            } while (isFind);

            return result;
        }
    }
}