using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace HappyCoding.CmdInterface.UsingCommandLine
{
    internal class BasicScenariosOptions
    {
        [Value(
            0, Required = true, 
            HelpText = "Target file name")]
        public string TargetFile { get; set; }

        [Option(
            'f', "force", Default = false,
            HelpText = "Force processing of the file")]
        public bool Force { get; set; }

        [Option(
            'v', "verbose", Default = false,
            HelpText = "Write all debug messages to standard output")]
        public bool Verbose { get; set; }
    }
}
