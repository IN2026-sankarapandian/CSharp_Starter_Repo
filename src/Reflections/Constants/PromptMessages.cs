namespace Reflections.Constants;

/// <summary>
/// Provide messages for the user.
/// </summary>
public static class PromptMessages
{
    /// <summary>
    /// Prompt message displayed to the user to enter the path of the file.
    /// </summary>
    public const string EnterPath = "Enter a path of the file to inspect : ";

    /// <summary>
    /// Prompt message displayed to the user to enter the desired action.
    /// </summary>
    public const string EnterWhichTaskToRun = "Enter which task to run : ";

    /// <summary>
    /// Prompt message displayed to the user about the test options in mocking framework task.
    /// </summary>
    public const string MockingFrameworkMenuOptions = "1. Test add method\n2. Test subtract method\n3. Exit\n";

    /// <summary>
    /// Prompt message displayed to the user to enter the desired test action.
    /// </summary>
    public const string EnterWhichTestToRun = "Enter what you want to do : ";

    /// <summary>
    /// Prompt message displayed to the user to inform that subtract test failed.
    /// </summary>
    public const string SubTestFailed = "Subtract test failed";

    /// <summary>
    /// Prompt message displayed to the user to inform that subtract test passed.
    /// </summary>
    public const string SubTestPassed = "Subtract test passed";

    /// <summary>
    /// Prompt message displayed to the user to inform that sum test failed.
    /// </summary>
    public const string SumTestFailed = "Sum test failed";

    /// <summary>
    /// Prompt message displayed to the user to inform that sum test passed.
    /// </summary>
    public const string SumTestPassed = "Sum test passed";

    /// <summary>
    /// Prompt message displayed to the user about the time elapsed for serialization with reflection.
    /// </summary>
    public const string SerializationUsingReflection = "Type elapsed for serialization using reflection is {0}";

    /// <summary>
    /// Prompt message displayed to the user about the time elapsed for serialization with reflection emit.
    /// </summary>
    public const string SerializationUsingReflectionEmit = "Type elapsed for serialization using reflection and op code is {0}";

    /// <summary>
    /// Prompt messages displayed to the user to inform press enter to exit.
    /// </summary>
    public const string PressEnterToExit = "Press enter to exit";

    /// <summary>
    /// Prompt message displayed to the user to enter the value for the selected property.
    /// </summary>
    public const string EnterArgumentValue = "Enter value for parameter {0}({1}) : ";
}
