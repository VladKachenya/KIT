using System;
using BISC.Model.Iec61850Ed2.SclModelTemplates;

namespace BISC.Model.Iec61850Ed2.TreeHelpers
{
    public class ObjectItemPath
    {
        public void SetPathString(string pathStringWithSlash)
        {
            _pathStringWithSlash = pathStringWithSlash;
        }
        private string _pathStringWithSlash;
        public string LDeviceString { get; set; }
        public string IedString{ get; set; }
        public tAnyLN LnOfObject { get; set; }

        public string ToString(bool isWithSlash)
        {
            return isWithSlash ? _pathStringWithSlash : _pathStringWithSlash.Replace("/", ".");
        }

        public string ToStringWithoutIed()
        {
            return _pathStringWithSlash.Replace(IedString, String.Empty);
        }

    }
}