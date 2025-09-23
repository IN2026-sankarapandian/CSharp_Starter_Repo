using System.Reflection;
using System.Reflection.Emit;
using Reflections.Enums;
using Reflections.Handlers;
using Reflections.Tasks;
using Reflections.UserInterface;

namespace MockingFramework.Tasks;

/// <summary>
/// Its an dynamic type builder used to create assemblies
/// </summary>
public class MockingFrameworkTask
{
    /// <summary>
    /// Creates and return a mock type.
    /// </summary>
    /// <returns>Mock type created.</returns>
    public Type? CreateMockType()
    {
        Type interfaceType = typeof(ITask);

        AssemblyName assemblyName = new AssemblyName("FrameworkMock");
        AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
        ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("Tasks");

        TypeBuilder typeBuilder = moduleBuilder.DefineType("TaskMock", TypeAttributes.Public | TypeAttributes.Class, null, new Type[] { interfaceType });

        PropertyBuilder nameProperty = typeBuilder.DefineProperty(
            "Name",
            PropertyAttributes.None,
            typeof(string),
            null);

        MethodBuilder getName = typeBuilder.DefineMethod(
            "get_Name",
            MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.SpecialName | MethodAttributes.HideBySig,
            typeof(string),
            Type.EmptyTypes);
        nameProperty.SetGetMethod(getName);

        ILGenerator il = getName.GetILGenerator();
        il.Emit(OpCodes.Ldstr, "Hello World !");
        il.Emit(OpCodes.Ret);

        PropertyInfo? namePropertyAtInterface = interfaceType.GetProperty("Name");
        MethodInfo? nameGetterAtInterface = namePropertyAtInterface?.GetGetMethod();
        if (nameGetterAtInterface is not null)
        {
            typeBuilder.DefineMethodOverride(getName, nameGetterAtInterface);
        }

        MethodBuilder runMethod = typeBuilder.DefineMethod(
            "Run",
            MethodAttributes.Public | MethodAttributes.Virtual,
            typeof(void),
            Type.EmptyTypes);

        ILGenerator ilRun = runMethod.GetILGenerator();
        ilRun.Emit(OpCodes.Ret);

        MethodInfo? runAtInterface = interfaceType.GetMethod("Run");
        if (runAtInterface is not null)
        {
            typeBuilder.DefineMethodOverride(runMethod, runAtInterface);
        }

        Type? mockType = typeBuilder.CreateType();
        return mockType;
    }
}
