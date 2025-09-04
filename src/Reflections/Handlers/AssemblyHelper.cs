using System.Reflection;
using Reflections.Utilities;

namespace Reflections.Handlers;

public class AssemblyHelper
{
    //private readonly Utility _utility;

    public AssemblyHelper()
    {
        this._utility = utility;
    }

    /// <summary>
    /// Attempts to load the assembly in specified path.
    /// </summary>
    /// <param name="path">Path to load assembly from.</param>
    /// <returns><see cref="Result{Assembly}"/> object indicating success with loaded assembly or failure with error message.</returns>
    public Result<Assembly> LoadAssemblyFile(string path)
    {
        try
        {
            Assembly assembly = Assembly.LoadFile(path);
            return Result<Assembly>.Success(assembly);
        }
        catch (FileNotFoundException)
        {
            return Result<Assembly>.Failure("No file exists in the specified path !");
        }
        catch (BadImageFormatException)
        {
            return Result<Assembly>.Failure("The file is not a valid .NET assembly !");
        }
        catch (FileLoadException ex)
        {
            return Result<Assembly>.Failure($"The file can't be load {ex.Message}");
        }
    }

    /// <summary>
    /// Attempts to invoke a method.
    /// </summary>
    /// <param name="instance">The type on which method to invoke.</param>
    /// <param name="method">Method info of method to invoke.</param>
    /// <param name="arguments">Arguments to the method</param>
    /// <returns><see cref="Result{Assembly}"/> object indicating success with invoking method or failure with error message.</returns>
    public Result<object> InvokeMethod(object instance, MethodInfo method, object?[]? arguments)
    {
        try
        {
            object?[] invokeArgs = arguments ?? Array.Empty<object?>();
            object? result = method.Invoke(instance, invokeArgs);

            if (method.ReturnType != typeof(void))
            {
                return Result<object>.Success(result);
            }

            return Result<object>.Success(null);
        }
        catch (Exception ex)
        {
            return Result<object>.Failure($"Exception caught while invoking method : {ex.Message}");
        }
    }

    public Result<object?> CreateTypeInstance(Type type)
    {
        if (!type.IsInterface && !type.IsAbstract && type.GetConstructor(Type.EmptyTypes) != null && !type.ContainsGenericParameters)
        {
            try
            {
                object? typeInstance = Activator.CreateInstance(type);
                return Result<object?>.Success(typeInstance);
            }
            catch (Exception ex)
            {
                return Result<object?>.Failure($"Unexpected error got while creating instance for type : {type.Name} error : {ex}");
            }
        }

        return Result<object?>.Failure("Type can't be initiated, so unable to edit values !");
    }

    private Result<object?> ConvertParameter(string? input, Type type)
    {
        if (type == typeof(string))
        {
            return Result<object?>.Success(input);
        }

        if (type.IsPrimitive || type == typeof(decimal))
        {
            try
            {
                object? converted = Convert.ChangeType(input, type);
                return Result<object?>.Success(converted);
            }
            catch (Exception ex)
            {
                return Result<object?>.Failure(ex.Message);
            }
        }

        return Result<object?>.Failure($"Type {type.Name} not supported");
    }
}
