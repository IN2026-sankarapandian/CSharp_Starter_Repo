using System.Reflection;
using Reflections.Common;
using Reflections.Enums;
using Reflections.Handlers;
using Reflections.Helpers;
using Reflections.Tasks;
using Reflections.UserInterface;

namespace DynamicMethodInvoker.Tasks;

/// <summary>
/// Its an dynamic method invoker used to invoke any methods of assemblies
/// </summary>
public class DynamicMethodInvokerTask : ITask
{
    private readonly IUserInterface _userInterface;
    private readonly FormHandler _formHandlers;
    private readonly AssemblyHelper _assemblyHelper;

    /// <summary>
    /// Initializes a new instance of the <see cref="DynamicMethodInvokerTask"/> class.
    /// </summary>
    /// <param name="userInterface"> Provides operations to interact with user.</param>
    /// <param name="formHandlers"> Gets required data from user.</param>
    /// <param name="assemblyHelper">Provide helper methods to manipulate and work with assembly and it related types.</param>
    public DynamicMethodInvokerTask(IUserInterface userInterface, FormHandler formHandlers, AssemblyHelper assemblyHelper)
    {
        this._userInterface = userInterface;
        this._formHandlers = formHandlers;
        this._assemblyHelper = assemblyHelper;
    }

    /// <inheritdoc/>
    public string Name => "Dynamic method invoker";

    /// <inheritdoc/>
    public void Run()
    {
        Assembly assembly = this.HandleGetAssembly();
        this.HandleSelectTargetTypeMenu(assembly);
    }

    /// <summary>
    /// Handles the process of getting a valid assembly from user.
    /// </summary>
    /// <returns>The loaded assembly instance.</returns>
    private Assembly HandleGetAssembly()
    {
        this._userInterface.ShowMessage(MessageType.Title, string.Format("{0} > Select assembly", this.Name));
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
    /// Handles menu of this task.
    /// </summary>
    /// <param name="assembly">Assembly to load t</param>
    private void HandleSelectTargetTypeMenu(Assembly assembly)
    {
        bool isRunning = true;
        do
        {
            this._userInterface.ShowMessage(MessageType.Title, string.Format("{0} > Select assembly > Inspect Type", this.Name));
            this._userInterface.ShowMessage(MessageType.Information, string.Format("Assembly name : {0}", assembly.FullName));
            this._userInterface.ShowMessage(MessageType.Prompt, "1. Inspect a type 2. Exit app");
            this._userInterface.ShowMessage(MessageType.Prompt, "\nEnter what do you want to do : ");
            string? userChoice = Console.ReadLine();
            switch (userChoice)
            {
                case "1":
                    Type[] types = assembly.GetTypes();
                    object instance = this.HandleSelectTargetType(types);
                    this.HandleSelectTargetMethodMenu(instance);
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
    /// Displays the menu to select target method and route to the proper handler functions
    /// </summary>
    /// <param name="typeInstance">The type instance to inspect</param>
    private void HandleSelectTargetMethodMenu(object typeInstance)
    {
        while (true)
        {
            this._userInterface.ShowMessage(MessageType.Title, string.Format("{0} > Select assembly > Select Type > Select and update property", this.Name));
            this._userInterface.ShowMessage(MessageType.Information, string.Format("Type name : {0}", typeInstance.GetType().Name));
            this._userInterface.ShowMessage(MessageType.Information, "1. Invoke method 2. Go back");
            string? userChoice = this._formHandlers.GetUserInput("\nEnter what do you want to do : ");
            switch (userChoice)
            {
                case "1":
                    Type type = typeInstance.GetType();
                    MethodInfo[] methodInfos = type.GetMethods();
                    MethodInfo methodInfo = this._formHandlers.GetTargetMethodInfo(methodInfos, "\nEnter which method to invoke : ", this.IsSupportedMethod);
                    this.HandleInvokeMethod(typeInstance, methodInfo);
                    this._userInterface.ShowMessage(MessageType.Prompt, "Press enter to exit");
                    Console.ReadKey();
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
    /// Handles the process of getting a supported type from the user.
    /// </summary>
    /// <param name="types">Types to shown for the user</param>
    /// <returns>The instance of type selected by user.</returns>
    private object HandleSelectTargetType(Type[] types)
    {
        do
        {
            Type type = this._formHandlers.GetTargetType(types, "\nEnter which type to inspect : ", this.IsSupportedType);

            Result<object> typeInstance = this._assemblyHelper.CreateTypeInstance(type);
            if (!typeInstance.IsSuccess)
            {
                this._userInterface.ShowMessage(MessageType.Warning, typeInstance.ErrorMessage);
            }

            return typeInstance.Value;
        }
        while (true);
    }

    /// <summary>
    /// Gets the arguments (if any) from the user and invoke the specified method
    /// </summary>
    /// <param name="typeInstance">Instance of the type to invoke the method</param>
    /// <param name="method">Method to invoke</param>
    private void HandleInvokeMethod(object typeInstance, MethodInfo method)
    {
        ParameterInfo[] parameters = method.GetParameters();
#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
        Result<object?[]?> arguments = this._formHandlers.GetArguments(parameters);
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly

        if (!arguments.IsSuccess)
        {
            this._userInterface.ShowMessage(MessageType.Warning, "Invalid arguments, method invoking failed !");
            return;
        }

        // If there is no parameters, the method can be invoked otherwise arguments should not be null
        if (parameters.Length == 0 || arguments is not null)
        {
            Result<object?> result = this._assemblyHelper.InvokeMethod(typeInstance, method, arguments.Value);
            if (result.IsSuccess)
            {
                if (result.Value is not null)
                {
                    this._userInterface.ShowMessage(MessageType.Highlight, string.Format("Result : {0}", result.Value));
                }
                else
                {
                    this._userInterface.ShowMessage(MessageType.Highlight, string.Format("Result : N/A (void function)"));
                }
            }
            else
            {
                this._userInterface.ShowMessage(MessageType.Warning, result.ErrorMessage);
            }
        }
    }

    /// <summary>
    /// Validates whether the type is supported to inspect or invoke method.
    /// A type is considered unsupported if it is abstract, an interface, requires constructor arguments.
    /// </summary>
    /// <param name="type">Type to check.</param>
    /// <returns><see cref="Result{Assembly}"/> object indicating success if type can be instantiated ;otherwise failure with error message.</returns>
    private Result<bool> IsSupportedType(Type type)
    {
        if (type.IsInterface && type.IsAbstract && type.GetConstructor(Type.EmptyTypes) == null && type.ContainsGenericParameters)
        {
            return Result<bool>.Failure("Type can't be initiated, so cant invoke methods values");
        }

        var methods = type.GetMethods();

        if (methods.Length == 0)
        {
            return Result<bool>.Failure("No methods found in this type");
        }

        return Result<bool>.Success(true);
    }

    /// <summary>
    /// Validates whether the method is supported to invoke.
    /// A method is considered unsupported if it requires non primitive , non-decimal, non-string arguments.
    /// </summary>
    /// <param name="methodInfo">Method info of method to check.</param>
    /// <returns><see cref="Result{Assembly}"/> object indicating success if method can be invoked; otherwise failure with error message.</returns>
    private bool IsSupportedMethod(MethodInfo methodInfo)
    {
        ParameterInfo[] propertyInfos = methodInfo.GetParameters();
        return propertyInfos.All(p =>
            p.ParameterType.IsPrimitive ||
            p.ParameterType == typeof(decimal) ||
            p.ParameterType == typeof(string));
    }
}
