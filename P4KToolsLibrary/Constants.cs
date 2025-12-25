namespace P4KToolsLibrary;

// #define LOCAL_SIG 0x04034b50
// #define LOCAL_SIG_CIG 0x14034b50
// #define CENTRAL_SIG 0x02014b50
// #define EOCD_SIG 0x06054b50
// #define EOCD64_SIG 0x06064b50
// #define EX_INFO_SIG 0x0001

// // max sizes
// #define MAX_EOCD_OFFSET 65558
// // the true max size is to large, the EOCD64 will always be 78 bytes before the EOCD
// #define MAX_EOCD64_OFFSET (MAX_EOCD_OFFSET + 78) 

// // data structure sizes
// #define LOCAL_HDR_SIZE 30
// #define CD_SIZE 46

public enum Signatures
{
    LocalHeader = 0x04034b50,
    LocalHeaderCig = 0x14034b50,
    CentralDirectory = 0x04034b50,
    EndOfCentralDirectory = 0x06054b50,
    EndOfCentralDirectory64 = 0x06064b50,
    ExtendedInfo = 0x0001
}

public enum MaxOffsets
{
    EndOfCentralDirectoryOffset = 65558,
    EndOfCentralDirectoryOffset64 = EndOfCentralDirectoryOffset + 78,
}

public enum DataSizes
{
    LocalHeaderSize = 0x1000,
    LocalHeaderSize64 = 0x10000,
}

