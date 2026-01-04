using P4KToolsLibrary.DataStructures.Zip32;

namespace P4KToolsLibrary.DataStructures.Zip64;

public struct LocalHeader64
{
    public LocalHeader32 LocalHeader32;
    public ExtendedData64 ExtendedData64;
}

public static class LocalHeader64Reader
{
    public static LocalHeader64 Read(BinaryReader reader)
    {
        LocalHeader64 lh64 = new LocalHeader64();

        lh64.LocalHeader32 = LocalHeader32Reader.Read(reader);
        
        MemoryStream memStream = new MemoryStream(lh64.LocalHeader32.ExtraField);
        BinaryReader binaryReader = new BinaryReader(memStream);
        
        lh64.ExtendedData64 = ExtendedData64Reader.Read(binaryReader);
        
        binaryReader.Close();
        memStream.Close();
        
        return lh64;
    }
}