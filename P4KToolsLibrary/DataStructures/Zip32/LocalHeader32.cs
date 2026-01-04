using System.Text;

namespace P4KToolsLibrary.DataStructures.Zip32;

public struct LocalHeader32
{
    public uint MagicNumber;
    public ushort VersionNumber;
    public ushort BitFlags;
    public ushort CompressionMethod;
    public ushort LastModifiedTime;
    public ushort LastModifiedDate;
    public uint CRC;
    public uint CompressedSize;
    public uint UncompressedSize;
    public ushort FileNameLength;
    public ushort ExtraFieldLength;
    public string FileName;
    public byte[] ExtraField;
}

public static class LocalHeader32Reader
{
    public static LocalHeader32 Read(BinaryReader reader)
    {
        LocalHeader32 lc32 = new LocalHeader32();
        
        lc32.MagicNumber = reader.ReadUInt32();

        if (lc32.MagicNumber != Signatures.LocalHeader)
        {
            throw new InvalidDataException($"Invalid Local Header Signature: {lc32.MagicNumber}");
        }
        
        lc32.VersionNumber = reader.ReadUInt16();
        lc32.BitFlags = reader.ReadUInt16();
        lc32.CompressionMethod = reader.ReadUInt16();
        lc32.LastModifiedTime = reader.ReadUInt16();
        lc32.LastModifiedDate = reader.ReadUInt16();
        lc32.CRC = reader.ReadUInt32();
        lc32.CompressedSize = reader.ReadUInt32();
        lc32.UncompressedSize = reader.ReadUInt32();
        lc32.FileNameLength = reader.ReadUInt16();
        lc32.ExtraFieldLength = reader.ReadUInt16();
        
        byte[] fileName = reader.ReadBytes(lc32.FileNameLength);
        lc32.FileName = Encoding.Default.GetString(fileName);
        
        lc32.ExtraField = reader.ReadBytes(lc32.ExtraFieldLength);
        
        return lc32;
    }
}