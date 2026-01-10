namespace P4KToolsLibrary.DataStructures.Zip64;

public struct ExtendedData64
{
    public ushort MagicNumber;
    public ushort FieldSizeInBytes;
    public ulong UncompressedSize;
    public ulong CompressedSize;
    public ulong LocalHeaderOffset;
    public uint StartDiskNumber;
    /// <summary>
    /// this field is not standard to regular zip64 format
    /// will alternate between:
    /// ( 8671232 | 0x845000 )
    /// and
    /// ( 260179558 | 0xF820666 )
    /// </summary>
    public uint UnknownField;
}

public static class ExtendedData64Reader
{
    public static ExtendedData64 Read(BinaryReader reader)
    {
        ExtendedData64 extended64 = new ExtendedData64();

        extended64.MagicNumber = reader.ReadUInt16();
        extended64.FieldSizeInBytes = reader.ReadUInt16();

        switch (extended64.FieldSizeInBytes)
        {
            case 8:
                extended64.UncompressedSize = reader.ReadUInt64();
                break;
            
            case 16:
                extended64.UncompressedSize = reader.ReadUInt64();
                extended64.CompressedSize = reader.ReadUInt64();
                break;
            
            case 24:
                extended64.UncompressedSize = reader.ReadUInt64();
                extended64.CompressedSize = reader.ReadUInt64();
                extended64.LocalHeaderOffset = reader.ReadUInt64();
                break;
            
            case 28:
                extended64.UncompressedSize = reader.ReadUInt64();
                extended64.CompressedSize = reader.ReadUInt64();
                extended64.LocalHeaderOffset = reader.ReadUInt64();
                extended64.StartDiskNumber = reader.ReadUInt32();
                break;
            case 32:
                extended64.UncompressedSize = reader.ReadUInt64();
                extended64.CompressedSize = reader.ReadUInt64();
                extended64.LocalHeaderOffset = reader.ReadUInt64();
                extended64.StartDiskNumber = reader.ReadUInt32();
                extended64.UnknownField = reader.ReadUInt32();
                break;
            
            default:
                throw new InvalidDataException($"Invalid extended data size: {extended64.FieldSizeInBytes}");
        }
        
        return extended64;
    }
}