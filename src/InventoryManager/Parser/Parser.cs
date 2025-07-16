namespace InventoryManager.Parsers;

internal class Parser
{
    /// <summary>
    /// Convert the string representation of input to the given type equivelant. A return value indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="input">Input string</param>
    /// <param name="type">Type of the object</param>
    /// <param name="result">Parsed value</param>
    /// <returns>true if <see cref="input"/> is converted; otherwise</returns>
    public static bool TryParseValue(string input, Type type, out object? result)
    {
        result = null;
        if (type == typeof(string))
        {
            result = input;
            return true;
        }

        if (type == typeof(int))
        {
            bool status = int.TryParse(input, out int number);
            result = number;
            return status;
        }
        Console.WriteLine("Invalid value !");
        return false;
    }
}
