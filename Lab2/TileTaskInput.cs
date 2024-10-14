using API;
using API.Reader;

namespace Lab02;

public class TileTaskInput : AbstractTaskParams
{
    public TileTaskInput()
    {
    }

    public TileTaskInput(int m, int n)
    {
        M = m;
        N = n;
    }

    public int M { get; private set; }
    public int N { get; private set; }
    
    protected override void Read0(IReader reader)
    {
        var input = reader.ReadAll().ToArray()[0];
        M = int.Parse(input.Split()[0]);
        N = int.Parse(input.Split()[1]);
    }
    
}