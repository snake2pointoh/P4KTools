namespace P4KToolsLibrary.DataStructures.Zip64;

public struct EndOfCentralDirectoryLocator
{
    public uint MagicNumber;
    public uint Eocd64DiskNumber;
    public ulong Eocd64Offset;
    public uint NumberOfDisks;
}

public static class EndOfCentralDirectoryLocatorReader
{
    /// <summary>
    /// Reads an End Of Central Directory Locator data block from the given BinaryReader
    /// at the current position of the reader
    /// </summary>
    /// <exception cref="InvalidDataException">Thrown if the magic number does not match expected</exception>
    /// <param name="reader">The BinaryReader to read from</param>
    /// <returns>The End Of Central Directory Locator data block read from the reader</returns>
    public static EndOfCentralDirectoryLocator Read(BinaryReader reader)
    {
        EndOfCentralDirectoryLocator newLocator = new EndOfCentralDirectoryLocator();
        newLocator.MagicNumber = reader.ReadUInt32();

        if (newLocator.MagicNumber != Signatures.EndOfCentralDirectoryLocator)
        {
            throw new InvalidDataException("End of Central Directory Locator magic number is invalid: " + newLocator.MagicNumber);
        }
        
        newLocator.Eocd64DiskNumber = reader.ReadUInt32();
        newLocator.Eocd64Offset = reader.ReadUInt64();
        newLocator.NumberOfDisks = reader.ReadUInt32();
        
        return newLocator;
    }
}