using AssemblyMetadataInspector.Task1;
using Reflections.Handlers;
using Reflections.UserInterface;
using Reflections.Validators;

namespace AssemblyMetadataInspector;

public class Program
{
    static void Main(string[] args)
    {
        AssemblyHelper assemblyHelper = new AssemblyHelper();
        IUserInterface userInterface = new ConsoleUI();
        FormHandlers formHandlers = new FormHandlers(userInterface, new Validator(), new Reflections.Utilities.Utility());
        new Task1().Run();
    }
}