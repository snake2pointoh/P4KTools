namespace P4KToolsLibrary;

public class P4KEntry
{
    private readonly P4KArchive _archive;
    
    
    public P4KEntry(P4KArchive archive)
    {
        _archive = archive;
    }
    
    // "new SubReadStream" for opening a stream from the original stream
}