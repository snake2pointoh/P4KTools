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

        if (lh64.LocalHeader32.ExtraField == null)
        {
            throw new InvalidDataException("Extra field is missing");
        }
        
        MemoryStream memStream = new MemoryStream(lh64.LocalHeader32.ExtraField);
        BinaryReader binaryReader = new BinaryReader(memStream);
        
        lh64.ExtendedData64 = ExtendedData64Reader.Read(binaryReader);
        
        // set to null for GC cleanup
        lh64.LocalHeader32.ExtraField = null;
        
        binaryReader.Dispose();
        memStream.Dispose();
        
        return lh64;
    }
}