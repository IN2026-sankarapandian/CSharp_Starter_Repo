using System.Reflection;
using InspectAssemblyMetadata.Constants;
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
    public string Name => Messages.InspectAssemblyDataTitle;

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
        this._userInterface.ShowMessage(MessageType.Title, string.Format(Messages.SelectAssemblyTitle, this.Name));
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
            this._userInterface.ShowMessage(MessageType.Title, string.Format(Messages.SelectTargetTypeTitle, this.Name));
            this._userInterface.ShowMessage(MessageType.Information, string.Format(Messages.AssemblyName, assembly.FullName));
            this._userInterface.ShowMessage(MessageType.Information, Messages.SelectTargetTypeOptions);

            string? userChoice = this._formHandlers.GetUserInput(Messages.EnterOption);
            if (userChoice.Equals("1"))
            {
                Type[] types = assembly.GetTypes();
                Type targetType = this._formHandlers.GetTargetType(types, Messages.EnterTypeToInspect);
                this.HandleShowTypeDetailsMenu(targetType);
            }
            else if (userChoice.Equals("1"))
            {
                return;
            }
            else
            {
                this._userInterface.ShowMessage(MessageType.Warning, Messages.EnterValidOption);
                Thread.Sleep(1000);
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
            this._userInterface.ShowMessage(MessageType.Title, string.Format(Messages.SelectMemberKindTitle, this.Name));
            this._userInterface.ShowMessage(MessageType.Prompt, string.Format(Messages.TypeName, type.Name));
            this._userInterface.ShowMessage(MessageType.Prompt, Messages.SelectMemberKindOptions);

            string? userChoice = this._formHandlers.GetUserInput(Messages.EnterMemberKindToInspect);
            if (userChoice.Equals("1"))
            {
                this._userInterface.ShowMessage(MessageType.Title, string.Format(Messages.PropertyTitle, this.Name));
                this._userInterface.DisplayTypeProperties(type, type.GetProperties());
            }
            else if (userChoice.Equals("1"))
            {
                this._userInterface.ShowMessage(MessageType.Title, string.Format(Messages.FieldTitle, this.Name));
                this._userInterface.DisplayTypeFields(type, type.GetFields());
            }
            else if (userChoice.Equals("1"))
            {
                this._userInterface.ShowMessage(MessageType.Title, string.Format(Messages.MethodTitle, this.Name));
                this._userInterface.DisplayTypeMethods(type.GetMethods());
            }
            else if (userChoice.Equals("1"))
            {
                this._userInterface.ShowMessage(MessageType.Title, string.Format(Messages.EventTitle, this.Name));
                this._userInterface.DisplayTypeEvents(type.GetEvents());
            }
            else
            {
                this._userInterface.ShowMessage(MessageType.Warning, Messages.EventTitle);
                Thread.Sleep(1000);
            }

            this._userInterface.ShowMessage(MessageType.Prompt, Messages.PressEnterToExit);
            this._userInterface.GetInput();
        }
        while (true);
    }
}
