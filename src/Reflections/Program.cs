using Microsoft.Extensions.DependencyInjection;
using Reflections.Handlers;
using Reflections.Helpers;
using Reflections.TaskManagers;
using Reflections.Tasks;
using Reflections.Tasks.Task6;
using Reflections.Tasks.Task7;
using Reflections.UserInterface;
using Reflections.Utilities;
using Reflections.Validators;

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

        services.AddSingleton<IUserInterface, ConsoleUI>();
        services.AddSingleton<AssemblyHelper>();
        services.AddTransient<PluginHandler>();

        // Adding plugin tasks
        using (var tempServiceProvider = services.BuildServiceProvider())
        {
            PluginHandler? pluginHandler = tempServiceProvider.GetService<PluginHandler>();
            pluginHandler?.LoadPlugins(services);
        }

        services.AddSingleton<FormHandler>();
        services.AddSingleton<Utility>();
        services.AddSingleton<Validator>();
        services.AddTransient<TaskManager>();

        // Adding built in tasks
        services.AddTransient<ITask, MockingFrameworkTask>();
        services.AddTransient<ITask, SerializationAPI>();

        ServiceProvider serviceProvider = services.BuildServiceProvider();
        TaskManager? taskManager = serviceProvider.GetRequiredService<TaskManager>();
        taskManager?.HandleMenu();
        Console.ReadKey();
    }
}
