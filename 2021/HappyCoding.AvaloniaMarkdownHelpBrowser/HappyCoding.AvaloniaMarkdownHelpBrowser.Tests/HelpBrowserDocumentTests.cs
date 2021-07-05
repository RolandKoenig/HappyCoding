using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HappyCoding.AvaloniaMarkdownHelpBrowser.Tests
{
    [TestClass]
    public class HelpBrowserDocumentTests
    {
        [TestMethod]
        public void EmptyFile()
        {
            var document = new HelpBrowserDocument("Test", "");

            Assert.IsFalse(document.IsValid, document.ParseError);
        }

        [TestMethod]
        public void TitleFromTitleMarkup()
        {
            var document = new HelpBrowserDocument(
                "Test",
                "# DummyTitle" + Environment.NewLine +
                "Test content test content" + Environment.NewLine +
                "Test content test content");

            Assert.IsTrue(document.IsValid);
            Assert.AreEqual(document.YamlHeader.Title, "DummyTitle");
        }

        [TestMethod]
        public void TitleAndAuthorFromYamlHeader()
        {
            var document = new HelpBrowserDocument(
                "Test",
                "---" + Environment.NewLine +
                "title: DummyTitle" + Environment.NewLine +
                "author: RolandK" + Environment.NewLine + 
                "---" + Environment.NewLine +
                "Test content test content" + Environment.NewLine +
                "Test content test content");

            Assert.IsTrue(document.IsValid);
            Assert.AreEqual(document.YamlHeader.Title, "DummyTitle");
            Assert.AreEqual(document.YamlHeader.Author, "RolandK");
        }

        [TestMethod]
        public void TitleFromFileKey()
        {
            var document = new HelpBrowserDocument(
                "Test",
                "## DummyTitle");

            Assert.IsTrue(document.IsValid);
            Assert.AreEqual(document.YamlHeader.Title, "Test");
        }
    }
}
