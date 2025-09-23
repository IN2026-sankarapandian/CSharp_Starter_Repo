using InspectAssemblyMetadata.Tasks;
using Reflections.Handlers;
using Reflections.Helpers;
using Reflections.Tasks;
using Reflections.UserInterface;
using Reflections.Utilities;
using Reflections.Validators;

namespace InspectAssemblyMetadata;

/// <summary>
/// Its an dynamic object inspector which allows user to view info about assembly.
/// </summary>
public class Program
{
    /// <summary>
    /// Its an entry point of dynamic object inspector.
    /// </summary>
    public static void Main()
    {
        IUserInterface userInterface = new ConsoleUI();
        Validator validator = new Validator();
        Utility utility = new Utility();
        FormHandler formHandler = new FormHandler(userInterface, validator, utility);
        AssemblyHelper assemblyHelper = new AssemblyHelper();

        ITask task = new InspectAssemblyMetadataTask(userInterface, formHandler, assemblyHelper);
        task.Run();
    }
}