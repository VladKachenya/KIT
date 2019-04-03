using System.Text.RegularExpressions;

namespace BISC.Presentation.Infrastructure.Services
{
    public static class StaticStringValidationService
    {
       private static readonly Regex RegexForNameValidation = new Regex(@"^[a-zA-Z0-9_]*$");
       private static readonly Regex RegexForMacAddresValidation = new Regex(@"^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$");


        /// <summary>
        /// Checks the string against the pattern @"^[a-zA-Z0-9_]*$"
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool NameValidation(string name)
        {
            return RegexForNameValidation.Match(name).Success;
        }

        /// <summary>
        /// Checks the string against the pattern @"^[a-zA-Z0-9_]*$"
        /// </summary>
        /// <param macAddress="macAddress"></param>
        /// <returns></returns>
        public static bool MacAddressValidation(string macAddress)
        {
            return RegexForMacAddresValidation.Match(macAddress).Success;
        }
    }
}