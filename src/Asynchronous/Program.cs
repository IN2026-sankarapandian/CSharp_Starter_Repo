using Asynchronous.FileServices;
using Asynchronous.FormHandlers;
using Asynchronous.TaskManagers;
using Asynchronous.Tasks;
using Asynchronous.Tasks.Task1;
using Asynchronous.Tasks.Task2;
using Asynchronous.Tasks.Task3;
using Asynchronous.Tasks.Task4;
using Asynchronous.Tasks.Task6;
using Asynchronous.Tasks.Task7;
using Asynchronous.UserInterface;
using Asynchronous.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Asynchronous;

/// <summary>
/// Its an console app demonstrating tasks for asynchronous assignment.
/// </summary>
public class Program
{
    /// <summary>
    /// Its an entry point of asynchronous assignment.
    /// </summary>
    public static void Main()
    {
        ServiceCollection services = new ServiceCollection();

        // Adding services
        services.AddSingleton<IUserInterface, ConsoleUI>();
        services.AddTransient<TaskManager>();
        services.AddTransient<FormHandler>();
        services.AddTransient<Validator>();
        services.AddTransient<HttpContentFetcher>();
        services.AddTransient<FileService>();
        services.AddTransient<IntegerGenerator>();
        services.AddTransient<ExceptionThrower>();

        // Adding tasks
        services.AddTransient<ITask, Task1>();
        services.AddTransient<ITask, Task2>();
        services.AddTransient<ITask, Task3>();
        services.AddTransient<ITask, Task4>();
        services.AddTransient<ITask, Task6>();
        services.AddTransient<ITask, Task7>();

        ServiceProvider serviceProvider = services.BuildServiceProvider();

        TaskManager? taskManager = serviceProvider.GetService<TaskManager>();
        taskManager?.HandleMenu();
    }
}