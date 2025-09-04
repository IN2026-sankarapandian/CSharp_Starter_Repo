//using System.Reflection;
//using Reflections.Enums;
//using Reflections.Handlers;
//using Reflections.UserInterface;

//namespace Reflections.Tasks.Task1;

///// <summary>
///// Its an dynamic object inspector used to extract information of assemblies
///// </summary>
//public class Task1 : ITask
//{
//    private readonly IUserInterface _userInterface;
//    private readonly FormHandlers _formHandlers;
//    private readonly AssemblyHelper _assemblyHelper;

//    /// <summary>
//    /// Initializes a new instance of the <see cref="Task1"/> class.
//    /// </summary>
//    /// <param name="userInterface"> Provides operations to interact with user.</param>
//    public Task1(IUserInterface userInterface, FormHandlers formHandlers, AssemblyHelper assemblyHelper)
//    {
//        this._userInterface = userInterface;
//        this._formHandlers = formHandlers;
//        this._assemblyHelper = assemblyHelper;
//    }

//    /// <inheritdoc/>
//    public string Name => "Inspect Assembly Metadata";

//    /// <inheritdoc/>
//    public void Run()
//    {
//        do
//        {
//            this._userInterface.ShowMessage(MessageType.Title, this.Name);
//            string path = this._formHandlers.GetPath();
//            Result<Assembly>? assembly = this._assemblyHelper.LoadAssemblyFile(path);

//            if (assembly.IsSuccess)
//            {
//                this.HandleMenu(assembly.Value);
//                return;
//            }
//            else
//            {
//                this._userInterface.ShowMessage(MessageType.Warning, assembly.ErrorMessage);
//                Thread.Sleep(1000);
//            }
//        }
//        while (true);
//    }

//    /// <summary>
//    /// Handles menu of this task.
//    /// </summary>
//    /// <param name="assembly">Assembly to load t</param>
//    public void HandleMenu(Assembly assembly)
//    {
//        while (true)
//        {
//            this._userInterface.ShowMessage(MessageType.Title, string.Format("{0}", this.Name));
//            this._userInterface.ShowMessage(MessageType.Information, "1. Inspect a type 2. Exit app");
//            string? userChoice = this._formHandlers.GetUserInput("Enter what do you want to do : ");
//            switch (userChoice)
//            {
//                case "1":
//                    this._userInterface.ShowMessage(MessageType.Title, string.Format("{0} > Select type\n", this.Name));
//                    Type[] types = assembly.GetTypes();
//                    Type targetType = this._formHandlers.GetTargetType(types, "\nEnter which type to inspect : ");
//                    this.HandleShowTypeDetails(targetType);
//                    break;
//                case "2":
//                    return;
//                default:
//                    this._userInterface.ShowMessage(MessageType.Warning, "Enter a valid option !");
//                    Thread.Sleep(1000);
//                    break;
//            }
//        }
//    }

//    /// <summary>
//    /// Handle displaying all kind of members of the specified type.
//    /// </summary>
//    /// <param name="type">Type data of which members to display.</param>
//    private void HandleShowTypeDetails(Type type)
//    {
//        do
//        {
//            this._userInterface.ShowMessage(MessageType.Title, string.Format("{0} > Select type > Select member kind", this.Name));
//            this._userInterface.ShowMessage(MessageType.Prompt, string.Format("\nType name : {0}", type.Name));
//            this._userInterface.ShowMessage(MessageType.Prompt, "\n1. Get properties\n2. Get fields\n3. Get methods\n4. Get events\n5. Go back\n");
//            string? userChoice = this._formHandlers.GetUserInput("Enter which kind of members you want to inspect : ");

//            switch (userChoice)
//            {
//                case "1":
//                    this._userInterface.ShowMessage(MessageType.Title, string.Format("{0} > Select type > Select member kind > Properties\n", this.Name));
//                    this._userInterface.DisplayTypeProperties(type, type.GetProperties());
//                    break;
//                case "2":
//                    this._userInterface.ShowMessage(MessageType.Title, string.Format("{0} > Select type > Select member kind > Fields\n", this.Name));
//                    this._userInterface.DisplayTypeFields(type, type.GetFields());
//                    break;
//                case "3":
//                    this._userInterface.ShowMessage(MessageType.Title, string.Format("{0} > Select type > Select member kind > Methods\n", this.Name));
//                    this._userInterface.DisplayTypeMethods(type.GetMethods());
//                    break;
//                case "4":
//                    this._userInterface.ShowMessage(MessageType.Title, string.Format("{0} > Select type > Select member kind > Events\n", this.Name));
//                    this._userInterface.DisplayTypeEvents(type.GetEvents());
//                    break;
//                case "5":
//                    return;
//                default:
//                    this._userInterface.ShowMessage(MessageType.Warning, "Not a valid input !");
//                    Thread.Sleep(1000);
//                    continue;
//            }

//            this._userInterface.ShowMessage(MessageType.Prompt, "\nPress any key to exit");
//            this._userInterface.GetInput();
//        }
//        while (true);
//    }
//}
