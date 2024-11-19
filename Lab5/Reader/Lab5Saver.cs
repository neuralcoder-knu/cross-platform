using API.Reader;
using API.Saver;
using Lab5.Models;

namespace Lab5.Reader;

public class Lab5Saver(LabResultModel model) : ISaver
{
    
    public void Save(object o)
    {
        model.Output = o.ToString();
    }

    public void Save(IEnumerable<object> o)
    {
        foreach (var obj in o)
        {
            model.Output += o + "\n";
        }
    }
}