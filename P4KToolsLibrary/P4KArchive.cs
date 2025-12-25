namespace P4KToolsLibrary;

public class P4KArchive : IDisposable
{
    //TODO: implement this
    
    private readonly Stream _stream;
    private readonly BinaryReader _reader;
    private readonly List<P4KEntry> _entries;
    
    private bool _isDisposed;
    
    /// <summary>
    /// Initializes a new instance of P4KArchive for reading
    /// </summary>
    /// <param name="stream">The stream containing the archive to read</param>
    /// <exception cref="ArgumentNullException">The stream is null</exception>
    /// <exception cref="InvalidDataException">The contents of the stream could not be interpreted as a valid p4k archive</exception>
    public P4KArchive(Stream stream)
    {
        ArgumentNullException.ThrowIfNull(stream);
        
        _entries = new List<P4KEntry>();
        _stream = stream;
        _reader = new BinaryReader(stream);
    }

    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }
        _isDisposed = true;

        _stream.Dispose();
        _reader.Dispose();
    }
}