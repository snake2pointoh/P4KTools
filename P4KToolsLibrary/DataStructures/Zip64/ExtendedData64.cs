namespace P4KToolsLibrary.DataStructures.Zip64;

public struct ExtendedData64
{
    public ushort MagicNumber;
    public ushort FieldSizeInBytes;
    public uint UncompressedSize;
    public uint CompressedSize;
    public uint LocalHeaderOffset;
    public uint StartDiskNumber;
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
                extended64.UncompressedSize = reader.ReadUInt32();
                break;
            
            case 16:
                extended64.UncompressedSize = reader.ReadUInt32();
                extended64.CompressedSize = reader.ReadUInt32();
                break;
            
            case 24:
                extended64.UncompressedSize = reader.ReadUInt32();
                extended64.CompressedSize = reader.ReadUInt32();
                extended64.LocalHeaderOffset = reader.ReadUInt32();
                break;
            
            case 28:
                extended64.UncompressedSize = reader.ReadUInt32();
                extended64.CompressedSize = reader.ReadUInt32();
                extended64.LocalHeaderOffset = reader.ReadUInt32();
                extended64.StartDiskNumber = reader.ReadUInt32();
                break;
            
            default:
                throw new InvalidDataException("Invalid extended data size");
        }
        
        return extended64;
    }
}