namespace P4KToolsLibrary.DataStructures.Zip32;

public struct EndOfCentralDirectory32
{
    public uint MagicNumber;
    public ushort CurrentDiskNumber;
    public ushort CentralDirectoryDiskNumber;
    public ushort CentralDirectoryRecordsOnDisk;
    public ushort TotalCentralDirectoryRecords;
    public uint CentralDirectorySize;
    public uint StartOfCentralDirectory;
    public ushort CommentLength;
    public byte[] Comment;
}

public static class EndOfCentralDirectoryReader
{
    public static EndOfCentralDirectory32 Read(BinaryReader reader)
    {
        EndOfCentralDirectory32 eocd32 = new EndOfCentralDirectory32();
        
        eocd32.MagicNumber = reader.ReadUInt32();

        if (eocd32.MagicNumber != Signatures.EndOfCentralDirectory32)
        {
            throw new InvalidDataException("End of Central Directory 32 magic number is invalid: " + eocd32.MagicNumber);
        }
        
        eocd32.CurrentDiskNumber = reader.ReadUInt16();
        eocd32.CentralDirectoryDiskNumber = reader.ReadUInt16();
        eocd32.CentralDirectoryRecordsOnDisk = reader.ReadUInt16();
        eocd32.TotalCentralDirectoryRecords = reader.ReadUInt16();
        eocd32.CentralDirectorySize = reader.ReadUInt32();
        eocd32.StartOfCentralDirectory = reader.ReadUInt32();
        eocd32.CommentLength = reader.ReadUInt16();
        eocd32.Comment = reader.ReadBytes(eocd32.CommentLength);
        
        return eocd32;
    }
}