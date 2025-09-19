#pragma warning disable SA1011 // Closing square brackets should be spaced correctlyusing System.Reflection;
using System.Reflection;
using Reflections.Common;
using Reflections.Constants;

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
            return Result<Assembly>.Failure(ErrorMessages.NoFileExists);
        }
        catch (BadImageFormatException)
        {
            return Result<Assembly>.Failure(ErrorMessages.NotValidAssembly);
        }
        catch (FileLoadException ex)
        {
            return Result<Assembly>.Failure(string.Format(ErrorMessages.FileCantLoaded, ex.Message));
        }
    }

    /// <summary>
    /// Attempts to invoke a method.
    /// </summary>
    /// <param name="instance">The type on which method to invoke.</param>
    /// <param name="method">Method info of method to invoke.</param>
    /// <param name="arguments">Arguments to the method</param>
    /// <returns><see cref="Result{Assembly}"/> object indicating success with invoking method or failure with error message.</returns>
    public Result<object?> InvokeMethod(object instance, MethodInfo method, object?[]? arguments)
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
            // Unexpected exception may get caught here as user may invoke any kind of method from any assemblies.
            return Result<object?>.Failure(string.Format(ErrorMessages.ExceptionCaught, ex.Message));
        }
    }

    /// <summary>
    /// Attempts to create a new instance of given type.
    /// </summary>
    /// <param name="type">Type of the object to create instance for.</param>
    /// <returns><see cref="Result{Assembly}"/> object indicating success with creating instance or failure with error message.</returns>
    public Result<object> CreateTypeInstance(Type type)
    {
        if (type.IsInterface || type.IsAbstract || type.GetConstructor(Type.EmptyTypes) == null || type.ContainsGenericParameters)
        {
            return Result<object>.Failure(ErrorMessages.TypeCantInitiated);
        }

        try
        {
            object? typeInstance = Activator.CreateInstance(type);
            if (typeInstance is null)
            {
                return Result<object>.Failure(ErrorMessages.NullInstance);
            }

            return Result<object>.Success(typeInstance);
        }
        catch (Exception ex)
        {
            return Result<object>.Failure(string.Format(ErrorMessages.ExceptionCaught, ex.Message));
        }
    }
}
#pragma warning disable SA1011 // Closing square brackets should be spaced correctly