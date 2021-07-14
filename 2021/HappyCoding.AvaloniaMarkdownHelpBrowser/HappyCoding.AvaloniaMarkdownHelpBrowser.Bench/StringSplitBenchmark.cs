using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using HappyCoding.AvaloniaMarkdownHelpBrowser.DocFramework;

namespace HappyCoding.AvaloniaMarkdownHelpBrowser.Bench
{
    [MemoryDiagnoser]
    public class StringSplitBenchmark
    {
        [Benchmark]
        public void SimpleCase_Normal()
        {
            foreach (var actSplitted in "Test1.Test2.Test3".Split('.'))
            {
               
            }
        }

        [Benchmark]
        public void SimpleCase_ZeroAlloc()
        {
            foreach (var actSplitted in "Test1.Test2.Test3".SplitZeroAlloc('.'))
            {
               
            }
        }
    }
}
