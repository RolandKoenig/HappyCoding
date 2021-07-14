using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HappyCoding.AvaloniaMarkdownHelpBrowser.DocFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HappyCoding.AvaloniaMarkdownHelpBrowser.Tests
{
    [TestClass]
    public class StringSplitTests
    {
        [TestMethod]
        public void ThreeItems()
        {
            var results = new List<string>();
            foreach (var actSplitted in "Test1.Test2.Test3".SplitZeroAlloc('.'))
            {
                results.Add(actSplitted.ToString());
            }

            Assert.AreEqual(3, results.Count);
            Assert.AreEqual("Test1", results[0]);
            Assert.AreEqual("Test2", results[1]);
            Assert.AreEqual("Test3", results[2]);
        }

        [TestMethod]
        public void OneItem()
        {
            var results = new List<string>();
            foreach (var actSplitted in "Test1".SplitZeroAlloc('.'))
            {
                results.Add(actSplitted.ToString());
            }

            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Test1", results[0]);
        }

        [TestMethod]
        public void EmptyString()
        {
            var results = new List<string>();
            foreach (var actSplitted in "".SplitZeroAlloc('.'))
            {
                results.Add(actSplitted.ToString());
            }

            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("", results[0]);
        }

        [TestMethod]
        public void EmptyItem_Start()
        {
            var results = new List<string>();
            foreach (var actSplitted in ".Test2.Test3".SplitZeroAlloc('.'))
            {
                results.Add(actSplitted.ToString());
            }

            Assert.AreEqual(3, results.Count);
            Assert.AreEqual("", results[0]);
            Assert.AreEqual("Test2", results[1]);
            Assert.AreEqual("Test3", results[2]);
        }

        [TestMethod]
        public void EmptyItem_Middle()
        {
            var results = new List<string>();
            foreach (var actSplitted in "Test1..Test3".SplitZeroAlloc('.'))
            {
                results.Add(actSplitted.ToString());
            }

            Assert.AreEqual(3, results.Count);
            Assert.AreEqual("Test1", results[0]);
            Assert.AreEqual("", results[1]);
            Assert.AreEqual("Test3", results[2]);
        }

        [TestMethod]
        public void EmptyItem_Last()
        {
            var results = new List<string>();
            foreach (var actSplitted in "Test1.Test2.".SplitZeroAlloc('.'))
            {
                results.Add(actSplitted.ToString());
            }

            Assert.AreEqual(3, results.Count);
            Assert.AreEqual("Test1", results[0]);
            Assert.AreEqual("Test2", results[1]);
            Assert.AreEqual("", results[2]);
        }
    }
}
