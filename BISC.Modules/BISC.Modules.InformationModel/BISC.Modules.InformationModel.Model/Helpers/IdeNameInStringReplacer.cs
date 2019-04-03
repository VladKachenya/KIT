using System;

namespace BISC.Modules.InformationModel.Model.Helpers
{
    internal class IdeNameInStringReplacer
    {
        public string ReplaseIdeNameInStringWithExeption(string mainString, string replaceable, string substitute)
        {
            if (!Replase(ref mainString, replaceable, substitute))
            {
                throw new Exception($"Не удалось изменить {mainString}");
            }
            return mainString;
        }

        public string ReplaseIdeNameInStringWithoutExeption(string mainString, string replaceable, string substitute)
        {
            Replase(ref mainString, replaceable, substitute);
            return mainString;
        }

        private bool Replase(ref string mainString, string replaceable, string substitute)
        {
            if (string.IsNullOrWhiteSpace(mainString))
            {
                return false;
            }
            int index = mainString.IndexOf(replaceable);
            if (index == 0)
            {
                mainString = substitute + mainString.Substring(replaceable.Length);
                return true;
            }

            return false;
        }
    }
}