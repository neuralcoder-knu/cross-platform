using API;
using API.Saver;

namespace Lab02;

public class TileTask(ISaver saver) : ProcessTask<TileTaskInput, TileTaskResult>(saver) 
{
    
    protected override object[] Handle0(TileTaskInput abstractTaskParams)
    {
        var n = abstractTaskParams.N;
        var m = abstractTaskParams.M;
        
        var variants = new int[n + 1];
        variants[0] = 1;
        
        for (var i = 1; i <= n; i++)
        {
            variants[i] += variants[i - 1];
            
            if (i >= m)
            {
                variants[i] += variants[i - m];
            }
        }

        return ReturnResults(variants[n]);
    }
}