using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Reflections.Enums;
using Reflections.Handlers;
using Reflections.TaskManagers;
using Reflections.Tasks;
using Reflections.Tasks.Task1;
using Reflections.Tasks.Task2;
using Reflections.Tasks.Task4;
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
        services.AddSingleton<FormHandlers>();
        services.AddSingleton<AssemblyHelper>();
        services.AddTransient<TaskManager>();

        ServiceProvider serviceProvider = services.BuildServiceProvider();
        IUserInterface? userInterface = serviceProvider.GetService<IUserInterface>();

        string rootPath = Path.GetFullPath("../../../TaskPlugins/");
        List<Assembly> assemblies = new List<Assembly>();
        foreach (string dllPath in Directory.GetFiles(rootPath, "*.dll"))
        {
            try
            {
                Assembly assembly = Assembly.LoadFile(dllPath);
                assemblies.Add(assembly);

                IUserInterface co = new ConsoleUI();
                Type? taskImplementation = assembly.GetTypes().Where(type => type.IsClass && type.Name == "Task").FirstOrDefault();
                if (taskImplementation is not null)
                {
                    //services.AddTransient(taskInterface, taskImplementation);
                }

                userInterface.ShowMessage(MessageType.Prompt, assembly.FullName + " Loaded Successfully");
            }
            catch (FileNotFoundException)
            {
                userInterface.ShowMessage(MessageType.Prompt, "No file exists in the specified path !");
            }
            catch (BadImageFormatException)
            {
                userInterface.ShowMessage(MessageType.Prompt, "The file is not a valid .NET assembly !");
            }
            catch (FileLoadException ex)
            {
                userInterface.ShowMessage(MessageType.Prompt, string.Format("The file can't be load {0}", ex.Message));
            }
        }

        //Thread.Sleep(2000);

        services.AddTransient<ITask, Task1>();
        services.AddTransient<ITask, Task2>();
        services.AddTransient<ITask, Task3>();
        services.AddTransient<ITask, Task4>();
        services.AddTransient<ITask, Task6>();

        serviceProvider = services.BuildServiceProvider();

        TaskManager? taskManager = serviceProvider.GetService<TaskManager>();
        taskManager?.HandleMenu();
        Console.ReadKey();
    }
}
