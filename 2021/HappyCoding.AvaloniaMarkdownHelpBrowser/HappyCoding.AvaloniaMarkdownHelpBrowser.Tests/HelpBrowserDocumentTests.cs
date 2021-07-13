using System;
using System.IO;
using System.Reflection;
using HappyCoding.AvaloniaMarkdownHelpBrowser.DocFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HappyCoding.AvaloniaMarkdownHelpBrowser.Tests
{
    [TestClass]
    public class HelpBrowserDocumentTests
    {
        [TestMethod]
        public void EmptyFile()
        {
            var document = new HelpBrowserDocument(new HelpBrowserDocumentPathMock(
                "Test", 
                ""));

            Assert.IsFalse(document.IsValid, document.ParseError);
        }

        [TestMethod]
        public void TitleFromTitleMarkup()
        {
            var document = new HelpBrowserDocument(new HelpBrowserDocumentPathMock(
                "Test",
                "# DummyTitle" + Environment.NewLine +
                "Test content test content" + Environment.NewLine +
                "Test content test content"));

            Assert.IsTrue(document.IsValid);
            Assert.AreEqual("DummyTitle", document.YamlHeader.Title);
        }

        [TestMethod]
        public void TitleAndAuthorFromYamlHeader()
        {
            var document = new HelpBrowserDocument(new HelpBrowserDocumentPathMock(
                "Test",
                "---" + Environment.NewLine +
                "title: DummyTitle" + Environment.NewLine +
                "author: RolandK" + Environment.NewLine + 
                "---" + Environment.NewLine +
                "Test content test content" + Environment.NewLine +
                "Test content test content"));

            Assert.IsTrue(document.IsValid);
            Assert.AreEqual("DummyTitle", document.YamlHeader.Title);
            Assert.AreEqual("RolandK", document.YamlHeader.Author);
        }

        [TestMethod]
        public void TitleFromTitleMarkupAfterYamlHeader()
        {
            var document = new HelpBrowserDocument(new HelpBrowserDocumentPathMock(
                "Test",
                "---" + Environment.NewLine +
                "author: RolandK" + Environment.NewLine + 
                "---" + Environment.NewLine +
                "# DummyTitle" + Environment.NewLine +
                "Test content test content" + Environment.NewLine +
                "Test content test content"));

            Assert.IsTrue(document.IsValid);
            Assert.AreEqual("DummyTitle", document.YamlHeader.Title);
            Assert.AreEqual("RolandK", document.YamlHeader.Author);
        }

        [TestMethod]
        public void TitleFromFileKey()
        {
            var document = new HelpBrowserDocument(new HelpBrowserDocumentPathMock(
                "Test",
                "## DummyTitle"));

            Assert.IsTrue(document.IsValid);
            Assert.AreEqual("Test", document.YamlHeader.Title);
        }

        //*********************************************************************
        //*********************************************************************
        //*********************************************************************
        private record HelpBrowserDocumentPathMock(
            string FileName,
            string FileContent) : IHelpBrowserDocumentPath
        {
            public string EmbeddedResourceDirectory => string.Empty;

            public Assembly HostAssembly => Assembly.GetExecutingAssembly();

            /// <inheritdoc />
            public override string ToString()
            {
                return this.FileName;
            }

            /// <inheritdoc />
            public TextReader OpenRead()
            {
                return new StringReader(this.FileContent);
            }

            public HelpBrowserDocumentPath FollowLocalPath(string localFileSystemPath)
            {
                throw new NotImplementedException();
            }
        }
    }
}
