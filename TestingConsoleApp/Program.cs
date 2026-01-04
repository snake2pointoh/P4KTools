using P4KToolsLibrary;

namespace TestingConsoleApp;

public static class Program 
{
    public static void Main(string[] args)
    {
        FileStream filestream = File.Open("E:/StarCitizen/StarCitizen/LIVE/Data.p4k", FileMode.Open, FileAccess.Read);
        
        P4KArchive archive = new P4KArchive(filestream);
    }
}