using System;
using BenchmarkDotNet.Running;

namespace HappyCoding.AvaloniaMarkdownHelpBrowser.Bench
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run(typeof(Program).Assembly);
        }
    }
}
