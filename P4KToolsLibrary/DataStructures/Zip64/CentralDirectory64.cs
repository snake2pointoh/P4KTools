using P4KToolsLibrary.DataStructures.Zip32;

namespace P4KToolsLibrary.DataStructures.Zip64;

public struct CentralDirectory64
{
    public CentralDirectory32 CentralDirectory32;
    //use MemoryStream to convert byte[] to a stream
    public ExtendedData64 ExtendedData64;
}

public static class CentralDirectory64Reader
{
    public static CentralDirectory64 Read(BinaryReader reader)
    {
        CentralDirectory64 cd64 = new CentralDirectory64();
        cd64.CentralDirectory32 = CentralDirectory32Reader.Read(reader);
        
        MemoryStream memStream = new MemoryStream(cd64.CentralDirectory32.ExtraField);
        BinaryReader binaryReader = new BinaryReader(memStream);
        
        cd64.ExtendedData64 = ExtendedData64Reader.Read(binaryReader);
        
        binaryReader.Close();
        memStream.Close();
        
        return cd64;
    }
}
