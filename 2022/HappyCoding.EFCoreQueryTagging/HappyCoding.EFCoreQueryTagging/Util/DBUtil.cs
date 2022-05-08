using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace HappyCoding.EFCoreQueryTagging.Util;

public static class DBUtil
{
    public static IQueryable<T> TagWithClassAndMethodName<T>(
        this IQueryable<T> queryable,
        [CallerFilePath] string filePath = "", 
        [CallerMemberName] string methodName = "")
    {
        if (string.IsNullOrEmpty(filePath) ||
            string.IsNullOrEmpty(methodName))
        {
            return queryable;
        }

        var className = Path.GetFileNameWithoutExtension(filePath);
        return queryable
            .TagWith($"{className}.{methodName}");
    }
}