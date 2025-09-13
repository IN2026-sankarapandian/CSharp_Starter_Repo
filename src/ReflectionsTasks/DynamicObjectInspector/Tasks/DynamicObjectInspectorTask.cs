using System.Reflection;
using DynamicObjectInspector.Constants;
using Reflections.Common;
using Reflections.Enums;
using Reflections.Handlers;
using Reflections.Helpers;
using Reflections.Tasks;
using Reflections.UserInterface;

namespace DynamicObjectInspector.Tasks;

/// <summary>
/// Its an dynamic object inspector used to extract information of assemblies and change value of properties.
/// </summary>
public class DynamicObjectInspectorTask : ITask
{
    private readonly IUserInterface _userInterface;
    private readonly FormHandler _formHandlers;
    private readonly AssemblyHelper _assemblyHelper;

    /// <summary>
    /// Initializes a new instance of the <see cref="DynamicObjectInspectorTask"/> class.
    /// </summary>
    /// <param name="userInterface"> Provides operations to interact with user.</param>
    /// <param name="formHandlers"> Gets required data from user.</param>
    /// <param name="assemblyHelper">Provide helper methods to manipulate and work with assembly and it related types.</param>
    public DynamicObjectInspectorTask(IUserInterface userInterface, FormHandler formHandlers, AssemblyHelper assemblyHelper)
    {
        this._userInterface = userInterface;
        this._formHandlers = formHandlers;
        this._assemblyHelper = assemblyHelper;
    }

    /// <inheritdoc/>
    public string Name => Messages.DynamicObjectInspectorTitle;

    /// <inheritdoc/>
    public void Run()
    {
        Assembly assembly = this.HandleGetAssembly();
        this.HandleSelectTargetTypeMenu(assembly);
    }

    /// <summary>
    /// Handles the process of getting a valid assembly from user.
    /// </summary>
    /// <returns>The loaded assembly instance.</returns>
    private Assembly HandleGetAssembly()
    {
        do
        {
            string path = this._formHandlers.GetPath();
            Result<Assembly>? assembly = this._assemblyHelper.LoadAssemblyFile(path);

            if (assembly.IsSuccess)
            {
                return assembly.Value;
            }
            else
            {
                this._userInterface.ShowMessage(MessageType.Warning, assembly.ErrorMessage);
            }
        }
        while (true);
    }

    /// <summary>
    /// Displays the menu to select target type and route to the proper handler functions
    /// </summary>
    /// <param name="assembly">The assembly to inspect.</param>
    private void HandleSelectTargetTypeMenu(Assembly assembly)
    {
        while (true)
        {
            this._userInterface.ShowMessage(MessageType.Title, string.Format(Messages.SelectTargetTypeTitle, this.Name));
            this._userInterface.ShowMessage(MessageType.Information, string.Format(Messages.AssemblyName, assembly.FullName));
            this._userInterface.ShowMessage(MessageType.Information, Messages.SelectTargetTypeOptions);
            string? userChoice = this._formHandlers.GetUserInput(Messages.EnterOption);
            switch (userChoice)
            {
                case "1":
                    Type[] types = assembly.GetTypes();
                    object typeInstance = this.HandleSelectTargetType(types);
                    this.HandleSelectTargetPropertyMenu(typeInstance);
                    break;
                case "2":
                    return;
                default:
                    this._userInterface.ShowMessage(MessageType.Warning, Messages.EnterValidOption;
                    Thread.Sleep(1000);
                    break;
            }
        }
    }

    /// <summary>
    /// Displays the menu to select target property and route to the proper handler functions
    /// </summary>
    /// <param name="typeInstance">The type instance to inspect</param>
    private void HandleSelectTargetPropertyMenu(object typeInstance)
    {
        while (true)
        {
            this._userInterface.ShowMessage(MessageType.Title, string.Format(Messages.SelectTargetPropertyTitle, this.Name));
            this._userInterface.ShowMessage(MessageType.Information, string.Format(Messages.TypeName, typeInstance.GetType().Name));
            this._userInterface.ShowMessage(MessageType.Information, Messages.SelectTargetPropertyOptions);
            string? userChoice = this._formHandlers.GetUserInput(Messages.PressEnterToExit);
            switch (userChoice)
            {
                case "1":
                    this.HandleUpdateProperty(typeInstance);
                    break;
                case "2":
                    return;
                default:
                    this._userInterface.ShowMessage(MessageType.Warning, Messages.EnterValidOption);
                    Thread.Sleep(1000);
                    break;
            }
        }
    }

    /// <summary>
    /// Handles the process of getting a supported type from the user.
    /// </summary>
    /// <param name="types">Types to shown for the user</param>
    /// <returns>The instance of type selected by user.</returns>
    private object HandleSelectTargetType(Type[] types)
    {
        do
        {
            Type type = this._formHandlers.GetTargetType(types, Messages.EnterTypeToInspect, this.IsSupportedType);

            Result<object> typeInstance = this._assemblyHelper.CreateTypeInstance(type);
            if (!typeInstance.IsSuccess)
            {
                this._userInterface.ShowMessage(MessageType.Warning, typeInstance.ErrorMessage);
            }

            return typeInstance.Value;
        }
        while (true);
    }

    /// <summary>
    /// Handle the process of getting the target property and updating it value.
    /// </summary>
    /// <param name="typeInstance">Type instance whose property needs to updated</param>
    private void HandleUpdateProperty(object typeInstance)
    {
        do
        {
            PropertyInfo? propertyInfo = this._formHandlers.GetTargetPropertyInfo(typeInstance.GetType(), Messages.EnterPropertyToInspect, this.IsSupportedProperty);
            string? newValue = this._formHandlers.GetUserInput(string.Format(Messages.EnterNewValue, propertyInfo.Name, propertyInfo.PropertyType.Name));
            Result<object> convertedResult = this.ConvertType(newValue, propertyInfo.PropertyType);

            if (convertedResult.IsSuccess)
            {
                Result<bool> propertyValueChangeResult = this.ChangePropertyValue(typeInstance, propertyInfo, convertedResult.Value);
                if (propertyValueChangeResult.IsSuccess)
                {
                    this._userInterface.ShowMessage(MessageType.Highlight, Messages.PropertyValueUpdated);
                    this._userInterface.ShowMessage(MessageType.Prompt, Messages.PressEnterToExit);
                    Console.ReadKey();
                    break;
                }
                else
                {
                    this._userInterface.ShowMessage(MessageType.Warning, propertyValueChangeResult.ErrorMessage);
                }
            }
            else
            {
                this._userInterface.ShowMessage(MessageType.Warning, convertedResult.ErrorMessage);
            }
        }
        while (true);
    }

    /// <summary>
    /// Attempts to change the value of specified property.
    /// </summary>
    /// <param name="typeInstance">Type instance of the specified property.</param>
    /// <param name="propertyInfo">Target property info</param>
    /// <param name="newValue">New value to be assigned for target property.</param>
    /// <returns><see cref="Result{Assembly}"/> object indicating success changing property value or failure with error message.</returns>
    private Result<bool> ChangePropertyValue(object? typeInstance, PropertyInfo propertyInfo, object newValue)
    {
        try
        {
            propertyInfo.SetValue(typeInstance, newValue);
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

    /// <summary>
    /// Attempts to convert the given input string to the specified target type.
    /// Currently it supports primitive types, decimal, string.
    /// </summary>
    /// <param name="input">Input string.</param>
    /// <param name="type">Target type.</param>
    /// <returns><see cref="Result{Assembly}"/> object indicating success if property can be written to or failure with error message.</returns>
    private Result<object> ConvertType(string? input, Type type)
    {
        if (input is null)
        {
            return Result<object>.Failure(Messages.InputCannotBeNull);
        }

        if (type == typeof(string))
        {
            return Result<object>.Success(input);
        }

        if (type.IsPrimitive || type == typeof(decimal))
        {
            try
            {
                object? converted = Convert.ChangeType(input, type);
                if (converted is not null)
                {
                    return Result<object>.Success(converted);
                }
            }
            catch (Exception ex)
            {
                return Result<object>.Failure(ex.Message);
            }
        }

        return Result<object>.Failure(string.Format(Messages.TypeNotSupported, type.Name));
    }

    /// <summary>
    /// Validates whether the given property is supported for editing.
    /// </summary>
    /// <param name="propertyInfo">Property info of the property to check.</param>
    /// <returns><see cref="Result{Assembly}"/> object indicating success changing property value or failure with error message.</returns>
    private Result<bool> IsSupportedProperty(PropertyInfo propertyInfo)
    {
        if (!propertyInfo.CanWrite)
        {
            return Result<bool>.Failure(Messages.NotWritableProperty);
        }

        Type propType = propertyInfo.PropertyType;

        if (!(propType == typeof(string) ||
              propType == typeof(decimal) ||
              propType.IsPrimitive))
        {
            return Result<bool>.Failure(Messages.NotSupportedProperty);
        }

        return Result<bool>.Success(true);
    }

    /// <summary>
    /// Validates whether the type is supported to inspect or property value edit.
    /// A type is considered unsupported if it is abstract, an interface, requires constructor arguments,
    /// or contains only unsupported properties.
    /// </summary>
    /// <param name="type">Type to check.</param>
    /// <returns><see cref="Result{Assembly}"/> object indicating success if type can be instantiated and contains atleast one supported property ;otherwise failure with error message.</returns>
    private Result<bool> IsSupportedType(Type type)
    {
        if (type.IsInterface && type.IsAbstract && type.GetConstructor(Type.EmptyTypes) == null && type.ContainsGenericParameters)
        {
            return Result<bool>.Failure(Messages.TypeCantInitiated);
        }

        var properties = type.GetProperties();

        if (properties.Length == 0)
        {
            return Result<bool>.Failure(Messages.NoProperties);
        }

        foreach (var prop in properties)
        {
            var result = this.IsSupportedProperty(prop);
            if (result.IsSuccess)
            {
                return Result<bool>.Success(true);
            }
        }

        return Result<bool>.Failure(Messages.NoSupportedProperties);
    }
}
