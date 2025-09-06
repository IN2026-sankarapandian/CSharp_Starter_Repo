using System.Reflection;
using System.Reflection.Emit;
using Reflections.Enums;
using Reflections.Handlers;
using Reflections.Tasks;
using Reflections.UserInterface;

namespace DynamicTypeBuilder.Tasks;

/// <summary>
/// Its an dynamic type builder used to create assemblies
/// </summary>
public class DynamicTypeBuilderTask : ITask
{
    private readonly IUserInterface _userInterface;
    private readonly FormHandler _formHandlers;

    /// <summary>
    /// Initializes a new instance of the <see cref="DynamicTypeBuilderTask"/> class.
    /// </summary>
    /// <param name="userInterface"> Provides operations to interact with user.</param>
    /// <param name="formHandlers"> Gets required data from user.</param>
    public DynamicTypeBuilderTask(IUserInterface userInterface, FormHandler formHandlers)
    {
        this._userInterface = userInterface;
        this._formHandlers = formHandlers;
        AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("SampleAssembly"), AssemblyBuilderAccess.Run);
    }

    /// <inheritdoc/>
    public string Name => "Dynamic type builder";

    /// <inheritdoc/>
    public void Run()
    {
        this._userInterface.ShowMessage(MessageType.Title, this.Name);

        string className = this._formHandlers.GetIdentifierName("Enter class name : ");
        string propertyName = this._formHandlers.GetIdentifierName("\nEnter property (string) name : ");
        string propertyValue = this._formHandlers.GetIdentifierName("Enter a value for the property : ");
        string methodName = this._formHandlers.GetIdentifierName("\nEnter method name : ");
        this._userInterface.ShowMessage(MessageType.Information, "This method will print the property value");

        // Creates assembly, module and defines the type with user specified name
        AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("DynAsm"), AssemblyBuilderAccess.Run);
        ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
        TypeBuilder typeBuilder = moduleBuilder.DefineType(className, TypeAttributes.Public);

        // Creates property with user specified name, getter and setters
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

        // Creates the method with user specified name which prints the property value.
        MethodBuilder method = typeBuilder.DefineMethod(methodName, MethodAttributes.Public, typeof(void), Type.EmptyTypes);
        ILGenerator ilMethod = method.GetILGenerator();
        ilMethod.Emit(OpCodes.Ldarg_0);
        ilMethod.EmitCall(OpCodes.Call, getter, null);
        ilMethod.EmitCall(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new[] { typeof(string) })!, null);
        ilMethod.Emit(OpCodes.Ret);

        // Creates the type
        Type dynamicType = typeBuilder.CreateType()!;
        object? obj = Activator.CreateInstance(dynamicType)!;

        // Sets the property with user specified value
        dynamicType.GetProperty(propertyName)!.SetValue(obj, propertyValue);
        this._userInterface.ShowMessage(MessageType.Information, string.Format("Property set with the value  : {0}", dynamicType.GetProperty(propertyName)!.GetValue(obj)?.ToString()));

        // Calls the created method
        this._userInterface.ShowMessage(MessageType.Prompt, string.Format("Calling method {0} : ", methodName));
        dynamicType.GetMethod(methodName)!.Invoke(obj, null);

        this._userInterface.ShowMessage(MessageType.Prompt, "\nPress any key to exit");
        this._userInterface.GetInput();
    }
}
