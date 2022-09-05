using System.Runtime.InteropServices;

namespace HappyCoding.WinProcessSignalingHelper;

// Original implementation from:
// https://stackoverflow.com/questions/283128/how-do-i-send-ctrlc-to-a-process-in-c

internal class NativeMethods
{
    internal const int CTRL_C_EVENT = 0;

    [DllImport("kernel32.dll")]
    internal static extern bool GenerateConsoleCtrlEvent(uint dwCtrlEvent, uint dwProcessGroupId);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern bool AttachConsole(uint dwProcessId);

    [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
    internal static extern bool FreeConsole();

    [DllImport("kernel32.dll")]
    internal static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate? handlerRoutine, bool Add);

    // Delegate type to be used as the Handler Routine for SCCH
    internal delegate Boolean ConsoleCtrlDelegate(uint ctrlType);
}
