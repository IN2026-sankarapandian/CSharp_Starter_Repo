using System.Reflection;
using Reflections.Enums;
using Reflections.Tasks;
using Reflections.UserInterface;

namespace AssemblyMetadataInspector;

/// <summary>
/// Its an dynamic object inspector used to extract information of assemblies
/// </summary>
public class Task : ITask
{
    private readonly IUserInterface _userInterface;

    /// <summary>
    /// Initializes a new instance of the <see cref="Task"/> class.
    /// </summary>
    /// <param name="userInterface"> Provides operations to interact with user.</param>
    public Task(IUserInterface userInterface)
    {
        _userInterface = userInterface;
    }

    /// <inheritdoc/>
    public string Name => "Inspect Assembly Metadata";

    /// <inheritdoc/>
    public void Run()
    {
        do
        {
            _userInterface.ShowMessage(MessageType.Title, Name);
            string path = GetPath();
            Assembly? assembly = HandleLoadAssemblyFromPath(path);

            if (assembly is not null)
            {
                HandleMenu(assembly);
                return;
            }
        }
        while (true);
    }

    /// <summary>
    /// Handles menu of this task.
    /// </summary>
    /// <param name="assembly">Assembly to load t</param>
    private void HandleMenu(Assembly assembly)
    {
        Type[] types = assembly.GetTypes();
        do
        {
            _userInterface.ShowMessage(MessageType.Title, string.Format("{0} > Select type\n", Name));
            HandleShowTypes(types);
            Console.WriteLine("1. Inspect a type 2. Exit app");
            Console.Write("Enter what do you want to do : ");
            string? userChoice = Console.ReadLine();
            switch (userChoice)
            {
                case "1":
                    int typeIndex = GetTypeIndex(types);
                    HandleShowTypeMembers(types[typeIndex]);
                    break;
                case "2":
                    return;
                default:
                    Console.WriteLine("Enter a valid option !");
                    break;
            }
        }
        while (true);
    }

    /// <summary>
    /// Handles loading of an assembly file in the specified path
    /// </summary>
    /// <param name="path">Path of the assembly.</param>
    /// <returns>Assembly loaded from the path.</returns>
    private Assembly? HandleLoadAssemblyFromPath(string path)
    {
        try
        {
            Assembly assembly = Assembly.LoadFile(path);
            return assembly;
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("No file exists in the specified path !");
        }
        catch (BadImageFormatException)
        {
            Console.WriteLine("The file is not a valid .NET assembly !");
        }
        catch (FileLoadException ex)
        {
            Console.WriteLine("The file can't be load {0}", ex.Message);
        }

        return null;
    }

    /// <summary>
    /// Handle displaying types data to the user.
    /// </summary>
    /// <param name="types">Types data to display.</param>
    private void HandleShowTypes(Type[] types)
    {
        _userInterface.DisplayTypes(types);
    }

    /// <summary>
    /// Handle displaying all kind of members of the specified type.
    /// </summary>
    /// <param name="type">Type data of which members to display.</param>
    private void HandleShowTypeMembers(Type type)
    {
        do
        {
            _userInterface.ShowMessage(MessageType.Title, string.Format("{0} > Select type > Select member kind\n", Name));
            Console.WriteLine(string.Format("Type name : {0}", type.Name));
            Console.WriteLine("1. Get properties\n2. Get fields\n3. Get methods\n4. Get events\n5. Go back");
            Console.Write("Enter which kind of members you want to inspect : ");
            string? userChoice = Console.ReadLine();

            _userInterface.ShowMessage(MessageType.Title, string.Format("{0} > Select type > Select member kind > Result\n", Name));
            switch (userChoice)
            {
                case "1":
                    _userInterface.DisplayTypeProperties(type, type.GetProperties());
                    break;
                case "2":
                    _userInterface.DisplayTypeFields(type, type.GetFields());
                    break;
                case "3":
                    _userInterface.DisplayTypeMethods(type.GetMethods());
                    break;
                case "4":
                    _userInterface.DisplayTypeEvents(type.GetEvents());
                    break;
                case "5":
                    return;
                default:
                    continue;
            }

            _userInterface.ShowMessage(MessageType.Prompt, "\nPress any key to exit");
            _userInterface.GetInput();
        }
        while (true);
    }

    /// <summary>
    /// Gets type index from the user.
    /// </summary>
    /// <param name="types">Type array which index value to get from user</param>
    /// <returns>Index given by user.</returns>
    private int GetTypeIndex(Type[] types)
    {
        do
        {
            Console.Write("\nEnter which type to inspect : ");
            string? userTypeInput = Console.ReadLine();
            if (int.TryParse(userTypeInput, out int typeIndex) && typeIndex <= types.Length && typeIndex > 0)
            {
                return typeIndex - 1;
            }

            Console.WriteLine("Enter a valid index !");
        }
        while (true);
    }

    /// <summary>
    /// Gets a valid and existing path from user.
    /// </summary>
    /// <returns>Path defined by user.</returns>
    private string GetPath()
    {
        do
        {
            Console.Write("Enter a path of the file to inspect : ");
            string? path = Console.ReadLine();

            if (string.IsNullOrEmpty(path))
            {
                Console.WriteLine("Input cannot be empty !");
            }
            else if (!File.Exists(path))
            {
                Console.WriteLine("No file exists in the specified path !");
            }
            else
            {
                return path;
            }
        }
        while (true);
    }
}
