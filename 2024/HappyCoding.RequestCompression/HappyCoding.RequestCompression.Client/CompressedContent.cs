using System.Net;

namespace HappyCoding.RequestCompression.Client;

// Based on 
// https://github.com/WebApiContrib/WebAPIContrib/blob/master/src/WebApiContrib/Content/CompressedContent.cs

public class CompressedContent : HttpContent
{
    private readonly HttpContent _originalContent;
    private readonly CompressedContentEncoding _encodingType;

    public CompressedContent(HttpContent content, CompressedContentEncoding encodingType)
    {
        ArgumentNullException.ThrowIfNull(content);
        ArgumentNullException.ThrowIfNull(encodingType);

        _originalContent = content;
        _encodingType = encodingType;

        foreach (var header in _originalContent.Headers)
        {
            Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        Headers.ContentEncoding.Add(encodingType.EncodingName);
    }

    protected override bool TryComputeLength(out long length)
    {
        length = -1;
        return false;
    }

    protected override Task SerializeToStreamAsync(Stream stream, TransportContext? context)
    {
        var compressedStream = _encodingType.WrapStream(stream);

        return _originalContent.CopyToAsync(compressedStream).ContinueWith(_ =>
        {
            compressedStream.Dispose();
        });
    }
}