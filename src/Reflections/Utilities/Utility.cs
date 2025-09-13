using Reflections.Common;
using Reflections.Constants;

namespace Reflections.Utilities;

/// <summary>
/// Provide utilities methods required for reflection based operations.
/// </summary>
public class Utility
{
    /// <summary>
    /// Attempts to convert the given input string to the specified target type.
    /// </summary>
    /// <param name="input">Input string.</param>
    /// <param name="type">Target type.</param>
    /// <returns>Converted value of input to the target type.</returns>
    /// <exception cref="NotSupportedException">Thrown if the specified type is not supported.</exception>
    public Result<object?> ConvertType(string? input, Type type)
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

        return Result<object?>.Failure(string.Format(WarningMessages.TypeNotSupported, type.Name));
    }
}