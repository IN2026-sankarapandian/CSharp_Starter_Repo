using DynamicTypeBuilder.Tasks;
using Reflections.Handlers;
using Reflections.Helpers;
using Reflections.Tasks;
using Reflections.UserInterface;
using Reflections.Utilities;
using Reflections.Validators;

namespace DynamicTypeBuilder;

/// <summary>
/// Its an dynamic object inspector which can build type dynamically.
/// </summary>
public class Program
{
    /// <summary>
    /// Its an entry point for dynamic object inspector.
    /// </summary>
    public static void Main()
    {
        IUserInterface userInterface = new ConsoleUI();
        Validator validator = new Validator();
        Utility utility = new Utility();
        FormHandler formHandler = new FormHandler(userInterface, validator, utility);
        AssemblyHelper assemblyHelper = new AssemblyHelper();

        ITask task = new DynamicTypeBuilderTask(userInterface, formHandler);
        task.Run();
    }
}