﻿using BISC.Modules.Device.Infrastructure.Model;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using BISC.Modules.Device.Infrastructure.Model.Revision;

namespace BISC.Modules.Device.Model.Model
{
    internal class Revision : IRevision
    {
        private readonly int _revisionVersion;
        private readonly int _revisionSubversion;
        private readonly DateTime _revisionDateTime;

        public Revision(string revision)
        {
            if (string.IsNullOrWhiteSpace(revision))
            {
                return;
            }

            var rev = revision.Trim('R', 'e', 'v', '.', ' ', ')').Split('(', '.');

            if (rev.Length != 3)
            {
                return;
            }

            int.TryParse(rev[0], out _revisionVersion);
            int.TryParse(rev[1], out _revisionSubversion);
            DateTime.TryParseExact(Regex.Replace(rev[2], @"\s+", " "), "MMM d yyyy-HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault, out _revisionDateTime);
        }

        public int RevisionVersion => _revisionVersion;

        public int RevisionSubversion => _revisionSubversion;

        public DateTime RevisionDateTime => _revisionDateTime;

        #region implementation of IVersionComparable

        public int CompareVersionTo(int version, int subversion)
        {
            if (RevisionVersion > version) return 1;
            if (RevisionVersion < version) return -1;
            if (RevisionSubversion > subversion)return 1;
            if (RevisionSubversion < subversion) return -1;
            return 0;
        }

        public int CompareVersionTo(IRevision revision)
        {
            if (revision == null) return 1;
            return CompareVersionTo(revision.RevisionVersion, revision.RevisionSubversion);
        }
        #endregion

    }
}