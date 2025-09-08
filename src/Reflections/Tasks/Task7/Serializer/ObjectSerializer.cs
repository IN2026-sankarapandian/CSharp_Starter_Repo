using System.Reflection;
using System.Reflection.Emit;
using System.Text;

/// <summary>
/// Serialize the object with reflections and reflection emit.
/// </summary>
public class ObjectSerializer
{
    private Func<object, string>? _serializer;

    /// <summary>
    /// Serialize the object properties and fields as string using reflection.
    /// </summary>
    /// <param name="obj">Type instance</param>
    /// <returns>Serialized string</returns>
    public string SerializeUsingReflection(object obj)
    {
        Type type = obj.GetType();
        FieldInfo[] fields = type.GetFields();
        PropertyInfo[] props = type.GetProperties();
        StringBuilder stringBuilder = new StringBuilder("{\n");
        foreach (FieldInfo field in fields)
        {
            stringBuilder.AppendLine($"{field.Name} : {field.GetValue(obj)?.ToString()},");
        }

        foreach (PropertyInfo property in props)
        {
            stringBuilder.AppendLine($"{property.Name} : {property.GetValue(obj)?.ToString()}");
        }

        stringBuilder.AppendLine("}");
        return stringBuilder.ToString();
    }

    /// <summary>
    /// Serialize the object properties and fields as string using reflections and opcodes.
    /// </summary>
    /// <param name="obj">Class instance</param>
    /// <returns>Serialized string</returns>
    public string SerializeUsingOpcode(object obj)
    {
        Type type = obj.GetType();
        FieldInfo[] fields = type.GetFields();
        PropertyInfo[] props = type.GetProperties();
        DynamicMethod dynamicMethod = new DynamicMethod(
            "ClassSerializer",
            typeof(string),
            new Type[] { typeof(object) },
            typeof(ObjectSerializer).Module);

        Type? stringBuilderType = typeof(StringBuilder);
        MethodInfo? appendInfo = stringBuilderType.GetMethod("Append", new Type[] { typeof(string) });
        MethodInfo? toString = stringBuilderType.GetMethod("ToString", Type.EmptyTypes);
        ILGenerator iLGenerator = dynamicMethod.GetILGenerator();

        string result = string.Empty;

        if (stringBuilderType is not null && appendInfo is not null && toString is not null)
        {
            LocalBuilder localBuilder = iLGenerator.DeclareLocal(stringBuilderType);
            iLGenerator.Emit(OpCodes.Newobj, stringBuilderType.GetConstructor(Type.EmptyTypes));
            iLGenerator.Emit(OpCodes.Stloc, localBuilder);
            iLGenerator.Emit(OpCodes.Ldloc, localBuilder);
            iLGenerator.Emit(OpCodes.Ldstr, "{\n");
            iLGenerator.Emit(OpCodes.Callvirt, appendInfo);
            iLGenerator.Emit(OpCodes.Pop);

            foreach (PropertyInfo property in props)
            {
                string propName = $"{property.Name} : ";
                string propValue = $"{property.GetValue(obj)?.ToString()},\n";

                iLGenerator.Emit(OpCodes.Ldloc, localBuilder);
                iLGenerator.Emit(OpCodes.Ldstr, propName);
                iLGenerator.Emit(OpCodes.Callvirt, appendInfo);
                iLGenerator.Emit(OpCodes.Pop);

                iLGenerator.Emit(OpCodes.Ldloc, localBuilder);
                iLGenerator.Emit(OpCodes.Ldstr, propValue);
                iLGenerator.Emit(OpCodes.Callvirt, appendInfo);
                iLGenerator.Emit(OpCodes.Pop);
            }

            foreach (FieldInfo field in fields)
            {
                string fieldName = $"{field.Name} : ";
                string fieldValue = $"{field.GetValue(obj)},\n";

                iLGenerator.Emit(OpCodes.Ldloc, localBuilder);
                iLGenerator.Emit(OpCodes.Ldstr, fieldName);
                iLGenerator.Emit(OpCodes.Callvirt, appendInfo);
                iLGenerator.Emit(OpCodes.Pop);

                iLGenerator.Emit(OpCodes.Ldloc, localBuilder);
                iLGenerator.Emit(OpCodes.Ldstr, fieldValue);
                iLGenerator.Emit(OpCodes.Callvirt, appendInfo);
            }

            iLGenerator.Emit(OpCodes.Ldloc, localBuilder);
            iLGenerator.Emit(OpCodes.Ldstr, "}");
            iLGenerator.Emit(OpCodes.Callvirt, appendInfo);
            iLGenerator.Emit(OpCodes.Pop);

            iLGenerator.Emit(OpCodes.Ldloc, localBuilder);
            iLGenerator.Emit(OpCodes.Callvirt, toString);
            iLGenerator.Emit(OpCodes.Ret);

            this._serializer = dynamicMethod.CreateDelegate(typeof(Func<object, string>)) as Func<object, string>;

            if (this._serializer != null)
            {
                result = this._serializer(obj);
            }
        }

        return result;
    }
}