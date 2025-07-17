using InventoryManager.UI;

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
    /// <param name="result">Contains parsed if parse is successful, else the default string</param>
    /// <param name="error">Contains the error message of why parsing failed, if parsing passed error will be empty.</param>
    /// <returns>true if parsing was successful, false if cant parsed</returns>
    public static bool TryParseValue(string input, Type type, out object result, out string error)
    {
        bool status = false;
        error = string.Empty;
        if (type == typeof(int))
        {
            status = int.TryParse(input, out int number);
            result = number;
            if (!status)
            {
                error = "Give a valid input!";
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