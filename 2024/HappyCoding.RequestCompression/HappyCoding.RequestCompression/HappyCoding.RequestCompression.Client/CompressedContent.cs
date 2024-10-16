using System.IO.Compression;
using System.Net;

namespace HappyCoding.RequestCompression.Client;

// Based on 
// https://github.com/WebApiContrib/WebAPIContrib/blob/master/src/WebApiContrib/Content/CompressedContent.cs

public class CompressedContent : HttpContent
{
    private readonly HttpContent _originalContent;
    private readonly string _encodingType;

    public CompressedContent(HttpContent content, string encodingType)
    {
        ArgumentNullException.ThrowIfNull(content);
        ArgumentNullException.ThrowIfNull(encodingType);

        _originalContent = content;
        _encodingType = encodingType.ToLowerInvariant();

        if (_encodingType != "gzip" && this._encodingType != "deflate")
        {
            throw new InvalidOperationException($"Encoding '{this._encodingType}' is not supported. Only supports gzip or deflate encoding.");
        }

        foreach (var header in _originalContent.Headers)
        {
            Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        Headers.ContentEncoding.Add(encodingType);
    }

    protected override bool TryComputeLength(out long length)
    {
        length = -1;
        return false;
    }

    protected override Task SerializeToStreamAsync(Stream stream, TransportContext? context)
    {
        
        Stream compressedStream = _encodingType switch
        {
            "gzip" => new GZipStream(stream, CompressionMode.Compress, leaveOpen: true),
            "deflate" => new DeflateStream(stream, CompressionMode.Compress, leaveOpen: true),
            _ => throw new InvalidOperationException($"Unable to compress. Encoding type '{_encodingType}' is not handled!")
        };

        return _originalContent.CopyToAsync(compressedStream).ContinueWith(_ =>
        {
            compressedStream.Dispose();
        });
    }
}