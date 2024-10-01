using API.Reader;

namespace API;

public abstract class AbstractTaskParams
{
    public AbstractTaskParams()
    {
    }

    protected abstract void Read0(IReader reader);

    public AbstractTaskParams Read(IReader reader)
    {
        Read0(reader);

        return this;
    }
    
}