using Reflections.Enums;
using Reflections.Handlers;
using Reflections.UserInterface;
using System.Reflection;

namespace Reflections.Tasks.Task2;

/// <summary>
/// Its an dynamic method invoker used to invoke any methods of assemblies
/// </summary>
public class Task3 : ITask
{
    private readonly IUserInterface _userInterface;
    private readonly FormHandlers _formHandlers;
    private readonly AssemblyHelper _assemblyHelper;

    /// <summary>
    /// Initializes a new instance of the <see cref="Task3"/> class.
    /// </summary>
    /// <param name="userInterface"> Provides operations to interact with user.</param>
    /// <param name="formHandlers"> Gets required data from user.</param>
    public Task3(IUserInterface userInterface, FormHandlers formHandlers, AssemblyHelper assembly)
    {
        this._userInterface = userInterface;
        this._formHandlers = formHandlers;
        this._assemblyHelper = assembly;
    }

    /// <inheritdoc/>
    public string Name => "Dynamic method invoker";

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
        bool isRunning = true;
        do
        {
            this._userInterface.ShowMessage(MessageType.Title, string.Format("{0}\n", this.Name));
            this._userInterface.ShowMessage(MessageType.Prompt, "1. Inspect a type 2. Exit app");
            this._userInterface.ShowMessage(MessageType.Prompt, "\nEnter what do you want to do : ");
            string? userChoice = Console.ReadLine();
            switch (userChoice)
            {
                case "1":
                    this._userInterface.ShowMessage(MessageType.Title, string.Format("{0} > Select type\n", this.Name));
                    Type[] types = assembly.GetTypes();
                    Type type = this._formHandlers.GetTargetType(types, "\nEnter which type to inspect : ");
                    MethodInfo[] methodInfos = type.GetMethods();
                    if (methodInfos.Length == 0)
                    {
                        this._userInterface.ShowMessage(MessageType.Warning, string.Format("The target type {0} don't have methods", type.Name));
                        Thread.Sleep(1000);
                        continue;
                    }

                    MethodInfo methodinfo = this._formHandlers.GetTargetMethodInfo(methodInfos, "\nEnter which method to invoke : ");
                    Result<object?> typeInstance = this._assemblyHelper.CreateTypeInstance(type);

                    if (typeInstance.IsSuccess)
                    {
                        this.HandleInvokeMethod(typeInstance.Value, methodinfo);
                    }
                    else
                    {
                        this._userInterface.ShowMessage(MessageType.Warning, typeInstance.ErrorMessage);
                    }

                    this._userInterface.ShowMessage(MessageType.Prompt, "\nPress any key to exit");
                    Console.ReadKey();
                    break;
                case "2":
                    isRunning = false;
                    break;
                default:
                    this._userInterface.ShowMessage(MessageType.Prompt, "Enter a valid option !");
                    break;
            }
        }
        while (isRunning);
    }

    /// <summary>
    /// Gets the arguments (if any) from the user and invoke the specified method
    /// </summary>
    /// <param name="typeInstance">Instance of the type to invoke the method</param>
    /// <param name="method">Method to invoke</param>
    private void HandleInvokeMethod(object typeInstance, MethodInfo method)
    {
        ParameterInfo[] parameters = method.GetParameters();
        Result<object?[]?> arguments = this._formHandlers.GetArguments(parameters);

        if (!arguments.IsSuccess)
        {
            this._userInterface.ShowMessage(MessageType.Warning, "Invalid arguments, method invoking failed !");
            return;
        }

        // If there is no parameters, the method can be invoked otherwise arguments should not be null
        if (parameters.Length == 0 || arguments is not null)
        {
            Result<object> result = this._assemblyHelper.InvokeMethod(typeInstance, method, arguments.Value);
            if (result.IsSuccess)
            {
                this._userInterface.ShowMessage(MessageType.Highlight, string.Format("Result : {0}", result.Value ?? "null"));
            }
            else
            {
                this._userInterface.ShowMessage(MessageType.Warning, result.ErrorMessage);
            }
        }
    }
}
