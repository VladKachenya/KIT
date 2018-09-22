using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using Microsoft.Win32;

namespace BISC.Presentation.BaseItems.Common
{
   public static class FileHelper
    {

        public static Maybe<FileInfo> SelectFileToOpen(string windowTitle, string filter, bool multiselect = false)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            Maybe<FileInfo> fileInfoMaybe = new Maybe<FileInfo>();
            ofd.Multiselect = multiselect;
            ofd.Title = windowTitle;
            ofd.Filter = filter;
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == true)
            {
                foreach (var element in ofd.FileNames)
                    fileInfoMaybe.AddValue(new FileInfo(element));
            }
            return fileInfoMaybe;
        }


        public static Maybe<string> SelectFilePathToSave(string windowTitle, string defaultExtension, string filter, string initialName)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            Maybe<string> filepathMaybe = new Maybe<string>();
            dlg.Title = windowTitle;
            dlg.FilterIndex = 0;
            dlg.DefaultExt = defaultExtension;
            dlg.CheckPathExists = true;
            dlg.Filter = filter;
            dlg.OverwritePrompt = true;
            dlg.ValidateNames = true;
            dlg.FileName = initialName;
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                filepathMaybe.AddValue(dlg.FileName);
            }

            return filepathMaybe;
        }
    }
}
