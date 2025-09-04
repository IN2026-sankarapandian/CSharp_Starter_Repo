using System.Reflection;
using Reflections.Utilities;

namespace Reflections.Handlers;

public class AssemblyHelper
{
    private readonly Utility _utility;

    public AssemblyHelper(Utility utility)
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

    /// <summary>
    /// Attempts to change the value of specified property.
    /// </summary>
    /// <param name="typeInstance">Type instance of the specified property.</param>
    /// <param name="propertyInfo">Target property to change its value.</param>
    /// <param name="newValue">New value to be assigned for target property.</param>
    /// <returns><see cref="Result{Assembly}"/> object indicating success with changing property value or failure with error message.</returns>
    public Result<bool> ChangePropertyValue(object? typeInstance, PropertyInfo propertyInfo, string? newValue)
    {
        if (!propertyInfo.CanWrite)
        {
            return Result<bool>.Failure("Property is read only !");
        }

        try
        {
            Result<object?> convertedValue = this._utility.ConvertType(newValue, propertyInfo.PropertyType);
            if (!convertedValue.IsSuccess)
            {
                return Result<bool>.Failure(convertedValue.ErrorMessage);
            }

            propertyInfo.SetValue(typeInstance, convertedValue.Value);
            return Result<bool>.Success(true);
        }
        catch (NotSupportedException ex)
        {
            return Result<bool>.Failure(ex.Message);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(ex.Message);
        }
    }

    //private object? HandleCreateTypeInstance(Type type)
    //{
    //    if (!type.IsInterface && !type.IsAbstract && type.GetConstructor(Type.EmptyTypes) != null && !type.ContainsGenericParameters)
    //    {
    //        try
    //        {
    //            object? typeInstance = Activator.CreateInstance(type);
    //            return typeInstance;
    //        }
    //        catch (Exception ex)
    //        {
    //        }
    //    }
    //    return null;
    //}

    //private object? ConvertParameter1(string? input, Type type)
    //{
    //    if (type == typeof(string))
    //    {
    //        return input;
    //    }

    //    if (type.IsPrimitive || type == typeof(decimal))
    //    {
    //        return Convert.ChangeType(input, type);
    //    }

    //    throw new NotSupportedException($"Type {type.Name} not supported");
    //}

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
