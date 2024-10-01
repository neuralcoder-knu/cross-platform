using System;
using API;
using API.Saver;
using Lab02;
using NUnit.Framework;

namespace CrossTests;

public class Lab2Tests
{
    private ProcessTask<TileTaskInput, TileTaskResult> _luckyNumbersTask;
    private readonly ConsoleSaver _consoleSaver = new();
    
    [SetUp]
    public void Setup()
    {
        _luckyNumbersTask = new TileTask(_consoleSaver)
            .Valid("M must be more than 2", @params => @params.M >= 2)
            .Valid("M must be less than N", @params => @params.M <= @params.N)
            .Valid("N must be more less 50", @params => @params.N <= 50);
    }

    [Test]
    public void TestDefaultValue()
    {
        _luckyNumbersTask.Params(2, 2);

        Assert.AreEqual(_luckyNumbersTask.Handle().Result, 2);
    }
    
    [Test]
    public void TestValidationMMoreThenN()
    {
        Assert.Throws<ArgumentException>(code: (() => _luckyNumbersTask.Params(11, 10)), message:"M must be less than N");
    }
    
    [Test]
    public void TestValidationMLess2()
    {
        Assert.Throws<ArgumentException>(code: (() => _luckyNumbersTask.Params(1, 1)), message:"M must be more than 2");
    }
    
    [Test]
    public void TestDefaultValue4_6()
    {
        _luckyNumbersTask.Params(4, 6);

        Assert.AreEqual(_luckyNumbersTask.Handle().Result, 4);
    }
    
    [Test]
    public void TestDefaultValue3_7()
    {
        _luckyNumbersTask.Params(3, 7);

        Assert.AreEqual(_luckyNumbersTask.Handle().Result, 9);
    }
}