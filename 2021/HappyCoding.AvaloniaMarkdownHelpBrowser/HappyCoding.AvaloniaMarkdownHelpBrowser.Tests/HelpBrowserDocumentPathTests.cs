using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        [TestMethod]
        [DataRow("HelpBrowser.Dummy.md", "Dummy2.md", "HelpBrowser.Dummy2.md")]
        [DataRow("HelpBrowser.Dummy.md", "./Dummy2.md", "HelpBrowser.Dummy2.md")]
        [DataRow("HelpBrowser.Dummy.md", ".\\Dummy2.md", "HelpBrowser.Dummy2.md")]
        [DataRow("HelpBrowser.Dummy.md", "Subfolder/Dummy2.md", "HelpBrowser.Subfolder.Dummy2.md")]
        [DataRow("HelpBrowser.Dummy.md", "Subfolder\\Dummy2.md", "HelpBrowser.Subfolder.Dummy2.md")]
        [DataRow("HelpBrowser.Dummy.md", "Subfolder.Dummy2.md", "HelpBrowser.Subfolder.Dummy2.md")]
        [DataRow("HelpBrowser.Dummy.md", "../Dummy2.md", "Dummy2.md")]
        public void FollowLocalPath_GoodCases(string initialResource, string localPath, string targetResource)
        {
            var initialPath = new HelpBrowserDocumentPath(
                Assembly.GetExecutingAssembly(),
                initialResource);
            var targetPath = initialPath.FollowLocalPath(localPath);

            Assert.AreEqual(targetResource, targetPath.EmbeddedResourceName);
        }

        [TestMethod]
        [DataRow("HelpBrowser.Dummy.md", "../../Dummy2.md", "ArgumentException")]
        public void FollowLocalPath_Errors(string initialResource, string localPath, string exceptionType)
        {
            Exception? catchedException = null;
            try
            {
                var initialPath = new HelpBrowserDocumentPath(
                    Assembly.GetExecutingAssembly(),
                    initialResource);
                initialPath.FollowLocalPath(localPath);
            }
            catch (Exception e)
            {
                catchedException = e;
            }

            Assert.IsNotNull(catchedException, "No exception thrown!");
            Assert.AreEqual(exceptionType, catchedException.GetType().Name);
        }
    }
}
