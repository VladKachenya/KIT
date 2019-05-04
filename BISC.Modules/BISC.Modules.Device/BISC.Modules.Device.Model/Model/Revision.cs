using System;
using System.Globalization;
using System.Text.RegularExpressions;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.Device.Model.Model
{
    internal class Revision : IRevision
    {
        private readonly int _revisionVersion;
        private readonly int _revisionSubversion;
        private readonly DateTime _revisionDateTime;

        public Revision(string revision)
        {
            if(string.IsNullOrWhiteSpace(revision)) return;

            var rev = revision.Trim('R', 'e', 'v', '.', ' ', ')').Split('(', '.');

            if(rev.Length != 3) return;

            int.TryParse(rev[0], out _revisionVersion);
            int.TryParse(rev[1], out _revisionSubversion);
            DateTime.TryParseExact(Regex.Replace(rev[2], @"\s+", " "), "MMM d yyyy-HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault, out _revisionDateTime);
        }

        public int RevisionVersion => _revisionVersion;

        public int RevisionSubversion => _revisionSubversion;

        public DateTime RevisionDateTime => _revisionDateTime;
    }
}