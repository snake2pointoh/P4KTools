using P4KToolsLibrary.DataStructures.Zip64;

namespace P4KToolsLibrary;

public class P4KEntry
{
    private readonly P4KArchive _archive;
    private readonly CentralDirectory64 _centralDirectory64;
    private readonly LocalHeader64 _localHeader64;
    
    protected P4KEntry(P4KArchive archive, CentralDirectory64 centralDirectory64, LocalHeader64 localHeader64)
    {
        _archive = archive;
        _centralDirectory64 = centralDirectory64;
        _localHeader64 = localHeader64;
        
        //TODO - create a sub-stream to read the actual compressed data for this entry
        // "new SubReadStream" for opening a stream from the original stream
    }
    
}