using System.Reflection;
using Reflections.Enums;

namespace Reflections.UserInterface;

/// <summary>
/// Defines a contract for user interface.
/// </summary>
public interface IUserInterface
{
    /// <summary>
    /// Gets user input and returns it.
    /// </summary>
    /// <returns>User's input</returns>
    public string? GetInput();

    /// <summary>
    /// Shows the message to user as a specified type.
    /// </summary>
    /// <param name="type">Type of the message to show.</param>
    /// <param name="message">Message shown to user.</param>
    public void ShowMessage(MessageType type, string message);

    /// <summary>
    /// Display all the types to user in table format.
    /// </summary>
    /// <param name="types">Types to display.</param>
    public void DisplayTypes(Type[] types);

    /// <summary>
    /// Display all the properties of type to user.
    /// </summary>
    /// <param name="type">Types of the properties to display.</param>
    /// <param name="properties">Properties to display.</param>
    public void DisplayTypeProperties(Type type, PropertyInfo[] properties);

    /// <summary>
    /// Display all the fields of the type to user.
    /// </summary>
    /// <param name="type">Types of the fields to display.</param>
    /// <param name="fields">Fields to display.</param>
    public void DisplayTypeFields(Type type, FieldInfo[] fields);

    /// <summary>
    /// Display all the methods to user.
    /// </summary>
    /// <param name="methods">Methods to display.</param>
    public void DisplayTypeMethods(MethodInfo[] methods);

    /// <summary>
    /// Display all the events to user.
    /// </summary>
    /// <param name="events">Events to display.</param>
    public void DisplayTypeEvents(EventInfo[] events);
}
