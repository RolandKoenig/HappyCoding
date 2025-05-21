using System;
using System.Text;

namespace HappyCoding.AvaloniaImageViewer.Util;

public static class ThreadStaticStringBuilder
{
    [ThreadStatic] 
    private static StringBuilder? _instance;
    
    public static StringBuilder Instance => _instance ??= new StringBuilder(256);
}