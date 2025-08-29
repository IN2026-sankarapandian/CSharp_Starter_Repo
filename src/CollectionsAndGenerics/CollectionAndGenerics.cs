using CollectionsAndGenerics.Managers;
using CollectionsAndGenerics.UserInterface;
using CollectionsAndGenerics.UserInterfaces;

namespace CollectionsAndGenerics;

/// <summary>
/// Demonstrates generics and collections.
/// </summary>
public class CollectionAndGenerics
{
    /// <summary>
    /// Its an entry point of the <see cref="CollectionAndGenerics"/>.
    /// </summary>
    public static void Main()
    {
        IUserInterface consoleUI = new ConsoleUI();
        TaskManager taskManager = new (consoleUI);
        taskManager.ShowMenu();
    }
}
