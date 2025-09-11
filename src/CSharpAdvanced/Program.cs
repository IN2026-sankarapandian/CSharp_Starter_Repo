using CSharpAdvanced.TaskManagers;
using CSharpAdvanced.Tasks;
using CSharpAdvanced.Tasks.AnonymousMethod;
using CSharpAdvanced.Tasks.DelegatesAdvanced;
using CSharpAdvanced.Tasks.EventsAndDelegates;
using CSharpAdvanced.Tasks.LambdaExpressions;
using CSharpAdvanced.Tasks.PatternMatching;
using CSharpAdvanced.Tasks.Records;
using CSharpAdvanced.Tasks.VarAndDynamic;
using CSharpAdvanced.UserInterface;
using Microsoft.Extensions.DependencyInjection;

namespace CSharpAdvanced;

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
        services.AddTransient<TaskManager>();
        services.AddSingleton<Notifier>();
        services.AddTransient<ITask, Task1>();
        services.AddTransient<ITask, Task2>();
        services.AddTransient<ITask, Task3>();
        services.AddTransient<ITask, Task4>();
        services.AddTransient<ITask, Task5>();
        services.AddTransient<ITask, Task6>();
        services.AddTransient<ITask, Task7>();

        ServiceProvider serviceProvider = services.BuildServiceProvider();

        TaskManager? taskManager = serviceProvider.GetService<TaskManager>();
        taskManager?.HandleMenu();
    }
}
