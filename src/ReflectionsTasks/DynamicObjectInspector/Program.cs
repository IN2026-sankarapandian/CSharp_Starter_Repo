using DynamicObjectInspector.Tasks;
using Reflections.Handlers;
using Reflections.Helpers;
using Reflections.Tasks;
using Reflections.UserInterface;
using Reflections.Utilities;
using Reflections.Validators;

namespace DynamicObjectInspector;

/// <summary>
/// Its an dynamic object inspector which can get and set properties in assemblies dynamically.
/// </summary>
public class Program
{
    /// <summary>
    /// Its and entry point of the dynamic object inspector.
    /// </summary>
    public static void Main()
    {
        IUserInterface userInterface = new ConsoleUI();
        Validator validator = new Validator();
        Utility utility = new Utility();
        FormHandler formHandler = new FormHandler(userInterface, validator, utility);
        AssemblyHelper assemblyHelper = new AssemblyHelper();

        ITask task = new DynamicObjectInspectorTask(userInterface, formHandler, assemblyHelper);
        task.Run();
    }
}