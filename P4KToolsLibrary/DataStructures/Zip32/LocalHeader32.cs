using System.Text;

namespace P4KToolsLibrary.DataStructures.Zip32;


/*
8	2	Compression method; e.g. none = 0, DEFLATE = 8 (or "\0x08\0x00").
10	2	File last modification time.
12	2	File last modification date.
14	4	CRC-32 of uncompressed data.
18	4	Compressed size (or FF FF FF FF for ZIP64).
22	4	Uncompressed size (or FF FF FF FF for ZIP64).
26	2	File name length (n).
28	2	Extra field length (m).
30	n	File name.
30+n	m	Extra field.
 */

public struct LocalHeader32
{
    public uint MagicNumber;
    public ushort MinimumVersionNumber;
    public ushort BitFlags;
    public ushort CompressionMethod;
    public ushort LastModifiedTime;
    public ushort LastModifiedDate;
    public uint CRC32;
    public uint CompressedSize;
    public uint UncompressedSize;
    public ushort FileNameLength;
    public ushort ExtraFieldLength;
    public string FileName;
    /// <summary>
    /// this field in P4K files has thousands of bytes of padding at the end so it needs to be cleared
    /// as to not take up excessive amounts of ram
    /// </summary>
    public byte[]? ExtraField; 
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
        
        lc32.MinimumVersionNumber = reader.ReadUInt16();
        lc32.BitFlags = reader.ReadUInt16();
        lc32.CompressionMethod = reader.ReadUInt16();
        lc32.LastModifiedTime = reader.ReadUInt16();
        lc32.LastModifiedDate = reader.ReadUInt16();
        lc32.CRC32 = reader.ReadUInt32();
        lc32.CompressedSize = reader.ReadUInt32();
        lc32.UncompressedSize = reader.ReadUInt32();
        lc32.FileNameLength = reader.ReadUInt16();
        lc32.ExtraFieldLength = reader.ReadUInt16();
        
        lc32.FileName = Encoding.Default.GetString(reader.ReadBytes(lc32.FileNameLength));
        
        lc32.ExtraField = reader.ReadBytes(lc32.ExtraFieldLength);
        
        return lc32;
    }
}