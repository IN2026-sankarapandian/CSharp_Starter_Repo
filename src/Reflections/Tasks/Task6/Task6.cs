using Reflections.Enums;
using Reflections.Handlers;
using Reflections.UserInterface;
using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Reflections.Tasks.Task4;

/// <summary>
/// Its an dynamic type builder used to create assemblies
/// </summary>
public class Task6 : ITask
{
    private readonly IUserInterface _userInterface;
    private readonly FormHandlers _formHandlers;

    /// <summary>
    /// Initializes a new instance of the <see cref="Task6"/> class.
    /// </summary>
    /// <param name="userInterface"> Provides operations to interact with user.</param>
    public Task6(IUserInterface userInterface, FormHandlers formHandlers)
    {
        this._userInterface = userInterface;
        this._formHandlers = formHandlers;
    }

    /// <inheritdoc/>
    public string Name => "Mocking Framework";

    /// <inheritdoc/>
    public void Run()
    {
        this._userInterface.ShowMessage(MessageType.Title, this.Name);
        Type interfaceType = typeof(ITask);

        AssemblyName asmName = new AssemblyName("DynamicTaskMocks");
        AssemblyBuilder asmBuilder = AssemblyBuilder.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.Run);
        ModuleBuilder moduleBuilder = asmBuilder.DefineDynamicModule("MainModule");

        TypeBuilder typeBuilder = moduleBuilder.DefineType( "TaskMock", TypeAttributes.Public | TypeAttributes.Class,  null, new Type[] { interfaceType });

        PropertyBuilder nameProp = typeBuilder.DefineProperty(
            "Name",
            PropertyAttributes.None,
            typeof(string),
            null);

        MethodBuilder getName = typeBuilder.DefineMethod(
            "get_Name",
            MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.SpecialName | MethodAttributes.HideBySig,
            typeof(string),
            Type.EmptyTypes);

        ILGenerator il = getName.GetILGenerator();
        il.Emit(OpCodes.Ldstr, "MockTask");
        il.Emit(OpCodes.Ret);

        nameProp.SetGetMethod(getName);
        typeBuilder.DefineMethodOverride(getName, interfaceType.GetProperty("Name").GetGetMethod());

        MethodBuilder runMethod = typeBuilder.DefineMethod(
            "Run",
            MethodAttributes.Public | MethodAttributes.Virtual,
            typeof(void),
            Type.EmptyTypes);

        ILGenerator ilRun = runMethod.GetILGenerator();
        ilRun.Emit(OpCodes.Ret);
        typeBuilder.DefineMethodOverride(runMethod, interfaceType.GetMethod("Run"));

        Type mockType = typeBuilder.CreateType();
        ITask obj =  (ITask)Activator.CreateInstance(mockType)!;
        obj.Run();
        Console.WriteLine(obj.Name);
        Console.ReadKey();
    }
}
}
