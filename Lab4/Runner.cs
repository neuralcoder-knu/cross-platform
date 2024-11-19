using API;
using API.Reader;
using API.Saver;
using API.Util;
using Lab01;
using Lab02;
using Lab03;

namespace Lab4;

public class Runner
{
    private readonly string? _lab;

    private readonly IReader _reader;
    private readonly ISaver _saver;
    
    public Runner(string? lab, string inputPath, string outputPath)
    {
        _lab = lab;
        _reader = new FileReader(inputPath);
        _saver = new FileSaver(outputPath);
    }
    
    public Runner(string? lab, IReader reader, ISaver saver)
    {
        _lab = lab;
        _reader = reader;
        _saver = saver;
    }
    
    
    public void Run()
    {
        switch (_lab)
        {
            case "lab1":
            {
                HandleTaskExecute<LuckyNumbersTask, LuckyNumbersInput, LuckyNumberResult>();        
                break;
            }
            
            case "lab2":
            {
                HandleTaskExecute<TileTask, TileTaskInput, TileTaskResult>();        
                break;
            }
            
            case "lab3":
            {
                HandleTaskExecute<Lab3Task, Lab3Input, Lab3Result>();        
                break;
            }
        }
    }
    
    private void HandleTaskExecute<TTask, TTaskInput, TTaskResult>() 
        where TTaskResult : AbstractTaskResult
        where TTaskInput : AbstractTaskParams 
        where TTask : ProcessTask<TTaskInput, TTaskResult>
    {
        var task = ReflectionUtil.CreateTaskResult<TTask>(_saver)
            .Params(ReflectionUtil.CreateTaskResult<TTaskInput>().Read(_reader));
        
        task.Handle().WriteResult();
    }
    
}