namespace Reflections.Constants;

/// <summary>
/// Provide Error messages displayed to the user.
/// </summary>
public class ErrorMessages
{
    /// <summary>
    /// Error message displayed to the user when user enters empty input.
    /// </summary>
    public const string InputCannotEmpty = "Input cannot be empty !";

    /// <summary>
    /// Error message displayed to the user that no fields exists in the specified type.
    /// </summary>
    public const string NoFieldExists = "No fields exists";

    /// <summary>
    /// Error message displayed to the user to inform entered index is invalid and enter a valid index again.
    /// </summary>
    public const string EnterValidIndex = "Invalid index, Enter a valid index !";

    /// <summary>
    /// Error message displayed to the user to inform entered option is invalid and enter a valid option again.
    /// </summary>
    public const string EnterValidOption = "Enter a valid option !";

    /// <summary>
    /// Error message displayed to the user that no events exists in the specified type.
    /// </summary>
    public const string NoEventExists = "No events exists";

    /// <summary>
    /// Error message displayed to the user that no methods exists in the specified type.
    /// </summary>
    public const string NoMethodExists = "No methods exists";

    /// <summary>
    /// Error message displayed to the user that no properties exists in the specified type.
    /// </summary>
    public const string NoPropertyExists = "No properties exist";

    /// <summary>
    /// Error message displayed to the user that selected method is not supported for invoke.
    /// </summary>
    public const string MethodInvokeNotSupported = "Method invoke for this method is not supported !";

    /// <summary>
    /// Error message displayed to the user to inform the .dll file found at the path is not a .NET assembly file.
    /// </summary>
    public const string NotValidAssembly = "The file at the specified path is not a valid .NET assembly !";

    /// <summary>
    /// Error message displayed to the user that no file exists in the specified path.
    /// </summary>
    public const string NoFileExists = "No file exists in the specified path !";

    /// <summary>
    /// Error message displayed to the user to inform the file at the specified path can't be loaded
    /// </summary>
    public const string FileCantLoaded = "\"The file can't be load {0}\"";

    /// <summary>
    /// Error message displayed to the user to inform that unhandled exception caught while invoking method.
    /// </summary>
    public const string ExceptionCaught = "Exception caught while invoking method : {0}";

    /// <summary>
    /// Error message displayed to the user to inform that the specified type cant be initiated.
    /// </summary>
    public const string TypeCantInitiated = "The specified type can't be initiated, It is a non-instantiable type";

    /// <summary>
    /// Error message displayed to the user created instance of specified type is null.
    /// </summary>
    public const string NullInstance = "Instance is null";

    /// <summary>
    /// Error message displayed to the user that no type exists.
    /// </summary>
    public const string NoTypeExists = "No types exist";

    /// <summary>
    /// Error message displayed to the user that specified type is not supported.
    /// </summary>
    public const string TypeNotSupported = "Type {0} not supported";

    /// <summary>
    /// Error message displayed to the user that entered identifier name is not valid.
    /// </summary>
    public const string NotValidIdentifierName = "Not a valid identifier name";

    /// <summary>
    /// Error message displayed to the user that entered keyword is the reserved keyword in c sharp.
    /// </summary>
    public const string NameIsReservedKeyword = "{0} is a reserved keyword in c#";

    /// <summary>
    /// Error message displayed to the user that can't get value of the property as type can't be initiated.
    /// </summary>
    public const string CantGetValues = "Type can't be initiated, so unable to get values.";

    /// <summary>
    /// Error message displayed to the user that the type instantiation failed.
    /// </summary>
    public const string TypeInstantiationFailed = "Type instantiation failed";

    /// <summary>
    /// Error message displayed to the user when instantiated doesn't implement ICalculator.
    /// </summary>
    public const string InstanceNotImplementICalculator = "The created instance does not implement ICalculator";

    /// <summary>
    /// Error message displayed to the user that calculator don't have add method.
    /// </summary>
    public const string CalculatorNotHaveAddMethod = "ICalculator doesn't have Add method";

    /// <summary>
    /// Error message displayed to the user that calculator don't have subtract method.
    /// </summary>
    public const string CalculatorNotHaveSubtractMethod = "ICalculator doesn't have Subtract method";

    /// <summary>
    /// Error message displayed to the user that type creation returned null.
    /// </summary>
    public const string CreateTypeReturnedNull = "Type creation returned null";

    /// <summary>
    /// Error message displayed to the user that calculator type creation failed.
    /// </summary>
    public const string FailedToCreateInstance = "Failed to create calculator type, ex :{0}";

    /// <summary>
    /// Error message displayed to the user that serialization failed with execption message
    /// </summary>
    public const string SerializationFailed = "Failed to create serialize, ex :{0}";
}
