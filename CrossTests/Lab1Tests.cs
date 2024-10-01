using API;
using API.Saver;
using Lab01;
using Assert = NUnit.Framework.Assert;

namespace CrossTests;

public class Lab1Tests
{

    private ProcessTask<LuckyNumbersInput, LuckyNumberResult> _luckyNumbersTask;
    private readonly ConsoleSaver _consoleSaver = new();
    
    [SetUp]
    public void Setup()
    {
        _luckyNumbersTask = new LuckyNumbersTask(_consoleSaver)
            .Valid("N must be less than 1032", @params => @params.N is >= 0 and <= 1032);
    }

    [Test]
    public void TestDefaultValue()
    {
        _luckyNumbersTask.Params(56);

        Assert.AreEqual(_luckyNumbersTask.Handle().Result, 4);
    }
    
    [Test]
    public void TestValidation()
    {
        Assert.Throws<ArgumentException>(code: (() => _luckyNumbersTask.Params(3000000)));
    }
    
    [Test]
    public void TestDefaultValue100()
    {
        _luckyNumbersTask.Params(100);

        Assert.AreEqual(_luckyNumbersTask.Handle().Result, 6);
    }
    
    [Test]
    public void TestDefaultValue4()
    {
        _luckyNumbersTask.Params(4);

        Assert.AreEqual(_luckyNumbersTask.Handle().Result, 1);
    }
    
    [Test]
    public void TestDefaultValue1032()
    {
        _luckyNumbersTask.Params(1032);

        Assert.AreEqual(_luckyNumbersTask.Handle().Result, 14);
    }
}