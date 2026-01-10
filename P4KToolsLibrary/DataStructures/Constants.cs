namespace P4KToolsLibrary.DataStructures;

internal static class Signatures
{
    internal const uint LocalHeader = 0x04034b50;
    internal const uint LocalHeaderCig = 0x14034b50;
    internal const uint CentralDirectory = 0x02014b50;
    internal const uint EndOfCentralDirectory32 = 0x06054b50;
    internal const uint EndOfCentralDirectory64 = 0x06064b50;
    internal const uint EndOfCentralDirectoryLocator = 0x07064b50;
    internal const ushort ExtendedInfo64 = 0x0001;
}

internal  static class MaxOffsets
{
    internal const uint EocdLocatorMaxOffset = 70000;
}

internal static class DataSizes
{
    internal const int LocalHeaderSize = 0x1000;
    internal const int LocalHeaderSize64 = 0x10000;
}

internal  static class CompressionTypes
{
    internal const ushort None = 0;
    internal const ushort Zstd = 100;
}

