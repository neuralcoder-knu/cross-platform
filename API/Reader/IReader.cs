namespace API.Reader;

public interface IReader
{

    IEnumerable<string> ReadAll();
    string Read();

}