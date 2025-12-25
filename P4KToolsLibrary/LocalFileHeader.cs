namespace P4KToolsLibrary;

public struct LocalFileHeader
{
    public short VersionNumber;
    public short BitFlags;
    public short CompressionMethod;
    public short LastModifiedTime;
    public short LastModifiedDate;
    public int CRC;
    public int CompressedSize;
    public int UncompressedSize;
    public short FileNameLength;
    public short ExtraFieldLength;
    public string FileName;
    public byte[] ExtraField;
}