using System;
using System.IO;
using CommandLine;

namespace HappyCoding.CmdInterface.UsingCommandLine
{
    internal static class TestUtilities
    {
        public static void WriteHelpToConsole<TCmdLineOptions>()
        {
            using var helpWriter = new StringWriter();
            var helpGenerator = new Parser(config =>
            {
                config.HelpWriter = helpWriter;
            });
            helpGenerator.ParseArguments<TCmdLineOptions>(new[] { "--help" });

            Console.WriteLine(helpWriter.ToString());
        }
    }
}
