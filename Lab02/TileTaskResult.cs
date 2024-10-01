using API;

namespace Lab02;

public class TileTaskResult(int result) : AbstractTaskResult
{
    public int Result { get; set; } = result;

    public override void WriteResult()
    {
        Saver.Save(Result);
    }
}