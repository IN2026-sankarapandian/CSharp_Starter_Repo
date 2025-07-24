using InventoryManager.Constants;

namespace InventoryManager.Parsers;

/// <summary>
/// Provides generic parser methods works for specified data types
/// </summary>
public class Parser
{
    /// <summary>
    /// Try to parse the input string to the specified data type. Out the appropriate error message if parsing faied
    /// </summary>
    /// <param name="input">Input string</param>
    /// <param name="type">Target data type</param>
    /// <param name="result">Contains parsed value if parse is successful, else the input string</param>
    /// <param name="errorMessage">Contains the error message of why parsing failed, if parsing passed error messagewill be empty.</param>
    /// <returns><see cref="true"/> if parsing was successful, <see cref="false"/> if can't parse</returns>
    public static bool TryParseValue(string input, Type type, out object result, out string errorMessage)
    {
        bool status = false;
        errorMessage = string.Empty;
        if (type == typeof(int))
        {
            status = int.TryParse(input, out int number);
            result = number;
            if (!status)
            {
                errorMessage = ErrorMessages.NeedANumber;
            }

            return status;
        }
        else if (type == typeof(decimal))
        {
            status = decimal.TryParse(input, out decimal number);
            result = number;
            if (!status)
            {
                errorMessage = ErrorMessages.NeedANumber;
            }

            return status;
        }
        else if (type == typeof(string))
        {
            result = input;
            return true;
        }

        result = input;
        return status;
    }
}