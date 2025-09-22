using InventoryManager.Models;

namespace InventoryManager.UI;

/// <summary>
/// Has functions related to UI manipulations.
/// </summary>
public interface IUserInterface
{
    /// <summary>
    /// Prompt the user and returns the user input for the prompt.
    /// </summary>
    /// <param name="prompt">Prompt shown to the user.</param>
    /// <param name="color">Color for the prompt.</param>
    /// <returns>User input</returns>
    public string PromptAndGetInput(string prompt, ConsoleColor color = ConsoleColor.White);

    /// <summary>
    /// Prompt the user with info with specified color.
    /// </summary>
    /// <param name="info">Info to shown.</param>
    /// <param name="color">Color for the prompt.</param>
    public void Prompt(string info, ConsoleColor color = ConsoleColor.White);

    /// <summary>
    /// Prompt the user with info in new line with specified color.
    /// </summary>
    /// <param name="prompt">Prompt to show.</param>
    /// <param name="color">Color for the prompt. Default value is white.</param>
    public void PromptLine(string? prompt, ConsoleColor color = ConsoleColor.White);

    /// <summary>
    /// Prints list of <see cref="Product"/>s as a table in the console.
    /// </summary>
    /// <param name="currentProductList">Product list to print.</param>
    public void PrintAsTable(List<Product> currentProductList);

    /// <summary>
    /// Wait for the keypress and clear the console and navigate back to the menu.
    /// </summary>
    public void WaitAndNavigateToMenu();

    /// <summary>
    /// Clears the console and create new page for the actions.
    /// </summary>
    /// <param name="action">Current action name.</param>
    public void CreateNewPageFor(string action);
}
