using System;
using BISC.Model.Iec61850Ed2.SclModelTemplates;
using BISC.Model.Iec61850Ed2.SclModelTemplates.Controls;

namespace BISC.Model.Iec61850Ed2.TreeHelpers
{
    public class StringHelper
    {
        //public static ObjectItemPath GetItemPathWithSlash(object currentObject)
        //{
        //    ObjectItemPath objectItemPath = new ObjectItemPath();
        //    if (currentObject.GetType() != typeof(tLDevice))
        //    {
        //        string path = GetAllParentsStrings(currentObject, objectItemPath);
        //        objectItemPath.SetPathString(path);
        //        return objectItemPath;
        //    }
        //    return null;
        //}

        //private static string GetAllParentsStrings(object obj, ObjectItemPath objectItemPath)
        //{
        //    if (obj is tLDevice)
        //    {
        //        objectItemPath.IedString = ((obj as tLDevice).Parent as tIED).name;
        //        objectItemPath.LDeviceString = (obj as tLDevice).name;
        //        return ((obj as tLDevice).Parent as tIED).name + (obj as tLDevice).name + "/";
        //    }
        //    if (obj is tAnyLN)
        //    {
        //        objectItemPath.LnOfObject = (obj as tAnyLN);
        //    }


        //    if (obj is INamableSclItem)
        //    {
        //        string parentStr = "";
        //        if (obj is IParentable)
        //        {
        //            parentStr = GetAllParentsStrings((obj as IParentable).Parent, objectItemPath);
        //            if (!parentStr.EndsWith("/") && !parentStr.EndsWith(".")) parentStr += ".";
        //        }


        //        if (obj is tReportControl)
        //        {
        //            if (!(obj as tReportControl).IsInDevice)
        //            {
        //                return parentStr;
        //            }
        //            if ((obj as tReportControl).buffered)
        //            {
        //                return parentStr + "BR." + (obj as INamableSclItem).name;
        //            }
        //            else
        //            {
        //                return parentStr + "RP." + (obj as INamableSclItem).name;
        //            }
        //        }


        //        return parentStr + (obj as INamableSclItem).name;
        //    }
        //    return String.Empty;
        //}
    }
}
