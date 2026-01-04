namespace P4KToolsLibrary.DataStructures;

public static class Signatures
{
    public const uint LocalHeader = 0x04034b50;
    public const uint LocalHeaderCig = 0x14034b50;
    public const uint CentralDirectory = 0x04034b50;
    public const uint EndOfCentralDirectory32 = 0x06054b50;
    public const uint EndOfCentralDirectory64 = 0x06064b50;
    public const uint EndOfCentralDirectoryLocator = 0x07064b50;
    public const ushort ExtendedInfo64 = 0x0001;
}

public static class MaxOffsets
{
    public const uint EocdLocatorMaxOffset = 70000;
}

public static class DataSizes
{
    public const int LocalHeaderSize = 0x1000;
    public const int LocalHeaderSize64 = 0x10000;
}

