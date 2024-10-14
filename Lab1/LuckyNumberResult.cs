using API;

namespace Lab01;

public class LuckyNumberResult(int result) : AbstractTaskResult
{
    public int Result { get; set; } = result;

    public override void WriteResult()
    {
        Saver.Save(Result);
    }
}