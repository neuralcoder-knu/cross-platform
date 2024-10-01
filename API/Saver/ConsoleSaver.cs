namespace API.Saver;

public class ConsoleSaver : ISaver
{
    public void Save(object o)
    {
        Console.WriteLine(o.ToString());
    }

    public void Save(IEnumerable<object> o)
    {
        foreach (var obj in o)
        {
            Console.WriteLine(obj.ToString());
        }
    }
}