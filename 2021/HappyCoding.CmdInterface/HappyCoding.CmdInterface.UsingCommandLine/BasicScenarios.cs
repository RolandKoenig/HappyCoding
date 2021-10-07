using System;
using System.IO;
using CommandLine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HappyCoding.CmdInterface.UsingCommandLine
{
    [TestClass]
    public class BasicScenarios
    {
        [TestMethod]
        public void WriteHelp()
        {
            TestUtilities.WriteHelpToConsole<BasicScenariosOptions>();
        }

        [TestMethod]
        public void FullArgumentList_1()
        {
            var args = new[]
            {
                "DummyFile.txt",
                "--force",
                "--verbose"
            };

            Parser.Default.ParseArguments<BasicScenariosOptions>(args)
                .WithParsed(opt =>
                {
                    Assert.AreEqual("DummyFile.txt", opt.TargetFile);
                    Assert.IsTrue(opt.Force);
                    Assert.IsTrue(opt.Verbose);
                });
        }

        [TestMethod]
        public void FullArgumentList_2()
        {
            var args = new[]
            {
                "DummyFile.txt",
                "-fv"
            };

            Parser.Default.ParseArguments<BasicScenariosOptions>(args)
                .WithParsed(opt =>
                {
                    Assert.AreEqual("DummyFile.txt", opt.TargetFile);
                    Assert.IsTrue(opt.Force);
                    Assert.IsTrue(opt.Verbose);
                });
        }

        [TestMethod]
        public void FullArgumentList_3()
        {
            var args = new[]
            {
                "DummyFile.txt",
                "-f",
                "-v"
            };

            Parser.Default.ParseArguments<BasicScenariosOptions>(args)
                .WithParsed(opt =>
                {
                    Assert.AreEqual("DummyFile.txt", opt.TargetFile);
                    Assert.IsTrue(opt.Force);
                    Assert.IsTrue(opt.Verbose);
                });
        }

        [TestMethod]
        public void OnlyRequiredArgument()
        {
            var args = new[]
            {
                "DummyFile.txt"
            };

            Parser.Default.ParseArguments<BasicScenariosOptions>(args)
                .WithParsed(opt =>
                {
                    Assert.AreEqual("DummyFile.txt", opt.TargetFile);
                    Assert.IsFalse(opt.Force);
                    Assert.IsFalse(opt.Verbose);
                });
        }
    }
}
