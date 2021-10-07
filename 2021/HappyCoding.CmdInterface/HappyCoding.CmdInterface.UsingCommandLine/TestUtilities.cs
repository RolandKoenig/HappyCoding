using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

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
