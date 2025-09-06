using System.Reflection;
using Reflections.Constants;
using Reflections.Handlers;

namespace Reflections.Helpers;

/// <summary>
/// Provide helper methods to manipulate and work assembly and it related types.
/// </summary>
public class AssemblyHelper
{
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
            return Result<Assembly>.Failure(Messages.NoFileExists);
        }
        catch (BadImageFormatException)
        {
            return Result<Assembly>.Failure(Messages.NotValidAssembly);
        }
        catch (FileLoadException ex)
        {
            return Result<Assembly>.Failure(string.Format(Messages.FileCantLoaded, ex.Message));
        }
    }

    /// <summary>
    /// Attempts to invoke a method.
    /// </summary>
    /// <param name="instance">The type on which method to invoke.</param>
    /// <param name="method">Method info of method to invoke.</param>
    /// <param name="arguments">Arguments to the method</param>
    /// <returns><see cref="Result{Assembly}"/> object indicating success with invoking method or failure with error message.</returns>
    public Result<object?> InvokeMethod(object instance, MethodInfo method, object?[] ? arguments)
    {
        try
        {
            object?[] invokeArgs = arguments ?? Array.Empty<object?>();
            object? result = method.Invoke(instance, invokeArgs);

            if (method.ReturnType != typeof(void))
            {
                return Result<object?>.Success(result);
            }

            return Result<object?>.Success(null);
        }
        catch (Exception ex)
        {
            return Result<object?>.Failure(string.Format(Messages.ExceptionCaught, ex.Message));
        }
    }

    /// <summary>
    /// Attempts to create a new instance of given type.
    /// </summary>
    /// <param name="type">Type of the object to create instance for.</param>
    /// <returns><see cref="Result{Assembly}"/> object indicating success with creating instance or failure with error message.</returns>
    public Result<object> CreateTypeInstance(Type type)
    {
        if (!type.IsInterface && !type.IsAbstract && type.GetConstructor(Type.EmptyTypes) != null && !type.ContainsGenericParameters)
        {
            try
            {
                object? typeInstance = Activator.CreateInstance(type);
                if (typeInstance is not null)
                {
                    return Result<object>.Success(typeInstance);
                }
                else
                {
                    return Result<object>.Failure(Messages.NullInstance);
                }
            }
            catch (Exception ex)
            {
                return Result<object>.Failure(string.Format(Messages.ExceptionCaught, ex.Message));
            }
        }

        return Result<object>.Failure(Messages.TypeCantInitiated);
    }
}
