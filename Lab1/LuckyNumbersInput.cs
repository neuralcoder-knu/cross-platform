using API;
using API.Reader;

namespace Lab01;

public class LuckyNumbersInput : AbstractTaskParams
{
    public LuckyNumbersInput() : base()
    {
    }

    public LuckyNumbersInput(int n)
    {
        N = n;
    }

    public int N { get; private set; }

    protected override void Read0(IReader reader)
    {
        N = int.Parse(reader.ReadAll().ToArray()[0]);
    }
    
}