namespace HappyCoding.WinProcessSignalingHelper;


internal class Program
{
    static int Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Invalid argument count!");
            return -1;
        }

        if (!uint.TryParse(args[0], out var processID))
        {
            Console.WriteLine(" Unable to parse argument processID!");
            return -1;
        }

        // Method for windows: https://stackoverflow.com/questions/283128/how-do-i-send-ctrlc-to-a-process-in-c
        NativeMethods.FreeConsole();
        if (NativeMethods.AttachConsole(processID)) 
        {
            NativeMethods.SetConsoleCtrlHandler(null, true);
            try
            {
                if (!NativeMethods.GenerateConsoleCtrlEvent(NativeMethods.CTRL_C_EVENT, 0))
                {
                    Console.WriteLine($"Unable send close event to process {processID}!");
                    return -1;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Unable send close event to process {processID}, Exception: {ex}");
                return -1;
            }
        }
        else
        {
            Console.WriteLine($"Unable to attach to console of process {processID}!");
            return -1;
        }

        return 0;
    }
}
