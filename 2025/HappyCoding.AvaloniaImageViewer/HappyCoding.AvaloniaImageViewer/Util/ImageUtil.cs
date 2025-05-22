using System;
using Avalonia.Platform.Storage;

namespace HappyCoding.AvaloniaImageViewer.Util;

public static class ImageUtil
{
    public static bool IsSupportedImageFormat(IStorageFile file)
    {
        var filePath = file.Path.LocalPath;
        
        foreach(var actSupportedFormat in ImageViewerConstants.SUPPORTED_IMAGE_FORMATS)
        {
            if (filePath.EndsWith(actSupportedFormat, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        
        return false;
    }
}