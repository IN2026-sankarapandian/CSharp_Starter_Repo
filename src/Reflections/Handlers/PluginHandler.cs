using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Reflections.Common;
using Reflections.Enums;
using Reflections.Helpers;
using Reflections.Tasks;
using Reflections.UserInterface;

namespace Reflections.Handlers;

/// <summary>
/// Handles all the plugins loading.
/// </summary>
public class PluginHandler
{
    private readonly IUserInterface _userInterface;
    private readonly AssemblyHelper _assemblyHelper;

    /// <summary>
    /// Initializes a new instance of the <see cref="PluginHandler"/> class.
    /// </summary>
    /// <param name="userInterface"> Provides operations to interact with user.</param>
    /// <param name="assemblyHelper">Provide helper methods to manipulate and work with assembly and it related types.</param>
    public PluginHandler(IUserInterface userInterface, AssemblyHelper assemblyHelper)
    {
        this._userInterface = userInterface;
        this._assemblyHelper = assemblyHelper;
    }

    /// <summary>
    /// Loads all the plugins to the specified services.
    /// </summary>
    /// <param name="services">Service to add plugins.</param>
    public void LoadPlugins(ServiceCollection services)
    {
        using (ServiceProvider tempServiceProvider = services.BuildServiceProvider())
        {
            string rootPath = Path.GetFullPath("../../../TaskPlugins/");
            foreach (string dllPath in Directory.GetFiles(rootPath, "*.dll"))
            {
                Result<Assembly> assemblyResult = this._assemblyHelper.LoadAssemblyFile(dllPath);
                if (assemblyResult.IsSuccess)
                {
                    Type? taskImplementation = assemblyResult.Value.GetTypes().Where(type => typeof(ITask).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract).FirstOrDefault();
                    if (taskImplementation is not null)
                    {
                        services.AddTransient(typeof(ITask), taskImplementation);
                    }
                }
                else
                {
                    this._userInterface.ShowMessage(MessageType.Warning, assemblyResult.ErrorMessage);
                }
            }
        }
    }
}
