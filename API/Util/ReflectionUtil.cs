namespace API.Util;

public static class ReflectionUtil
{
    
    public static TResult CreateTaskResult<TResult>(object[] parameters) 
    {

        var type = typeof(TResult);
        
        var constructor = type
            .GetConstructors()
            .FirstOrDefault(c => c.GetParameters().Length == parameters.Length);

        if (constructor == null)
        {
            throw new InvalidOperationException($"Constructor with {parameters.Length} parametrs not found for type {type.Name}");
        }
        
        return (TResult)constructor.Invoke(parameters);
    }
}