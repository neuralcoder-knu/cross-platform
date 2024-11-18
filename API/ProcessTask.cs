using API.Saver;
using API.Util;

namespace API;

public delegate bool Validate<in TTaskParams>(TTaskParams @params);
public abstract class ProcessTask<TTAskParams, TTaskResult> 
    where TTAskParams : AbstractTaskParams 
    where TTaskResult : AbstractTaskResult
{

    private readonly ISaver _saver;
    private TTAskParams @params;
    private readonly Dictionary<Validate<TTAskParams>, string> _validates = [];
    
    public ProcessTask(ISaver saver)
    {
        _saver = saver;
        RegisterDefaultValidation();
    }

    protected void RegisterDefaultValidation()
    {
        
    }

    //TODO: mb refactor, idk
    public static object[] ReturnResults(params object[] results)
    {
        return results;
    }
    
    protected abstract object[] Handle0(TTAskParams abstractTaskParams);

    public ProcessTask<TTAskParams, TTaskResult> Params(AbstractTaskParams paParams)
    {
        var resParams = (TTAskParams)paParams;
        Validate(resParams);
        @params = resParams;
        
        return this;
    }
    
    public ProcessTask<TTAskParams, TTaskResult> Params(params object[] res)
    {
        var paramObj = ReflectionUtil.CreateTaskResult<TTAskParams>(res);
        Validate(paramObj);
        @params = paramObj;
        
        return this;
    }
    
    public ProcessTask<TTAskParams, TTaskResult> Valid(string message, Validate<TTAskParams> validate)
    {
        _validates.Add(validate, message);

        return this;
    }
    
    public TTaskResult Handle()
    {
        return Handle(@params, _saver);
    }
    
    public TTaskResult Handle(TTAskParams abstractTaskParams, ISaver saver)
    {
        Validate(abstractTaskParams);
        
        var result = Handle0(abstractTaskParams);
        var resultObj = ReflectionUtil.CreateTaskResult<TTaskResult>(result);

        resultObj.Saver = saver;
        return resultObj;
    }

    private void Validate(TTAskParams abstractTaskParams)
    {
        foreach (var validate in _validates
                     .Where(validate => !validate.Key.Invoke(abstractTaskParams)))
        {
            throw new ArgumentException(validate.Value);
        }
    }

}