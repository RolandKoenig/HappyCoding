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
    public class HelpBrowserDocumentPathTests
    {
        [TestMethod]
        [DataRow(data1: "HappyCoding.HelpBrowser.Dummy.md", "HappyCoding.HelpBrowser")]
        [DataRow(data1: "HelpBrowser.Dummy.md", "HelpBrowser")]
        [DataRow(data1: "Dummy.md", "")]
        [DataRow(data1: "Dummy", "")]
        [DataRow(".Dummy", "")]
        [DataRow(data1: "", "")]
        public void GetEmbeddedResourceDirectory(string embeddedResourceName, string expectedDirectoryName)
        {
            var directoryName = HelpBrowserDocumentPath.GetEmbeddedResourceDirectory(embeddedResourceName);
            Assert.AreEqual(expectedDirectoryName, directoryName);
        }

        [TestMethod]
        [DataRow(data1: "HappyCoding.HelpBrowser.Dummy.md", "Dummy")]
        [DataRow(data1: "HelpBrowser.Dummy.md", "Dummy")]
        [DataRow(data1: "Dummy.md", "Dummy")]
        [DataRow(data1: "Dummy", "Dummy")]
        [DataRow(data1: "Dummy..md", "")]
        [DataRow(data1: "", "")]
        public void GetEmbeddedResourceFileNameWithoutExtension(string embeddedResourceName, string expectedFileName)
        {
            var fileName = HelpBrowserDocumentPath.GetEmbeddedResourceFileNameWithoutExtension(embeddedResourceName);
            Assert.AreEqual(expectedFileName, fileName);
        }
    }
}
