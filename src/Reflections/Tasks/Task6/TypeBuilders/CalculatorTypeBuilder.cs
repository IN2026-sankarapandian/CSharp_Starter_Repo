using System.Reflection;
using System.Reflection.Emit;
using Reflections.Tasks.Task6.Calculator;

namespace Reflections.Tasks.Task6.TypeBuilders;

/// <summary>
/// Its a dynamic type builder for <see cref="ICalculator"/>.
/// </summary>
public class CalculatorTypeBuilder
{
    /// <summary>
    /// Builds a type implementing <see cref="ICalculator"/> and returns it.
    /// </summary>
    /// <returns>Type implementing <see cref="ICalculator"/></returns>
    public Type BuildCalculatorType()
    {
        Type interfaceType = typeof(ICalculator);

        AssemblyName assemblyName = new AssemblyName("CalculatorMock");
        AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
        ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");

        // Defines the type
        TypeBuilder typeBuilder = moduleBuilder.DefineType(
            "Calculator",
            TypeAttributes.Public | TypeAttributes.Class,
            null,
            new[] { interfaceType });

        // Implement add method
        MethodBuilder addMethod = typeBuilder.DefineMethod(
            "Add",
            MethodAttributes.Public | MethodAttributes.Virtual,
            typeof(int),
            new[] { typeof(int), typeof(int) });

        ILGenerator ilAdd = addMethod.GetILGenerator();
        ilAdd.Emit(OpCodes.Ldarg_1);
        ilAdd.Emit(OpCodes.Ldarg_2);
        ilAdd.Emit(OpCodes.Add);
        ilAdd.Emit(OpCodes.Ret);

        MethodInfo? addInterfaceMethod = interfaceType.GetMethod("Add");
        typeBuilder.DefineMethodOverride(addMethod, addInterfaceMethod!);

        // Implement subtract method
        MethodBuilder subMethod = typeBuilder.DefineMethod(
            "Subtract",
            MethodAttributes.Public | MethodAttributes.Virtual,
            typeof(int),
            new[] { typeof(int), typeof(int) });

        ILGenerator ilSub = subMethod.GetILGenerator();
        ilSub.Emit(OpCodes.Ldarg_1);
        ilSub.Emit(OpCodes.Ldarg_2);
        ilSub.Emit(OpCodes.Sub);
        ilSub.Emit(OpCodes.Ret);

        MethodInfo? subInterfaceMethod = interfaceType.GetMethod("Subtract");
        typeBuilder.DefineMethodOverride(subMethod, subInterfaceMethod!);

        return typeBuilder.CreateType() !;
    }
}
