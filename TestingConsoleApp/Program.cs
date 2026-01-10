using P4KToolsLibrary;

namespace TestingConsoleApp;

public static class Program 
{
    public static void Main(string[] args)
    {
        FileStream filestream = File.Open(@"E:\StarCitizen\StarCitizen\LIVE\Data.p4k", FileMode.Open, FileAccess.Read);
        
        P4KArchive archive = new P4KArchive(filestream);

        P4KEntry entry = archive.Entries[0];

        Console.WriteLine(entry.FilePath);
        Console.WriteLine(entry.FileName);
        Console.WriteLine($"Compressed Size Bytes: {entry.CompressedSize}");
        Console.WriteLine($"Uncompressed Size Size Bytes: {entry.UncompressedSize}");
        
        FileStream outStream = File.Create($@"E:\Dev\c#\P4KTools\TestingConsoleApp\output\{entry.FileName}");
        entry.GetDecompressionStream().CopyTo(outStream);
        
    }
}