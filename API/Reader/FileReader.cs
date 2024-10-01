namespace API.Reader;

public class FileReader : IReader
{
    
    private readonly string _filePath;

    public FileReader(string filePath)
    {
        _filePath = filePath;
    }

    public void CreateIfNotExists(params string[] Lines)
    {
        if (!File.Exists(_filePath))
        {
            File.WriteAllLines(_filePath, Lines);
        }
    }
    
    public IEnumerable<string> ReadAll()
    {
        if (!File.Exists(_filePath))
        {
            return Array.Empty<string>();
        }

        try
        {
            return File.ReadAllLines(_filePath);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public string Read()
    {
        throw new NotImplementedException();
    }
}