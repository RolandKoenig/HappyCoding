using System.Threading.Tasks;

namespace HappyCoding.ConsoleLogWindow.Gui.Util;

internal static class CommonExtensions
{
    public static async void FireAndForget(this Task task)
    {
        await task;
    }
}