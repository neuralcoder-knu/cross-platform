namespace API.Saver;

public interface ISaver
{
    void Save(object o);
    
    void Save(IEnumerable<object> o);
}