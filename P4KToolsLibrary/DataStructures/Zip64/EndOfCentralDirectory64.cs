namespace P4KToolsLibrary.DataStructures.Zip64;

public struct EndOfCentralDirectory64
{
    public uint MagicNumber;
    // size of the REST of the record, subtracting the size of the magic number and the size variables 
    public ulong SizeMinus12;
    public ushort VersionCreatedBy;
    public ushort RequiredVersion;
    public uint CurrentDiskNumber;
    public uint CentralDirectoryDiskNumber;
    public ulong CentralDirectoryRecordsOnDisk;
    public ulong TotalCentralDirectoryRecords;
    public ulong CentralDirectorySizeBytes;
    public ulong CentralDirectoryOffset;
    public byte[] Comment; // remaining bytes in SizeMinus12 after reading all previous fields
}

public static class EndOfCentralDirectory64Reader
{
    private const ulong RemainingSize = (sizeof(ushort)*2) + (sizeof(uint)*2) + (sizeof(ulong)*4);
    
    public static EndOfCentralDirectory64 Read(BinaryReader reader)
    {
        EndOfCentralDirectory64 eocd64 = new EndOfCentralDirectory64();
        eocd64.MagicNumber = reader.ReadUInt32();

        if (eocd64.MagicNumber != Signatures.EndOfCentralDirectory64)
        {
            throw new InvalidDataException("End of Central Directory 64 magic number is invalid: " + eocd64.MagicNumber);
        }
        
        eocd64.SizeMinus12 = reader.ReadUInt64();
        eocd64.VersionCreatedBy = reader.ReadUInt16();
        eocd64.RequiredVersion = reader.ReadUInt16();
        eocd64.CurrentDiskNumber = reader.ReadUInt32();
        eocd64.CentralDirectoryDiskNumber = reader.ReadUInt32();
        eocd64.CentralDirectoryRecordsOnDisk = reader.ReadUInt64();
        eocd64.TotalCentralDirectoryRecords = reader.ReadUInt64();
        eocd64.CentralDirectorySizeBytes = reader.ReadUInt64();
        eocd64.CentralDirectoryOffset = reader.ReadUInt64();
        
        ulong commentLength = eocd64.SizeMinus12 - RemainingSize;
        
        if (commentLength > int.MaxValue)
        {
            throw new InvalidDataException("End of Central Directory 64 comment is too large: " + commentLength);
        }
        
        eocd64.Comment = reader.ReadBytes((int)commentLength);
        
        
        return eocd64;
    }
}
