using System.Reflection;
using Reflections.Common;
using Reflections.Constants;
using Reflections.Enums;
using Reflections.UserInterface;
using Reflections.Utilities;
using Reflections.Validators;

namespace Reflections.Handlers;

/// <summary>
/// Provides method for user input handling.
/// </summary>
public class FormHandler
{
    private readonly IUserInterface _userInterface;
    private readonly Validator _validator;
    private readonly Utility _utility;

    /// <summary>
    /// Initializes a new instance of the <see cref="FormHandler"/> class.
    /// </summary>
    /// <param name="userInterface">Provides operations to interact with user.</param>
    /// <param name="validator">Provide validation methods</param>
    /// <param name="utility">Provide utilities to manipulate type for reflection based methods.</param>
    public FormHandler(IUserInterface userInterface, Validator validator, Utility utility)
    {
        this._userInterface = userInterface;
        this._validator = validator;
        this._utility = utility;
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
                this._userInterface.ShowMessage(MessageType.Warning, WarningMessages.InputCannotEmpty);
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
            this._userInterface.ShowMessage(MessageType.Prompt, PromptMessages.EnterPath);
            string? path = Console.ReadLine();

            if (string.IsNullOrEmpty(path))
            {
                this._userInterface.ShowMessage(MessageType.Warning, WarningMessages.InputCannotEmpty);
            }
            else if (!File.Exists(path))
            {
                this._userInterface.ShowMessage(MessageType.Warning, WarningMessages.NoFileExists);
            }
            else
            {
                return path;
            }
        }
        while (true);
    }

    /// <summary>
    /// Gets index of value selected by the user from any set of values.
    /// </summary>
    /// <param name="valuesLength">Total length of the values.</param>
    /// <param name="prompt">Prompt shown to the user</param>
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

            this._userInterface.ShowMessage(MessageType.Warning, WarningMessages.EnterValidIndex);
        }
        while (true);
    }

    /// <summary>
    /// Gets the arguments from user for specified parameters.
    /// Arguments are received as string from user and converted to respective parameter types using utilities.
    /// </summary>
    /// <param name="parameters">Parameters to get argument from user.</param>
    /// <returns>Arguments given by the user</returns>
#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
    public Result<object?[]?> GetArguments(ParameterInfo[] parameters)
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
    {
        object?[] arguments = new object?[parameters.Length];
        for (int i = 0; i < parameters.Length; i++)
        {
            try
            {
                do
                {
                    string userInput = this.GetUserInput(string.Format(PromptMessages.EnterArgumentValue, parameters[i].Name, parameters[i].ParameterType.Name));
                    Result<object?> argument = this._utility.ConvertType(userInput, parameters[i].ParameterType);
                    if (argument.IsSuccess)
                    {
                        arguments[i] = argument.Value;
                        break;
                    }

                    this._userInterface.ShowMessage(MessageType.Warning, argument.ErrorMessage);
                }
                while (true);
            }
            catch (Exception ex)
            {
#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
                return Result<object?[]?>.Failure(ex.Message);
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
            }
        }

#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
        return Result<object?[]?>.Success(arguments);
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
    }

    /// <summary>
    /// Displays all the available types and prompts the user and return the type selected by user.
    /// </summary>
    /// <param name="types">Available types.</param>
    /// <param name="prompt">Prompt shown to the user.</param>
    /// <param name="isSupported">
    /// Optional delegate that validates whether a type is supported.
    /// It should return a <see cref="Result{Boolean}"/> indicating success if the type can be selected
    /// or failure with an error message if the type is not allowed.
    /// </param>
    /// <returns>Type selected by user.</returns>
    public Type GetTargetType(Type[] types, string prompt, Func<Type, Result<bool>>? isSupported = null)
    {
        this._userInterface.DisplayTypes(types);
        do
        {
            int typeIndex = this.GetIndex(types.Length, prompt);
            if (isSupported is not null)
            {
                Result<bool> isSupportedType = isSupported(types[typeIndex]);
                if (isSupportedType.IsSuccess && isSupportedType.Value)
                {
                    return types[typeIndex];
                }

                this._userInterface.ShowMessage(MessageType.Warning, isSupportedType.ErrorMessage);
            }
            else
            {
                return types[typeIndex];
            }
        }
        while (true);
    }

    /// <summary>
    /// Prompts the user to select the property from the listed available properties and return the selected property info.
    /// </summary>
    /// <param name="type">Properties of this type is prompted to user to select.</param>
    /// <param name="prompt">Prompt shown to the user.</param>
    /// <param name="isSupported">
    /// Optional delegate that validates whether a property is supported.
    /// It should return a <see cref="Result{Boolean}"/> indicating success if the property can be selected
    /// or failure with an error message if the type is not allowed.
    /// </param>
    /// <returns>Property info of property selected by user.</returns>
    public PropertyInfo GetTargetPropertyInfo(Type type, string prompt, Func<PropertyInfo, Result<bool>>? isSupported = null)
    {
        PropertyInfo[] propertyInfos = type.GetProperties();
        this._userInterface.DisplayTypeProperties(type, propertyInfos);

        do
        {
            int propertyInfoIndex = this.GetIndex(propertyInfos.Length, prompt);
            if (isSupported is not null)
            {
                Result<bool> isSupportedParameter = isSupported(propertyInfos[propertyInfoIndex]);
                if (isSupportedParameter.IsSuccess && isSupportedParameter.Value)
                {
                    return propertyInfos[propertyInfoIndex];
                }

                this._userInterface.ShowMessage(MessageType.Warning, isSupportedParameter.ErrorMessage);
            }
            else
            {
                return propertyInfos[propertyInfoIndex];
            }
        }
        while (true);
    }

    /// <summary>
    /// Prompts the user to select the method from the listed available methods and return the selected method info.
    /// </summary>
    /// <param name="methodInfos">Methods info shown to the user to select.</param>
    /// <param name="prompt">Prompt shown to the user.</param>
    /// <param name="isSupported">
    /// Optional delegate that validates whether a method is supported.
    /// It should return a <see cref="Result{Boolean}"/> indicating success if the method can be selected
    /// or failure with an error message if the type is not allowed.
    /// </param>
    /// <returns>Method info of method selected by user.</returns>
    public MethodInfo GetTargetMethodInfo(MethodInfo[] methodInfos, string prompt, Func<MethodInfo, bool>? isSupported = null)
    {
        this._userInterface.DisplayTypeMethods(methodInfos);
        int methodInfoIndex;
        do
        {
            methodInfoIndex = this.GetIndex(methodInfos.Length, prompt);
            if (isSupported is not null)
            {
                if (isSupported(methodInfos[methodInfoIndex]))
                {
                    return methodInfos[methodInfoIndex];
                }

                this._userInterface.ShowMessage(MessageType.Warning, WarningMessages.MethodInvokeNotSupported);
            }
            else
            {
                return methodInfos[methodInfoIndex];
            }
        }
        while (true);
    }

    /// <summary>
    /// Gets the valid identifier name string from the user.
    /// </summary>
    /// <param name="prompt">Prompt shown to the user.</param>
    /// <returns>User's input value for identifier name.</returns>
    public string GetIdentifierName(string prompt)
    {
        do
        {
            string userInput = this.GetUserInput(prompt);
            Result<bool> isValidIdentifier = this._validator.IsValidIdentifier(userInput);
            if (!isValidIdentifier.IsSuccess)
            {
                this._userInterface.ShowMessage(MessageType.Warning, isValidIdentifier.ErrorMessage);
            }
            else
            {
                return userInput;
            }
        }
        while (true);
    }
}
