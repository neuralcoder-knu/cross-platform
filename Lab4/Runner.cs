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

    private readonly FileReader _reader;
    private readonly FileSaver _saver;
    
    public Runner(string? lab, string inputPath, string outputPath)
    {
        _lab = lab;
        _reader = new FileReader(inputPath);
        _saver = new FileSaver(outputPath);
    }
    
    
    public void Run()
    {
        switch (_lab)
        {
            case "lab1":
            {
                HandleTaskExecute<LuckyNumbersTask, LuckyNumbersInput, LuckyNumberResult>(_reader, _saver);        
                break;
            }
            
            case "lab2":
            {
                HandleTaskExecute<TileTask, TileTaskInput, TileTaskResult>(_reader, _saver);        
                break;
            }
            
            case "lab3":
            {
                HandleTaskExecute<Lab3Task, Lab3Input, Lab3Result>(_reader, _saver);        
                break;
            }
        }
    }
    
    private void HandleTaskExecute<TTask, TTaskInput, TTaskResult>(FileReader reader, FileSaver saver) 
        where TTaskResult : AbstractTaskResult
        where TTaskInput : AbstractTaskParams 
        where TTask : ProcessTask<TTaskInput, TTaskResult>
    {
        var task = ReflectionUtil.CreateTaskResult<TTask>(saver)
            .Params(ReflectionUtil.CreateTaskResult<TTaskInput>().Read(reader));
        
        task.Handle().WriteResult();
    }
    
}