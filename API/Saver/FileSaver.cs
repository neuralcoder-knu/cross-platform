namespace API.Saver;

public class FileSaver : ISaver
{
    private readonly string _filePath;

    public FileSaver(string filePath)
    {
        _filePath = filePath;
    }
    
    public void Save(object o)
    {
        try
        {
            File.WriteAllTextAsync(_filePath, o.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void Save(IEnumerable<object> o)
    {
        try
        {
            File.AppendAllLines(_filePath, o.Select(o1 => o1.ToString())!);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}