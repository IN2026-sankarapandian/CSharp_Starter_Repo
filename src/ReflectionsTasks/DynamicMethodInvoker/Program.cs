using DynamicMethodInvoker.Tasks;
using Reflections.Handlers;
using Reflections.Helpers;
using Reflections.Tasks;
using Reflections.UserInterface;
using Reflections.Utilities;
using Reflections.Validators;

namespace DynamicMethodInvoker;

/// <summary>
/// Its an dynamic method invoker which can invoke methods in assemblies dynamically.
/// </summary>
public class Program
{
    /// <summary>
    /// Its and entry point of the dynamic method invoker.
    /// </summary>
    public static void Main()
    {
        IUserInterface userInterface = new ConsoleUI();
        Validator validator = new Validator();
        Utility utility = new Utility();
        FormHandler formHandler = new FormHandler(userInterface, validator, utility);
        AssemblyHelper assemblyHelper = new AssemblyHelper();

        ITask task = new DynamicMethodInvokerTask(userInterface, formHandler, assemblyHelper);
        task.Run();
    }
}