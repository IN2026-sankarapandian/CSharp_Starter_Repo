using System.Reflection;
using Reflections.Enums;
using Reflections.Handlers;
using Reflections.Tasks;
using Reflections.UserInterface;

namespace AssemblyMetadataInspector;

/// <summary>
/// Its an dynamic object inspector used to extract information of assemblies
/// </summary>
public class Task1 : ITask
{
    private readonly IUserInterface _userInterface;
    private readonly FormHandlers _formHandlers;
    private readonly AssemblyHelper _assemblyHelper;

    /// <summary>
    /// Initializes a new instance of the <see cref="Task1"/> class.
    /// </summary>
    /// <param name="userInterface"> Provides operations to interact with user.</param>
    public Task1(IUserInterface userInterface, FormHandlers formHandlers, AssemblyHelper assemblyHelper)
    {
        _userInterface = userInterface;
        _formHandlers = formHandlers;
        _assemblyHelper = assemblyHelper;
    }

    /// <inheritdoc/>
    public string Name => "Inspect Assembly Metadata";

    /// <inheritdoc/>
    public void Run()
    {
        do
        {
            _userInterface.ShowMessage(MessageType.Title, Name);
            string path = _formHandlers.GetPath();
            Result<Assembly>? assembly = _assemblyHelper.LoadAssemblyFile(path);

            if (assembly.IsSuccess)
            {
                HandleMenu(assembly.Value);
                return;
            }
            else
            {
                _userInterface.ShowMessage(MessageType.Warning, assembly.ErrorMessage);
                Thread.Sleep(1000);
            }
        }
        while (true);
    }

    /// <summary>
    /// Handles menu of this task.
    /// </summary>
    /// <param name="assembly">Assembly to load t</param>
    public void HandleMenu(Assembly assembly)
    {
        while (true)
        {
            _userInterface.ShowMessage(MessageType.Title, string.Format("{0}", Name));
            _userInterface.ShowMessage(MessageType.Information, "1. Inspect a type 2. Exit app");
            string? userChoice = _formHandlers.GetUserInput("Enter what do you want to do : ");
            switch (userChoice)
            {
                case "1":
                    _userInterface.ShowMessage(MessageType.Title, string.Format("{0} > Select type\n", Name));
                    Type[] types = assembly.GetTypes();
                    Type targetType = _formHandlers.GetTargetType(types, "\nEnter which type to inspect : ");
                    HandleShowTypeDetails(targetType);
                    break;
                case "2":
                    return;
                default:
                    _userInterface.ShowMessage(MessageType.Warning, "Enter a valid option !");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }

    /// <summary>
    /// Handle displaying all kind of members of the specified type.
    /// </summary>
    /// <param name="type">Type data of which members to display.</param>
    private void HandleShowTypeDetails(Type type)
    {
        do
        {
            _userInterface.ShowMessage(MessageType.Title, string.Format("{0} > Select type > Select member kind", Name));
            _userInterface.ShowMessage(MessageType.Prompt, string.Format("\nType name : {0}", type.Name));
            _userInterface.ShowMessage(MessageType.Prompt, "\n1. Get properties\n2. Get fields\n3. Get methods\n4. Get events\n5. Go back\n");
            string? userChoice = _formHandlers.GetUserInput("Enter which kind of members you want to inspect : ");

            switch (userChoice)
            {
                case "1":
                    _userInterface.ShowMessage(MessageType.Title, string.Format("{0} > Select type > Select member kind > Properties\n", Name));
                    _userInterface.DisplayTypeProperties(type, type.GetProperties());
                    break;
                case "2":
                    _userInterface.ShowMessage(MessageType.Title, string.Format("{0} > Select type > Select member kind > Fields\n", Name));
                    _userInterface.DisplayTypeFields(type, type.GetFields());
                    break;
                case "3":
                    _userInterface.ShowMessage(MessageType.Title, string.Format("{0} > Select type > Select member kind > Methods\n", Name));
                    _userInterface.DisplayTypeMethods(type.GetMethods());
                    break;
                case "4":
                    _userInterface.ShowMessage(MessageType.Title, string.Format("{0} > Select type > Select member kind > Events\n", Name));
                    _userInterface.DisplayTypeEvents(type.GetEvents());
                    break;
                case "5":
                    return;
                default:
                    _userInterface.ShowMessage(MessageType.Warning, "Not a valid input !");
                    Thread.Sleep(1000);
                    continue;
            }

            _userInterface.ShowMessage(MessageType.Prompt, "\nPress any key to exit");
            _userInterface.GetInput();
        }
        while (true);
    }
}
