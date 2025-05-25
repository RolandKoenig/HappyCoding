using System;
using System.IO;

namespace HappyCoding.AvaloniaImageViewer.Util;

public class PathUtil
{
    public static string GetFileName(Uri uri, int maxLength = 40)
    {
        var path = uri.LocalPath;
        var fileName = Path.GetFileName(path);
        
        if (fileName.Length > maxLength)
        {
            return "..." + fileName.Substring(0, maxLength - 3);
        }
        return fileName;
    }
}