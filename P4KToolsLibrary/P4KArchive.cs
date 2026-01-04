using P4KToolsLibrary.DataStructures;
using P4KToolsLibrary.DataStructures.Zip32;
using P4KToolsLibrary.DataStructures.Zip64;

namespace P4KToolsLibrary;

public class P4KArchive : IDisposable
{
    
    private readonly Stream _stream;
    private readonly BinaryReader _reader;
    private readonly List<P4KEntry> _entries;

    private EndOfCentralDirectory64 _EOCD64;
    private EndOfCentralDirectory32 _EOCD32;
    private EndOfCentralDirectoryLocator _EOCDLocator;
    
    private bool _isDisposed;
    
    /// <summary>
    /// Initializes a new instance of P4KArchive for reading
    /// </summary>
    /// <exception cref="ArgumentNullException">The stream is null</exception>
    /// <exception cref="ArgumentException">The stream cannot be read or seeked</exception>
    /// <exception cref="InvalidDataException">
    /// The contents of the stream could not be interpreted as a valid p4k archive
    /// </exception>
    /// <param name="stream">The stream containing the archive to read</param>
    public P4KArchive(Stream stream)
    {
        ArgumentNullException.ThrowIfNull(stream);
        if (!stream.CanSeek || !stream.CanRead)
        {
            throw new ArgumentException("Stream must be seekable and readable");
        }
        
        _entries = new List<P4KEntry>();
        _stream = stream;
        _reader = new BinaryReader(stream);
        
        _initializeArchive();
        _initializeEntries();
    }

    private void _initializeArchive()
    {
        _reader.BaseStream.Seek(-MaxOffsets.EocdLocatorMaxOffset, SeekOrigin.End);
        //scan file for locator signature
        uint checkNumber = 0;
        const int seekBackBytes = (sizeof(uint) - 1) * -1;

        try
        {
            while (checkNumber != Signatures.EndOfCentralDirectoryLocator)
            {
                checkNumber = _reader.ReadUInt32();
                //seek back 3 bytes for next read
                _reader.BaseStream.Seek(seekBackBytes, SeekOrigin.Current);
            }
            //seek back 1 byte so the stream is in the correct spot for further reading
            _reader.BaseStream.Seek(-1, SeekOrigin.Current);
        }
        catch (EndOfStreamException)
        {
            throw new InvalidDataException("Could not find End Of Central Directory Locator signature");
        }
        //Locator found, read data from file

        _EOCDLocator = EndOfCentralDirectoryLocatorReader.Read(_reader);
        _EOCD32 = EndOfCentralDirectoryReader.Read(_reader);
            
        _reader.BaseStream.Seek((long)_EOCDLocator.Eocd64Offset, SeekOrigin.Begin);
        _EOCD64 = EndOfCentralDirectory64Reader.Read(_reader);
    }

    private void _initializeEntries()
    {
        _entries.Clear();
        //TODO - read central directory and create P4KEntries
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_isDisposed || !disposing)
        {
            return;
        }
        _isDisposed = true;

        _stream.Dispose();
        _reader.Dispose();
    }
}