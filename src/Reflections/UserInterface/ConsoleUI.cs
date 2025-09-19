using System.Reflection;
using ConsoleTables;
using Reflections.Constants;
using Reflections.Enums;

namespace Reflections.UserInterface;

/// <summary>
/// Provides operations to interact with user via console.
/// </summary>
public class ConsoleUI : IUserInterface
{
    /// <inheritdoc/>
    public string? GetInput() => Console.ReadLine();

    /// <inheritdoc/>
    public void ShowMessage(MessageType type, string message)
    {
        message = message.Replace("\\n", Environment.NewLine);
        switch (type)
        {
            case MessageType.Prompt:
                Console.Write(message);
                break;
            case MessageType.Warning:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(message);
                break;
            case MessageType.Highlight:
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(message);
                break;
            case MessageType.Title:
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(message);
                break;
            case MessageType.Information:
                Console.WriteLine(message);
                break;
        }

        Console.ResetColor();
    }

    /// <inheritdoc/>
    public void DisplayTypes(Type[] types)
    {
        if (types.Length != 0)
        {
            ConsoleTable typesTable = new ConsoleTable(KeyWords.Index, KeyWords.Name, KeyWords.Namespace);
            for (int index = 0; index < types.Length; index++)
            {
                typesTable.AddRow(
                    index + 1,
                    types[index].Name,
                    types[index].Namespace);
            }

            typesTable.Write();
        }
        else
        {
            Console.WriteLine(ErrorMessages.NoTypeExists);
        }
    }

    /// <inheritdoc/>
    public void DisplayTypeProperties(Type type, PropertyInfo[] properties)
    {
        if (properties.Length != 0)
        {
            ConsoleTable typesTable = new ConsoleTable(KeyWords.Index, KeyWords.Name, KeyWords.PropertyType, KeyWords.Value);

            object? typeInstance = null;
            if (!type.IsInterface && !type.IsAbstract && type.GetConstructor(Type.EmptyTypes) != null && !type.ContainsGenericParameters)
            {
                try
                {
                    typeInstance = Activator.CreateInstance(type);
                }
                catch
                {
                    Console.WriteLine(ErrorMessages.TypeInstantiationFailed);
                }
            }
            else
            {
                Console.WriteLine(ErrorMessages.CantGetValues);
            }

            for (int index = 0; index < properties.Length; index++)
            {
                object? value = "N/A";
                if (properties[index].GetMethod?.IsStatic == true)
                {
                    value = properties[index].GetValue(null) ?? KeyWords.Null;
                }
                else if (typeInstance != null)
                {
                    value = properties[index].GetValue(typeInstance) ?? KeyWords.Null;
                    }

                typesTable.AddRow(
                    index + 1,
                    properties[index].Name,
                    properties[index].PropertyType.Name,
                    value);
            }

            typesTable.Write();
        }
        else
        {
            Console.WriteLine(ErrorMessages.NoPropertyExists);
        }
    }

    /// <inheritdoc/>
    public void DisplayTypeFields(Type type, FieldInfo[] fields)
    {
        if (fields.Length != 0)
        {
            ConsoleTable typesTable = new ConsoleTable(KeyWords.Index, KeyWords.Name, KeyWords.Value);
            for (int index = 0; index < fields.Length; index++)
            {
                typesTable.AddRow(
                    index + 1,
                    fields[index].Name,
                    fields[index].GetValue(type));
            }

            typesTable.Write();
        }
        else
        {
            Console.WriteLine(ErrorMessages.NoFieldExists);
        }
    }

    /// <inheritdoc/>
    public void DisplayTypeMethods(MethodInfo[] methods)
    {
        if (methods.Length != 0)
        {
            ConsoleTable typesTable = new ConsoleTable(KeyWords.Index, KeyWords.Name, KeyWords.ReturnType, KeyWords.Parameters);
            for (int index = 0; index < methods.Length; index++)
            {
                ParameterInfo[] parametersInfo = methods[index].GetParameters();
                string?[] parameters = parametersInfo?.Select(parameterInfo => parameterInfo?.ToString())?.ToArray() ?? Array.Empty<string>();

                typesTable.AddRow(
                    index + 1,
                    methods[index].Name,
                    methods[index].ReturnType,
                    string.Join(", ", parameters) ?? KeyWords.Empty);
            }

            typesTable.Write();
        }
        else
        {
            Console.WriteLine(ErrorMessages.NoMethodExists);
        }
    }

    /// <inheritdoc/>
    public void DisplayTypeEvents(EventInfo[] events)
    {
        if (events.Length != 0)
        {
            ConsoleTable typesTable = new ConsoleTable(KeyWords.Index, KeyWords.Name);
            for (int index = 0; index < events.Length; index++)
            {
                typesTable.AddRow(
                    index + 1,
                    events[index].Name);
            }

            typesTable.Write();
        }
        else
        {
            Console.WriteLine(ErrorMessages.NoEventExists);
        }
    }
}
