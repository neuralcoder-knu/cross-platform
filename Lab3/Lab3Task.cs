using API;
using API.Saver;

namespace Lab03;

public class Lab3Task(ISaver saver) : ProcessTask<Lab3Input, Lab3Result>(saver) 
{
    protected override object[] Handle0(Lab3Input abstractTaskParams)
    {
        var graph = new Graph(abstractTaskParams.N, abstractTaskParams.Grid,
            abstractTaskParams.Start, abstractTaskParams.End);

        var res = graph.Handle();
        Console.WriteLine(res);
        return ReturnResults(res);
    }
}