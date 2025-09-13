namespace Reflections.Constants;

/// <summary>
/// Provide warning messages displayed to the user.
/// </summary>
public class WarningMessages
{
    /// <summary>
    /// Warning message displayed to the user when user enters empty input.
    /// </summary>
    public const string InputCannotEmpty = "Input cannot be empty !";

    /// <summary>
    /// Warning message displayed to the user that no fields exists in the specified type.
    /// </summary>
    public const string NoFieldExists = "No fields exists";

    /// <summary>
    /// Warning message displayed to the user to inform entered index is invalid and enter a valid index again.
    /// </summary>
    public const string EnterValidIndex = "Invalid index, Enter a valid index !";

    /// <summary>
    /// Warning message displayed to the user to inform entered option is invalid and enter a valid option again.
    /// </summary>
    public const string EnterValidOption = "Enter a valid option !";

    /// <summary>
    /// Warning message displayed to the user that no events exists in the specified type.
    /// </summary>
    public const string NoEventExists = "No events exists";

    /// <summary>
    /// Warning message displayed to the user that no methods exists in the specified type.
    /// </summary>
    public const string NoMethodExists = "No methods exists";

    /// <summary>
    /// Warning message displayed to the user that no properties exists in the specified type.
    /// </summary>
    public const string NoPropertyExists = "No properties exist";

    /// <summary>
    /// Warning message displayed to the user that selected method is not supported for invoke.
    /// </summary>
    public const string MethodInvokeNotSupported = "Method invoke for this method is not supported !";

    /// <summary>
    /// Warning message displayed to the user to inform the .dll file found at the path is not a .NET assembly file.
    /// </summary>
    public const string NotValidAssembly = "The file at the specified path is not a valid .NET assembly !";

    /// <summary>
    /// Warning message displayed to the user that no file exists in the specified path.
    /// </summary>
    public const string NoFileExists = "No file exists in the specified path !";

    /// <summary>
    /// Warning message displayed to the user to inform the file at the specified path can't be loaded
    /// </summary>
    public const string FileCantLoaded = "\"The file can't be load {0}\"";

    /// <summary>
    /// Warning message displayed to the user to inform that unhandled exception caught while invoking method.
    /// </summary>
    public const string ExceptionCaught = "Exception caught while invoking method : {0}";

    /// <summary>
    /// Warning message displayed to the user to inform that the specified type cant be initiated.
    /// </summary>
    public const string TypeCantInitiated = "The specified type can't be initiated, It is a non-instantiable type";

    /// <summary>
    /// Warning message displayed to the user created instance of specified type is null.
    /// </summary>
    public const string NullInstance = "Instance is null";

    /// <summary>
    /// Warning message displayed to the user that no type exists.
    /// </summary>
    public const string NoTypeExists = "No types exist";

    /// <summary>
    /// Warning message displayed to the user that specified type is not supported.
    /// </summary>
    public const string TypeNotSupported = "Type {0} not supported";

    /// <summary>
    /// Warning message displayed to the user that entered identifier name is not valid.
    /// </summary>
    public const string NotValidIdentifierName = "Not a valid identifier name";

    /// <summary>
    /// Warning message displayed to the user that entered keyword is the reserved keyword in c sharp.
    /// </summary>
    public const string NameIsReservedKeyword = "{0} is a reserved keyword in c#";

    /// <summary>
    /// Warning message displayed to the user that can't get value of the property as type can't be initiated.
    /// </summary>
    public const string CantGetValues = "Type can't be initiated, so unable to get values.";

    /// <summary>
    /// Warning message displayed to the user that the type instantiation failed.
    /// </summary>
    public const string TypeInstantiationFailed = "Type instantiation failed";
}
