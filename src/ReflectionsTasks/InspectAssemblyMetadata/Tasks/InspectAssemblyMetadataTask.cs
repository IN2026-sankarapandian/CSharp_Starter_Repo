using System.Reflection;
using Reflections.Common;
using Reflections.Enums;
using Reflections.Handlers;
using Reflections.Helpers;
using Reflections.Tasks;
using Reflections.UserInterface;

namespace InspectAssemblyMetadata.Tasks;

/// <summary>
/// Its an dynamic object inspector used to extract information of assemblies.
/// </summary>
public class InspectAssemblyMetadataTask : ITask
{
    private readonly IUserInterface _userInterface;
    private readonly FormHandler _formHandlers;
    private readonly AssemblyHelper _assemblyHelper;

    /// <summary>
    /// Initializes a new instance of the <see cref="InspectAssemblyMetadataTask"/> class.
    /// </summary>
    /// <param name="userInterface"> Provides operations to interact with user.</param>
    /// <param name="formHandlers"> Gets required data from user.</param>
    /// <param name="assemblyHelper">Provide helper methods to manipulate and work with assembly and it related types.</param>
    public InspectAssemblyMetadataTask(IUserInterface userInterface, FormHandler formHandlers, AssemblyHelper assemblyHelper)
    {
        this._userInterface = userInterface;
        this._formHandlers = formHandlers;
        this._assemblyHelper = assemblyHelper;
    }

    /// <inheritdoc/>
    public string Name => "Inspect Assembly Metadata";

    /// <inheritdoc/>
    public void Run()
    {
        Assembly assembly = this.HandleGetAssembly();
        this.HandleMenu(assembly);
    }

    /// <summary>
    /// Handles the process of getting a valid assembly from user.
    /// </summary>
    /// <returns>The loaded assembly instance.</returns>
    private Assembly HandleGetAssembly()
    {
        this._userInterface.ShowMessage(MessageType.Title, this.Name);
        do
        {
            string path = this._formHandlers.GetPath();
            Result<Assembly>? assembly = this._assemblyHelper.LoadAssemblyFile(path);

            if (assembly.IsSuccess)
            {
                return assembly.Value;
            }
            else
            {
                this._userInterface.ShowMessage(MessageType.Warning, assembly.ErrorMessage);
            }
        }
        while (true);
    }

    /// <summary>
    /// Displays the menu to select a type to inspect and route to the proper handler functions
    /// </summary>
    /// <param name="assembly">The assembly to inspect.</param>
    private void HandleMenu(Assembly assembly)
    {
        while (true)
        {
            this._userInterface.ShowMessage(MessageType.Title, string.Format("{0}", this.Name));
            this._userInterface.ShowMessage(MessageType.Information, string.Format("Assembly name : {0}", assembly.FullName));
            this._userInterface.ShowMessage(MessageType.Information, "1. Inspect a type 2. Exit app");
            string? userChoice = this._formHandlers.GetUserInput("Enter what do you want to do : ");
            Type[] types = assembly.GetTypes();
            switch (userChoice)
            {
                case "1":
                    this._userInterface.ShowMessage(MessageType.Title, string.Format("{0} > Select type\n", this.Name));
                    Type targetType = this._formHandlers.GetTargetType(types, "\nEnter which type to inspect : ");
                    this.HandleShowTypeDetailsMenu(targetType);
                    break;
                case "2":
                    return;
                default:
                    this._userInterface.ShowMessage(MessageType.Warning, "Enter a valid option !");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }

    /// <summary>
    /// Displays the menu to select a type of member to inspect and route to the proper display functions
    /// </summary>
    /// <param name="type">Type data of which members to display.</param>
    private void HandleShowTypeDetailsMenu(Type type)
    {
        do
        {
            this._userInterface.ShowMessage(MessageType.Title, string.Format("{0} > Select type > Select member kind", this.Name));
            this._userInterface.ShowMessage(MessageType.Prompt, string.Format("\nType name : {0}", type.Name));
            this._userInterface.ShowMessage(MessageType.Prompt, "\n1. Get properties\n2. Get fields\n3. Get methods\n4. Get events\n5. Go back\n");
            string? userChoice = this._formHandlers.GetUserInput("Enter which kind of members you want to inspect : ");

            switch (userChoice)
            {
                case "1":
                    this._userInterface.ShowMessage(MessageType.Title, string.Format("{0} > Select type > Select member kind > Properties\n", this.Name));
                    this._userInterface.DisplayTypeProperties(type, type.GetProperties());
                    break;
                case "2":
                    this._userInterface.ShowMessage(MessageType.Title, string.Format("{0} > Select type > Select member kind > Fields\n", this.Name));
                    this._userInterface.DisplayTypeFields(type, type.GetFields());
                    break;
                case "3":
                    this._userInterface.ShowMessage(MessageType.Title, string.Format("{0} > Select type > Select member kind > Methods\n", this.Name));
                    this._userInterface.DisplayTypeMethods(type.GetMethods());
                    break;
                case "4":
                    this._userInterface.ShowMessage(MessageType.Title, string.Format("{0} > Select type > Select member kind > Events\n", this.Name));
                    this._userInterface.DisplayTypeEvents(type.GetEvents());
                    break;
                case "5":
                    return;
                default:
                    this._userInterface.ShowMessage(MessageType.Warning, "Not a valid input !");
                    Thread.Sleep(1000);
                    continue;
            }

            this._userInterface.ShowMessage(MessageType.Prompt, "\nPress any key to exit");
            this._userInterface.GetInput();
        }
        while (true);
    }
}
