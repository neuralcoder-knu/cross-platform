using API.Reader;

namespace Lab5.Reader;

public class Lab5Reader(List<string> input) : IReader
{
    public IEnumerable<string> ReadAll()
    {
        return input;
    }

    public string Read()
    {
        throw new NotImplementedException();
    }
}