using System.Reflection;
using Reflections.Enums;
using Reflections.Handlers;
using Reflections.UserInterface;

namespace Reflections.Tasks.Task2;

/// <summary>
/// Its an dynamic object inspector used to extract information of assemblies and change value of properties.
/// </summary>
public class Task2 : ITask
{
    private readonly IUserInterface _userInterface;
    private readonly FormHandlers _formHandlers;
    private readonly AssemblyHelper _assemblyHelper;

    /// <summary>
    /// Initializes a new instance of the <see cref="Task2"/> class.
    /// </summary>
    /// <param name="userInterface"> Provides operations to interact with user.</param>
    public Task2(IUserInterface userInterface, FormHandlers formHandlers, AssemblyHelper assemblyHelper)
    {
        this._userInterface = userInterface;
        this._formHandlers = formHandlers;
        this._assemblyHelper = assemblyHelper;
    }

    /// <inheritdoc/>
    public string Name => "Dynamic object inspector";

    /// <inheritdoc/>
    public void Run()
    {
        do
        {
            this._userInterface.ShowMessage(MessageType.Title, this.Name);
            string path = this._formHandlers.GetPath();
            Result<Assembly>? assembly = this._assemblyHelper.LoadAssemblyFile(path);

            if (assembly.IsSuccess)
            {
                this.HandleMenu(assembly.Value);
                return;
            }
            else
            {
                this._userInterface.ShowMessage(MessageType.Warning, assembly.ErrorMessage);
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
            this._userInterface.ShowMessage(MessageType.Title, string.Format("{0}\n", this.Name));
            this._userInterface.ShowMessage(MessageType.Information, "1. Inspect a type 2. Exit app");
            string? userChoice = this._formHandlers.GetUserInput("\nEnter what do you want to do : ");
            switch (userChoice)
            {
                case "1":
                    do
                    {
                        this._userInterface.ShowMessage(MessageType.Title, string.Format("{0} > Select type\n", this.Name));
                        Type[] types = assembly.GetTypes();
                        Type type = this._formHandlers.GetTargetType(types, "\nEnter which type to inspect : ");
                        if (type.GetProperties().Length == 0)
                        {
                            this._userInterface.ShowMessage(MessageType.Warning, string.Format("The target type {0} don't have properties", type.Name));
                            Thread.Sleep(1000);
                            continue;
                        }

                        Result<object?> typeInstance = this._assemblyHelper.CreateTypeInstance(type);
                        if (!typeInstance.IsSuccess)
                        {
                            this._userInterface.ShowMessage(MessageType.Warning, typeInstance.ErrorMessage);
                            Thread.Sleep(1000);
                            continue;
                        }

                        this.HandleChangePropertyValue(type, typeInstance.Value);
                        break;
                    }
                    while (true);
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

    private void HandleChangePropertyValue(Type type, object? typeInstance)
    {
        do
        {
            PropertyInfo? propertyInfo = this._formHandlers.GetTargetPropertyInfo(type, "\nEnter which property to change value : ");
            if (propertyInfo.CanWrite)
            {
                string? newValue = this._formHandlers.GetUserInput(string.Format("Enter new value for parameter {0}({1}) : ", propertyInfo.Name, propertyInfo.PropertyType.Name));
                Result<bool> isChanged = this._assemblyHelper.ChangePropertyValue(typeInstance, propertyInfo, newValue);
                if (isChanged.IsSuccess)
                {
                    this._userInterface.ShowMessage(MessageType.Highlight, "Property value updated !");
                    this._userInterface.ShowMessage(MessageType.Prompt, "Press enter to exit");
                    Console.ReadKey();
                    break;
                }
                else
                {
                    this._userInterface.ShowMessage(MessageType.Warning, isChanged.ErrorMessage);
                }
            }
            else
            {
                this._userInterface.ShowMessage(MessageType.Warning, "This property is read only, can't edit");
            }
        }
        while (true);
    }
}
