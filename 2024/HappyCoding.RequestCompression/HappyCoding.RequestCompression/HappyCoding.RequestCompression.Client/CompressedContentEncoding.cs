using System.IO.Compression;

namespace HappyCoding.RequestCompression.Client;

public class CompressedContentEncoding
{
    private readonly Func<Stream, Stream> _compressor;

    public string EncodingName { get; }

    public static readonly CompressedContentEncoding Deflate = 
        new ("deflate", stream => new ZLibStream(stream, CompressionMode.Compress, leaveOpen: true));

    public static readonly CompressedContentEncoding GZip = 
        new ("gzip", stream => new GZipStream(stream, CompressionMode.Compress, leaveOpen: true));

    public static readonly CompressedContentEncoding Brotli = 
        new ("br", stream => new BrotliStream(stream, CompressionMode.Compress, leaveOpen: true));

    private CompressedContentEncoding(string encodingName, Func<Stream, Stream> compressor)
    {
        EncodingName = encodingName;
        _compressor = compressor;
    }

    internal Stream WrapStream(Stream stream) => _compressor(stream);
}