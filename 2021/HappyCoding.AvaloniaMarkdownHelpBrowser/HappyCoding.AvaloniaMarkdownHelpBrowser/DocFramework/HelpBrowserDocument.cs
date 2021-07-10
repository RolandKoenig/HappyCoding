using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HappyCoding.AvaloniaMarkdownHelpBrowser.Util;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace HappyCoding.AvaloniaMarkdownHelpBrowser.DocFramework
{
    public class HelpBrowserDocument
    {
        public IHelpBrowserDocumentPath DocumentPath { get; }

        public string Title => this.YamlHeader.Title;

        public string YamlHeaderString { get; } = string.Empty;

        public string MarkdownContentString { get; } = string.Empty;

        public HelpBrowserDocumentHeader YamlHeader { get; }

        public bool IsValid { get; } = true;

        public string? ParseError { get; }

        public HelpBrowserDocument(IHelpBrowserDocumentPath documentPath)
        {
            this.DocumentPath = documentPath;

            // Start reading the document
            var fileContentReader = documentPath.OpenRead();

            // Check for content in the first line
            var firstLine = fileContentReader.ReadLine();
            if (firstLine == null)
            {
                this.IsValid = false;
                this.ParseError = "File is empty";
                return;
            }

            // Cut out the yaml header if present
            if (IsYamlHeaderSeparator(firstLine))
            {
                var strBuilderHeader = PooledStringBuilders.Current.TakeStringBuilder(1024);
                try
                {
                    var actLine = fileContentReader.ReadLine();
                    while (!IsYamlHeaderSeparator(actLine))
                    {
                        strBuilderHeader.AppendLine(actLine);
                        actLine = fileContentReader.ReadLine();
                    }
                    this.YamlHeaderString = strBuilderHeader.ToString();
                }
                finally
                {
                    PooledStringBuilders.Current.ReRegisterStringBuilder(strBuilderHeader);
                }

                firstLine = fileContentReader.ReadLine();
            }

            // Parse yaml header (if present)
            if (!string.IsNullOrEmpty(this.YamlHeaderString))
            {
                var yamlDeserializer = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .IgnoreUnmatchedProperties()
                    .Build();
                try
                {
                    this.YamlHeader = yamlDeserializer.Deserialize<HelpBrowserDocumentHeader>(this.YamlHeaderString);
                }
                catch (Exception e)
                {
                    this.IsValid = false;
                    this.ParseError = $"Unable to parse yaml header: {Environment.NewLine} {e}";
                    return;
                }
            }
            else
            {
                this.YamlHeader = new HelpBrowserDocumentHeader();
            }

            // Ensure we have a title
            if (string.IsNullOrEmpty(this.YamlHeader.Title))
            {
                if ((firstLine != null) &&
                    (firstLine.StartsWith("# ")) && 
                    (firstLine.Length > 2))
                {
                    this.YamlHeader.Title = firstLine[2..];
                }
                else
                {
                    this.YamlHeader.Title = documentPath.ToString() ?? string.Empty;
                }
            }

            // Read markdown content
            var strBuilderContent = PooledStringBuilders.Current.TakeStringBuilder(1024);
            try
            {
                if (firstLine != null) { strBuilderContent.AppendLine(firstLine); }

                var actLine = fileContentReader.ReadLine();
                while (actLine != null)
                {
                    strBuilderContent.AppendLine(actLine);
                    actLine = fileContentReader.ReadLine();
                }

                this.MarkdownContentString = strBuilderContent.ToString();
            }
            finally
            {
                PooledStringBuilders.Current.ReRegisterStringBuilder(strBuilderContent);
            }
        }

        /// <inheritdoc />
        public override string ToString() => this.Title;

        public static bool IsYamlHeaderSeparator(string? strToCheck)
        {
            if (string.IsNullOrEmpty(strToCheck)) { return false; }
            if (!strToCheck.StartsWith("---")) { return false; }

            if (strToCheck.Length > 3)
            {
                for (var loop = 3; loop < strToCheck.Length; loop++)
                {
                    if (strToCheck[loop] != ' ') { return false; }
                }
            }

            return true;
        }
    }
}
