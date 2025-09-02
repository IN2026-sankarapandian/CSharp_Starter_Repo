using System.Reflection;
using ConsoleTables;
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
            ConsoleTable typesTable = new ConsoleTable("Index", "Name", "Namespace");
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
            Console.WriteLine("No types exist");
        }
    }

    /// <inheritdoc/>
    public void DisplayTypeProperties(Type type, PropertyInfo[] properties)
    {
        if (properties.Length != 0)
        {
            ConsoleTable typesTable = new ConsoleTable("Index", "Name", "Value");
            for (int index = 0; index < properties.Length; index++)
            {
                object? typeInstance = Activator.CreateInstance(type);
                typesTable.AddRow(
                    index + 1,
                    properties[index].Name,
                    properties[index]?.GetValue(typeInstance) ?? "null");
            }

            typesTable.Write();
        }
        else
        {
            Console.WriteLine("No properties exists");
        }
    }

    /// <inheritdoc/>
    public void DisplayTypeFields(Type type, FieldInfo[] fields)
    {
        if (fields.Length != 0)
        {
            ConsoleTable typesTable = new ConsoleTable("Index", "Name", "Value");
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
            Console.WriteLine("No fields exists");
        }
    }

    /// <inheritdoc/>
    public void DisplayTypeMethods(MethodInfo[] methods)
    {
        if (methods.Length != 0)
        {
            ConsoleTable typesTable = new ConsoleTable("Index", "Name", "Return type", "Parameters");
            for (int index = 0; index < methods.Length; index++)
            {
                ParameterInfo[] parametersInfo = methods[index].GetParameters();
                string?[] parameters = parametersInfo?.Select(parameterInfo => parameterInfo?.ToString())?.ToArray() ?? Array.Empty<string>();

                typesTable.AddRow(
                    index + 1,
                    methods[index].Name,
                    methods[index].ReturnType,
                    string.Join(", ", parameters) ?? "empty");
            }

            typesTable.Write();
        }
        else
        {
            Console.WriteLine("No methods exists");
        }
    }

    /// <inheritdoc/>
    public void DisplayTypeEvents(EventInfo[] events)
    {
        if (events.Length != 0)
        {
            ConsoleTable typesTable = new ConsoleTable("Index", "Name");
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
            Console.WriteLine("No events exists");
        }
    }
}
