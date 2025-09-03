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
public class Task4 : ITask
{
    private readonly IUserInterface _userInterface;
    private readonly FormHandlers _formHandlers;

    /// <summary>
    /// Initializes a new instance of the <see cref="Task4"/> class.
    /// </summary>
    /// <param name="userInterface"> Provides operations to interact with user.</param>
    public Task4(IUserInterface userInterface, FormHandlers formHandlers)
    {
        this._userInterface = userInterface;
        this._formHandlers = formHandlers;
    }

    /// <inheritdoc/>
    public string Name => "Dynamic type builder";

    /// <inheritdoc/>
    public void Run()
    {
        this._userInterface.ShowMessage(MessageType.Title, this.Name);
        string className = this._formHandlers.GetUserInput("Enter class name : ");
        string propertyName = this._formHandlers.GetUserInput("\nEnter property (string) name : ");
        string propertyValue = this._formHandlers.GetUserInput("Enter a value for the property : ");
        string methodName = this._formHandlers.GetUserInput("\nEnter method name : ");
        this._userInterface.ShowMessage(MessageType.Information, "This method will print the property value");
        AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("DynAsm"), AssemblyBuilderAccess.Run);
        ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
        TypeBuilder typeBuilder = moduleBuilder.DefineType(className, TypeAttributes.Public);

        FieldBuilder field = typeBuilder.DefineField("_" + propertyName, typeof(string), FieldAttributes.Private);
        PropertyBuilder prop = typeBuilder.DefineProperty(propertyName, PropertyAttributes.HasDefault, typeof(string), null);

        MethodBuilder getter = typeBuilder.DefineMethod("get_" + propertyName, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, typeof(string), Type.EmptyTypes);
        ILGenerator ilGetter = getter.GetILGenerator();
        ilGetter.Emit(OpCodes.Ldarg_0);
        ilGetter.Emit(OpCodes.Ldfld, field);
        ilGetter.Emit(OpCodes.Ret);
        prop.SetGetMethod(getter);

        MethodBuilder setter = typeBuilder.DefineMethod(
            "set_" + propertyName,
            MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig,
            null,
            new[] { typeof(string) });

        ILGenerator ilSetter = setter.GetILGenerator();
        ilSetter.Emit(OpCodes.Ldarg_0);
        ilSetter.Emit(OpCodes.Ldarg_1);
        ilSetter.Emit(OpCodes.Stfld, field);
        ilSetter.Emit(OpCodes.Ret);
        prop.SetSetMethod(setter);

        MethodBuilder method = typeBuilder.DefineMethod(methodName, MethodAttributes.Public, typeof(void), Type.EmptyTypes);
        ILGenerator ilMethod = method.GetILGenerator();
        ilMethod.Emit(OpCodes.Ldarg_0);
        ilMethod.EmitCall(OpCodes.Call, getter, null);
        ilMethod.EmitCall(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new[] { typeof(string) })!, null);
        ilMethod.Emit(OpCodes.Ret);

        Type dynamicType = typeBuilder.CreateType()!;
        object? obj = Activator.CreateInstance(dynamicType)!;

        dynamicType.GetProperty(propertyName)!.SetValue(obj, propertyValue);
        this._userInterface.ShowMessage(MessageType.Information, string.Format("Property set with the value  : {0}", dynamicType.GetProperty(propertyName)!.GetValue(obj)?.ToString()));
        this._userInterface.ShowMessage(MessageType.Prompt, string.Format("Calling method {0} : ", methodName));
        dynamicType.GetMethod(methodName)!.Invoke(obj, null);

        this._userInterface.ShowMessage(MessageType.Prompt, "\nPress any key to exit");
        this._userInterface.GetInput();
    }
}
