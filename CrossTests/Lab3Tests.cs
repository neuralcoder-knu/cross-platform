using API;
using API.Saver;
using Lab03;

namespace CrossTests;

public class Lab3Tests
{
    private ProcessTask<Lab3Input, Lab3Result> _luckyNumbersTask;
    private readonly ConsoleSaver _consoleSaver = new();
    
    [SetUp]
    public void Setup()
    {
        _luckyNumbersTask = new Lab3Task(_consoleSaver);
    }

    [Test]
    public void TestDefaultValue()
    {
        _luckyNumbersTask.Params(LoadFromString("5\n....X\n.OOOO\n.....\nOOOO.\n@...."));
        
        Assert.IsTrue(_luckyNumbersTask.Handle().Result.Contains("YES"));
    }
    
    [Test]
    public void TestDefaultValue2()
    {
        _luckyNumbersTask.Params(LoadFromString("5\n....X\nOOOOO\n.....\nOOOO.\n@...."));
        
        Assert.IsTrue(_luckyNumbersTask.Handle().Result.Contains("NO"));
    }

    [Test]
    public void TestDefaultValue3()
    {
        _luckyNumbersTask.Params(LoadFromString("5\n.....\n.....\n..X..\n.....\n@...."));
        
        Assert.IsTrue(_luckyNumbersTask.Handle().Result.Contains("YES"));
    }
    
    [Test]
    public void TestDefaultValue4()
    {
        _luckyNumbersTask.Params(LoadFromString("5\n.....\n.OOO.\n.OXOO\n.OOO.\n.@..."));
        
        Assert.IsTrue(_luckyNumbersTask.Handle().Result.Contains("NO"));
    }
    
    [Test]
    public void TestDefaultValue5()
    {
        _luckyNumbersTask.Params(LoadFromString("2\nX.\n@."));
        
        Assert.IsTrue(_luckyNumbersTask.Handle().Result.Contains("YES"));
    }
    
    private object[] LoadFromString(string s)
    {
        var lines = s.Split('\n');
        var n = int.Parse(lines[0]);
        var grid = new char[n, n];
        var start = (-1, -1);
        var end = (-1, -1);

        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                grid[i, j] = lines[i + 1][j];
                if (grid[i, j] == '@')
                    start = (i, j);
                if (grid[i, j] == 'X')
                    end = (i, j);
            }
        }

        return ProcessTask<Lab3Input, Lab3Result>.ReturnResults(n, grid, start, end);
    }
}