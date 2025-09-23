namespace DynamicMethodInvoker.Constants;

/// <summary>
/// Contains messages displayed to the user.
/// </summary>
public class Messages
{
    /// <summary>
    /// Title message of dynamic method invoker plugin
    /// </summary>
    public const string DynamicMethodInvokerTitle = "Dynamic method invoker";

    /// <summary>
    /// Title message of select assembly menu
    /// </summary>
    public const string SelectAssemblyTitle = "{0} > Select assembly";

    /// <summary>
    /// Title message of inspect type menu
    /// </summary>
    public const string InspectTypeTitle = "{0} > Select assembly > Inspect Type";

    /// <summary>
    /// Prompt message displays the assembly name to user.
    /// </summary>
    public const string AssemblyName = "Assembly name : {0}";

    /// <summary>
    /// Prompt message displayed to the user about options available for the select target type menu
    /// </summary>
    public const string SelectTargetTypeMenuOptions = "1. Inspect a type 2. Exit app";

    /// <summary>
    /// Prompt message displayed to the user to enter a desired option.
    /// </summary>
    public const string EnterOption = "\nEnter what do you want to do : ";

    /// <summary>
    /// Prompt message displayed to the user to inform press enter to exit.
    /// </summary>
    public const string PressEnterToExit = "\nPress enter to exit";

    /// <summary>
    /// Error message displayed to the user to inform entered option is invalid and enter a valid option again.
    /// </summary>
    public const string EnterValidOption = "Enter a valid option !";

    /// <summary>
    /// Title message of select target method menu
    /// </summary>
    public const string SelectTargetMethodTitle = "{0} > Select assembly > Select Type > Select target method";

    /// <summary>
    /// Prompt message displays the type name to user.
    /// </summary>
    public const string TypeName = "Type name : {0}";

    /// <summary>
    /// Prompt message displayed to the user about options available for the select target method menu.
    /// </summary>
    public const string SelectTargetMethodOptions = "1. Invoke method 2. Go back";

    /// <summary>
    /// Prompt message displayed to the user to enter the index of method to invoke.
    /// </summary>
    public const string EnterMethodToInvoke = "\nEnter which method to invoke : ";

    /// <summary>
    /// Prompt message displayed to the user to enter the target type to inspect.
    /// </summary>
    public const string EnterTypeToInspect = "\nEnter which type to inspect : ";

    /// <summary>
    /// Error message displayed to the user that given arguments are invalid
    /// </summary>
    public const string InvalidArguments = "Invalid arguments, method invoking failed !";

    /// <summary>
    /// Prompt message displays the result to user.
    /// </summary>
    public const string Result = "Result : {0}";

    /// <summary>
    /// Prompt message displays the result for not applicable value to the user.
    /// </summary>
    public const string NotApplicableResult = "Result : N/A (void function)";

    /// <summary>
    /// Error message displayed to the user that method can't be invoked because type can't be initiated.
    /// </summary>
    public const string TypeCantBeInitiated = "Type can't be initiated, so cant invoke methods values";

    /// <summary>
    /// Error message displayed to the user that no method found in this type.
    /// </summary>
    public const string NoMethodsFound = "No methods found in this type";
}
