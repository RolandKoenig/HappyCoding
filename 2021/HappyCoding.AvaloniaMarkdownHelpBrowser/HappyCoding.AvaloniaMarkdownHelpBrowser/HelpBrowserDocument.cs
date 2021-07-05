using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HappyCoding.AvaloniaMarkdownHelpBrowser.Util;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace HappyCoding.AvaloniaMarkdownHelpBrowser
{
    public class HelpBrowserDocument
    {
        public string FileKey { get; }

        public string YamlHeaderString { get; } = string.Empty;

        public string MarkdownContentString { get; } = string.Empty;

        public HelpBrowserDocumentHeader YamlHeader { get; } = HelpBrowserDocumentHeader.Empty;

        public bool IsValid { get; } = true;

        public string? ParseError { get; }

        public HelpBrowserDocument(string fileKey, TextReader fileContentReader)
        {
            this.FileKey = fileKey;

            // Read yaml header
            var firstLine = fileContentReader.ReadLine();
            if (string.IsNullOrWhiteSpace(firstLine))
            {
                this.IsValid = false;
                this.ParseError = "File is empty";
                return;
            }
            else if (IsYamlHeaderSeparator(firstLine))
            {
                firstLine = null;

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
            }
            else if (firstLine.StartsWith("# ") && (firstLine.Length > 2))
            {
                this.YamlHeaderString = $"title: {firstLine[2..]}";
            }
            else
            {
                this.YamlHeaderString = $"title: {fileKey}";
            }

            // Parse yaml header
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
