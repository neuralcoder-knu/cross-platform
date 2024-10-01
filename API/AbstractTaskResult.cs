using API.Saver;

namespace API;

public abstract class AbstractTaskResult
{

    public ISaver Saver { get; set; }

    public abstract void WriteResult();

}