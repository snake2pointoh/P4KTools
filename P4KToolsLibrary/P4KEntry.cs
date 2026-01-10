using System.IO.Compression;
using P4KToolsLibrary.DataStructures;
using P4KToolsLibrary.DataStructures.Zip64;
using ZstdSharp;

namespace P4KToolsLibrary;

public class P4KEntry
{
    private readonly P4KArchive _archive;
    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly CentralDirectory64 _centralDirectory64;
    private readonly LocalHeader64 _localHeader64;
    private readonly long _positionOfDataInSuperStream;
    private readonly string _fileName;
    
    public string FilePath
    {
        get
        {
            return _localHeader64.LocalHeader32.FileName;
        }
    }

    public string FileName
    {
        get
        {
            return _fileName;
        }
    }

    public ulong CompressedSize
    {
        get
        {
            return _localHeader64.ExtendedData64.CompressedSize;
        }
    }
    
    public ulong UncompressedSize
    {
        get
        {
            return _localHeader64.ExtendedData64.UncompressedSize;
        }
    }
    
    internal P4KEntry(P4KArchive archive, BinaryReader archiveReader)
    {
        _archive = archive;
        
        // read central directory from provided reader
        _centralDirectory64 = CentralDirectory64Reader.Read(archiveReader);
        //save old position
        long oldPosition = archiveReader.BaseStream.Position;
        
        // read local header from reader
        archiveReader.BaseStream.Seek((long)_centralDirectory64.ExtendedData64.LocalHeaderOffset, SeekOrigin.Begin);
        _localHeader64 = LocalHeader64Reader.Read(archiveReader);
        
        _positionOfDataInSuperStream = archiveReader.BaseStream.Position;
        
        // reset reader position
        archiveReader.BaseStream.Position = oldPosition;
        
        // set fileName
        int index = _localHeader64.LocalHeader32.FileName.LastIndexOf('\\');
        _fileName = _localHeader64.LocalHeader32.FileName.Substring(index + 1);
    }

    public Stream GetDecompressionStream()
    {
        switch (_localHeader64.LocalHeader32.CompressionMethod)
        {
            case CompressionTypes.None:
                return _getRawStream();
            case CompressionTypes.Zstd:
                return new DecompressionStream(_getRawStream());
            default:
                throw new NotSupportedException("Unknown compression method");
        };
    }
    
    private P4KSubReadOnlyStream _getRawStream()
    {
        P4KSubReadOnlyStream readOnlyStream = new P4KSubReadOnlyStream(_archive.ArchiveStream, _positionOfDataInSuperStream, (long) _localHeader64.ExtendedData64.CompressedSize);
        return readOnlyStream;
    }
}

internal class P4KSubReadOnlyStream : Stream
    {
        private readonly Stream _superStream;
        private readonly long _startInSuperStream;
        private long _positionInSuperStream;
        private readonly long _endInSuperStream;
        private bool _canRead = true;
        private bool _isDisposed = false;
        
        public override long Length
        {
            get
            {
                return _endInSuperStream - _startInSuperStream; 
            }
        }
        public override long Position
        {
            get
            {
                return _positionInSuperStream - _startInSuperStream;
            }
            set
            {
                Seek(value, SeekOrigin.Begin);
            }
        }
        public override bool CanRead
        {
            get { return _canRead; }
        }
        public override bool CanSeek
        {
            get { return false; }
        }
        public override bool CanWrite
        {
            get { return false; }
        }
        
        internal P4KSubReadOnlyStream(Stream superStream, long startPosition, long maxLength)
        {
            _superStream = superStream;
            _startInSuperStream = startPosition;
            _positionInSuperStream = startPosition;
            _endInSuperStream = startPosition + maxLength;
        }
        
        public override int Read(byte[] buffer, int offset, int count)
        {
            ThrowIfDisposed();
            
            if (_superStream.Position != _positionInSuperStream)
            {
                _superStream.Seek(_positionInSuperStream, SeekOrigin.Begin);
            }

            if (_positionInSuperStream + count > _endInSuperStream)
            {
                count = (int)(_endInSuperStream - _positionInSuperStream);
            }
            
            int returnValue = _superStream.Read(buffer, offset, count);
            _positionInSuperStream += returnValue;
            
            return returnValue;
        }
        
        public override long Seek(long offset, SeekOrigin origin)
        {
            ThrowIfDisposed();
            
            switch (origin)
            {
                case SeekOrigin.Begin:
                    _positionInSuperStream = _startInSuperStream + offset;
                    break;
                
                case SeekOrigin.Current:
                    _positionInSuperStream += offset;
                    break;
                
                case SeekOrigin.End:
                    _positionInSuperStream = _endInSuperStream - offset;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            throw new NotImplementedException();
        }
        
        #region Unsupported Methods
        public override void Flush()
        {
            throw new NotSupportedException("Writing is not supported");
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException("Setting Length is not supported");
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("Writing is not supported");
        }
        #endregion
        
        protected override void Dispose(bool disposing)
        {
            if (disposing && !_isDisposed)
            {
                _canRead = false;
                _isDisposed = true;
            }
            base.Dispose(disposing);
        }

        private void ThrowIfDisposed()
        {
            if (!_isDisposed) return;
            throw new ObjectDisposedException("P4KSubReadOnlyStream");
        }
    }