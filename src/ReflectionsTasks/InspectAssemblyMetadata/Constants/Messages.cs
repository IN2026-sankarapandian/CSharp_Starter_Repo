namespace InspectAssemblyMetadata.Constants;

/// <summary>
/// Contains messages displayed to the user.
/// </summary>
public static class Messages
{
    /// <summary>
    /// Title message of inspect assembly data plugin
    /// </summary>
    public const string InspectAssemblyDataTitle = "Inspect Assembly Metadata";

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
    /// Error message displayed to the user to inform entered option is invalid and enter a valid option again.
    /// </summary>
    public const string EnterValidOption = "Enter a valid option !";

    /// <summary>
    /// Title message of select kind of member in type to inspect.
    /// </summary>
    public const string SelectMemberKindTitle = "{0} > Select type > Select member kind";

    /// <summary>
    /// Prompt message displayed to the user about options available for the select kind of members menu.
    /// </summary>
    public const string SelectMemberKindOptions = "\n1. Get properties\n2. Get fields\n3. Get methods\n4. Get events\n5. Go back\n";

    /// <summary>
    /// Prompt message displays the type name to user.
    /// </summary>
    public const string TypeName = "Type name : {0}";

    /// <summary>
    /// Prompt message displayed to the user to enter the target type to inspect.
    /// </summary>
    public const string EnterTypeToInspect = "\nEnter which type to inspect : ";

    /// <summary>
    /// Prompt message displayed to the user to enter the kind of member to inspect.
    /// </summary>
    public const string EnterMemberKindToInspect = "\nEnter which kind of member to inspect : ";

    /// <summary>
    /// Titles message to show properties.
    /// </summary>
    public const string PropertyTitle = "{0} > Select type > Select member kind > Properties\n";

    /// <summary>
    /// Titles message to show fields.
    /// </summary>
    public const string FieldTitle = "{0} > Select type > Select member kind > Fields\n";

    /// <summary>
    /// Titles message to show methods.
    /// </summary>
    public const string MethodTitle = "{0} > Select type > Select member kind > Methods\n";

    /// <summary>
    /// Titles message to show events.
    /// </summary>
    public const string EventTitle = "{0} > Select type > Select member kind > Events\n";
}