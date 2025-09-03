using System.Reflection;
using Reflections.Enums;
using Reflections.UserInterface;

namespace Reflections.Handlers;

public class FormHandlers
{
    private readonly IUserInterface _userInterface;

    /// <summary>
    /// Initializes a new instance of the <see cref="FormHandlers"/> class.
    /// </summary>
    /// <param name="userInterface"></param>
    public FormHandlers(IUserInterface userInterface)
    {
        this._userInterface = userInterface;
    }

    /// <summary>
    /// Gets the valid input string from the user.
    /// </summary>
    /// <param name="prompt">Prompt shown to the user.</param>
    /// <returns>User's input value.</returns>
    public string GetUserInput(string prompt)
    {
        do
        {
            this._userInterface.ShowMessage(MessageType.Prompt, prompt);
            string? userInput = Console.ReadLine();
            if (string.IsNullOrEmpty(userInput))
            {
                this._userInterface.ShowMessage(MessageType.Warning, "Input cannot be empty !");
            }
            else
            {
                return userInput;
            }
        }
        while (true);
    }

    /// <summary>
    /// Gets a valid and existing path from user.
    /// </summary>
    /// <returns>Path defined by user.</returns>
    public string GetPath()
    {
        do
        {
            this._userInterface.ShowMessage(MessageType.Prompt, "Enter a path of the file to inspect : ");
            string? path = Console.ReadLine();

            if (string.IsNullOrEmpty(path))
            {
                this._userInterface.ShowMessage(MessageType.Warning, "Input cannot be empty !");
            }
            else if (!File.Exists(path))
            {
                this._userInterface.ShowMessage(MessageType.Warning, "No file exists in the specified path !");
            }
            else
            {
                return path;
            }
        }
        while (true);
    }

    /// <summary>
    /// Gets value index of any set of values from the user.
    /// </summary>
    /// <param name="valuesLength">Total length of the values.</param>
    /// <returns>Index given by user.</returns>
    public int GetIndex(int valuesLength, string prompt)
    {
        do
        {
            this._userInterface.ShowMessage(MessageType.Prompt, prompt);
            string? userInput = Console.ReadLine();
            if (int.TryParse(userInput, out int indexValue) && indexValue <= valuesLength && indexValue > 0)
            {
                return indexValue - 1;
            }

            this._userInterface.ShowMessage(MessageType.Warning, "Enter a valid index !");
        }
        while (true);
    }

    /// <summary>
    /// Gets the arguments from user for specified parameters.
    /// </summary>
    /// <param name="parameters">Parameters to get argument from user.</param>
    /// <returns>Arguments given by the user</returns>
    public Result<object?[]?> GetArguments(ParameterInfo[] parameters)
    {
        object?[] arguments = new object?[parameters.Length];
        for (int i = 0; i < parameters.Length; i++)
        {
            string userInput = this.GetUserInput(string.Format("Enter value for parameter {0}({1}) : ", parameters[i].Name, parameters[i].ParameterType.Name));
            try
            {
                arguments[i] = this.ConvertParameter(userInput, parameters[i].ParameterType);
            }
            catch (Exception ex)
            {
                return Result<object?[]?>.Failure(ex.Message);
            }
        }

        return Result<object?[]?>.Success(arguments);
    }

    /// <summary>
    /// Prompts the user to select the type from the listed available types and return the selected type.
    /// </summary>
    /// <param name="types">Available types.</param>
    /// <returns>Type selected by user.</returns>
    public Type GetTargetType(Type[] types, string prompt)
    {
        this._userInterface.DisplayTypes(types);
        int typeIndex = this.GetIndex(types.Length, prompt);
        return types[typeIndex];
    }

    /// <summary>
    /// Prompts the user to select the property from the listed available properties and return the selected property info.
    /// </summary>
    /// <param name="type">Properties of this type is prompted to user to select.</param>
    /// <returns>Property info of property selected by user.</returns>
    public PropertyInfo GetTargetPropertyInfo(Type type, string prompt)
    {
        PropertyInfo[] propertyInfos = type.GetProperties();
        this._userInterface.DisplayTypeProperties(type, propertyInfos);
        int propertyInfoIndex = this.GetIndex(propertyInfos.Length, prompt);
        return propertyInfos[propertyInfoIndex];
    }

    /// <summary>
    /// Prompts the user to select the method from the listed available methods and return the selected method info.
    /// </summary>
    /// <param name="methodInfos">Methods info shown to the user to select.</param>
    /// <returns>Method info of method selected by user.</returns>
    public MethodInfo GetTargetMethodInfo(MethodInfo[] methodInfos, string prompt)
    {
        this._userInterface.DisplayTypeMethods(methodInfos);
        int methodInfoIndex = this.GetIndex(methodInfos.Length, "\nEnter which method to invoke : ");
        return methodInfos[methodInfoIndex];
    }

    /// <summary>
    /// Attempts to convert the given input string to the specified target type.
    /// </summary>
    /// <param name="input">Input string.</param>
    /// <param name="type">Target type.</param>
    /// <returns>Converted value of input to the target type.</returns>
    /// <exception cref="NotSupportedException">Thrown if the specified type is not supported.</exception>
    private object? ConvertParameter(string? input, Type type)
    {
        if (type == typeof(string))
        {
            return input;
        }

        if (type.IsPrimitive || type == typeof(decimal))
        {
            return Convert.ChangeType(input, type);
        }

        throw new NotSupportedException($"Type {type.Name} not supported");
    }
}
