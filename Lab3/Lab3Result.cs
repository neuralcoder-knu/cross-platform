using API;
using API.Reader;

namespace Lab03;

public class Lab3Result : AbstractTaskResult
{
    public Lab3Result(string result)
    {
        Result = result;
    }

    public readonly string Result;
    
    public override void WriteResult()
    {
        Saver.Save(Result);
    }
}