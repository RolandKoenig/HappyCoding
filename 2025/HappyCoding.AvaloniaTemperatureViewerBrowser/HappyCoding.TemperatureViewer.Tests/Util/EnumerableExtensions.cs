namespace HappyCoding.TemperatureViewer.Tests.Util;

public static class EnumerableExtensions
{
    public static async IAsyncEnumerable<T> ToAsyncEnumerable<T>(this IEnumerable<T> source)
    {
        foreach (var actElement in source)
        {
            yield return actElement;
        }

        await Task.CompletedTask;
    }
}