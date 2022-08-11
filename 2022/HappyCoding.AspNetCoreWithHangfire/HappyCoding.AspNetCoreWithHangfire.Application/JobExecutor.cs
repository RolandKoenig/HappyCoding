namespace HappyCoding.AspNetCoreWithHangfire.Application;

public class JobExecutor
{
    public static async Task ExecuteDummyJob1()
    {
        await Task.Delay(1000);

        Console.WriteLine("Job 1 executed");
    }

    public static async Task ExecuteDummyJob2()
    {
        await Task.Delay(5000);

        Console.WriteLine("Job 2 executed");
    }

    public static async Task ExecuteDummyRecuringJob()
    {
        Console.WriteLine("Recurring Job executed");
    }
}
