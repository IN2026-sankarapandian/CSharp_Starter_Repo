namespace DynamicObjectInspector.Constants;

/// <summary>
/// Contains messages displayed to the user.
/// </summary>
public static class Messages
{
    /// <summary>
    /// Title message of dynamic object inspector plugin
    /// </summary>
    public const string DynamicObjectInspectorTitle = "Dynamic object inspector";

    /// <summary>
    /// Title message of select assembly menu
    /// </summary>
    public const string SelectAssemblyTitle = "{0} > Select assembly";

    /// <summary>
    /// Title message of inspect type menu
    /// </summary>
    public const string SelectTargetTypeTitle = "{0} > Select assembly > Inspect Type";

    /// <summary>
    /// Prompt message displays the assembly name to user.
    /// </summary>
    public const string AssemblyName = "Assembly name : {0}";

    /// <summary>
    /// Prompt message displayed to the user about options available for the select target type menu
    public const string SelectTargetTypeOptions = "1. Inspect a type 2. Exit app";

    /// <summary>
    /// Prompt message displayed to the user to enter a desired option.
    /// </summary>
    public const string EnterOption = "\nEnter what do you want to do : ";

    /// <summary>
    /// Prompt message displayed to the user to inform press enter to exit.
    /// </summary>
    public const string PressEnterToExit = "\nPress enter to exit";

    /// <summary>
    /// Warning message displayed to the user to inform entered option is invalid and enter a valid option again.
    /// </summary>
    public const string EnterValidOption = "Enter a valid option !";

    /// <summary>
    /// Title message of select target type menu
    /// </summary>
    public const string SelectTargetPropertyTitle = "{0} > Select assembly > Select Type > Select and update property";

    /// <summary>
    /// Prompt message displayed to the user about options available for the select target property menu.
    /// </summary>
    public const string SelectTargetPropertyOptions = "1. Select and update a property 2. Go back";

    /// <summary>
    /// Prompt message displays the type name to user.
    /// </summary>
    public const string TypeName = "Type name : {0}";

    /// <summary>
    /// Prompt message displayed to the user to enter the target type to inspect.
    /// </summary>
    public const string EnterTypeToInspect = "\nEnter which type to inspect : ";

    /// <summary>
    /// Prompt message displayed to user to enter the target property to change its value.
    /// </summary>
    public const string EnterPropertyToInspect = "\nEnter which property to change value : ";

    /// <summary>
    /// Prompt message displayed to the user to enter new value.
    /// </summary>
    public const string EnterNewValue = "Enter new value for parameter {0}({1}) : ";

    /// <summary>
    /// Prompt message displayed to the user the property value updated.
    /// </summary>
    public const string PropertyValueUpdated = "Property value updated !";

    /// <summary>
    /// Warning message displayed to the user that input cannot be null.
    /// </summary>
    public const string InputCannotBeNull = "Input cannot be null";

    /// <summary>
    /// Warning message displayed to the user that type is not supported.
    /// </summary>
    public const string TypeNotSupported = "Type {0} not supported";

    /// <summary>
    /// Warning message displayed to the user that property restricts writing.
    /// </summary>
    public const string NotWritableProperty = "Property restricts write";

    /// <summary>
    /// Warning message displayed to the user that property type is not supported.
    /// </summary>
    public const string NotSupportedProperty = "Property type not supported";

    /// <summary>
    /// Warning message displayed to the user that type can't be initiated.
    /// </summary>
    public const string TypeCantInitiated = "Type can't be initiated, so cant update property values";

    /// <summary>
    /// Property message displayed to the user that no properties found.
    /// </summary>
    public const string NoProperties = "No properties found in this type";

    /// <summary>
    /// Prompt message displayed to the user that no supported properties found.
    /// </summary>
    public const string NoSupportedProperties = "No supported properties found in this type";
}
