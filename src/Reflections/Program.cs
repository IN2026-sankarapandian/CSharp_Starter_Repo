using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Reflections.TaskManagers;
using Reflections.Tasks;
using Reflections.Tasks.Task1;
using Reflections.Tasks.Task2;
using Reflections.UserInterface;

namespace Reflections;

/// <summary>
/// Demonstrates the working of advanced language features of c sharp
/// </summary>
public class Program
{
    /// <summary>
    /// Its an entry point for C Sharp advanced features demonstration.
    /// </summary>
    public static void Main()
    {
        ServiceCollection services = new ();
        Type taskInterface = typeof(ITask);

        services.AddSingleton<IUserInterface, ConsoleUI>();
        services.AddTransient<TaskManager>();

        string rootPath = Path.GetFullPath("../../../TaskPlugins/");
        List<Assembly> assemblies = new List<Assembly> ();
        foreach (string dllPath in Directory.GetFiles(rootPath, "*.dll"))
        {
            try
            {
                Assembly assembly = Assembly.LoadFile(dllPath);
                assemblies.Add(assembly);

                IUserInterface co = new ConsoleUI();
                Task1 to = new Task1(co);
                Type? taskImplementation = assembly.GetTypes().Where(type => type.IsClass && type.Name == "Task").FirstOrDefault();
                if (taskImplementation is not null)
                {
                    services.AddTransient(taskInterface, taskImplementation);
                }

                Console.WriteLine(assembly.FullName + " Loaded Successfully");
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
        }

        Thread.Sleep(2000);

        services.AddTransient<ITask, Task2>();

        ServiceProvider serviceProvider = services.BuildServiceProvider();

        TaskManager? taskManager = serviceProvider.GetService<TaskManager>();
        taskManager?.HandleMenu();
        Console.ReadKey();
    }
}
