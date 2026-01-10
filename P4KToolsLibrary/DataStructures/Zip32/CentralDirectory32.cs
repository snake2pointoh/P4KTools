using System.Text;

namespace P4KToolsLibrary.DataStructures.Zip32;


public struct CentralDirectory32
{
    
    public uint MagicNumber;
    public ushort VersionCreated;
    public ushort VersionRequired;
    public ushort BitFlags;
    public ushort CompressionMethod;
    public ushort LastModifiedTime;
    public ushort LastModifiedDate;
    public uint CRC;
    public uint CompressedSizeBytes;
    public uint UncompressedSizeBytes;
    public ushort FileNameLength;
    public ushort ExtraFieldLength;
    public ushort FileCommentLength;
    public ushort StartDiskNumber;
    public ushort InternalAttributes;
    public uint ExternalAttributes;
    public uint LocalHeaderOffset;
    public string FileName;
    public byte[]? ExtraField;
    public byte[] FileComment;
}

public static class CentralDirectory32Reader
{
    public static CentralDirectory32 Read(BinaryReader reader)
    {
        CentralDirectory32 cd32 = new CentralDirectory32();
        cd32.MagicNumber = reader.ReadUInt32();

        if (cd32.MagicNumber != Signatures.CentralDirectory)
        {
            throw new InvalidDataException($"Invalid Central Directory Signature: {cd32.MagicNumber}");
        }
        
        cd32.VersionCreated = reader.ReadUInt16();
        cd32.VersionRequired = reader.ReadUInt16();
        cd32.BitFlags = reader.ReadUInt16();
        cd32.CompressionMethod = reader.ReadUInt16();
        cd32.LastModifiedTime = reader.ReadUInt16();
        cd32.LastModifiedDate = reader.ReadUInt16();
        cd32.CRC = reader.ReadUInt32();
        cd32.CompressedSizeBytes = reader.ReadUInt32();
        cd32.UncompressedSizeBytes = reader.ReadUInt32();
        cd32.FileNameLength = reader.ReadUInt16();
        cd32.ExtraFieldLength = reader.ReadUInt16();
        cd32.FileCommentLength = reader.ReadUInt16();
        cd32.StartDiskNumber = reader.ReadUInt16();
        cd32.InternalAttributes = reader.ReadUInt16();
        cd32.ExternalAttributes = reader.ReadUInt32();
        cd32.LocalHeaderOffset = reader.ReadUInt32();

        byte[] fileNameBytes = reader.ReadBytes(cd32.FileNameLength);
        cd32.FileName = Encoding.Default.GetString(fileNameBytes);
        
        cd32.ExtraField = reader.ReadBytes(cd32.ExtraFieldLength);
        cd32.FileComment = reader.ReadBytes(cd32.FileCommentLength);
        
        return cd32;
    }
}