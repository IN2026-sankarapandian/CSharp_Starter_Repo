using ConsoleTables;
using InventoryManager.Constants;
using InventoryManager.Models;

namespace InventoryManager.UI;

/// <summary>
/// Has functions related to console UI manipulations.
/// </summary>
public class ConsoleUI
{
    /// <summary>
    /// Prompt the user and returns the user input for the prompt.
    /// </summary>
    /// <param name="prompt">Prompt shown to the user.</param>
    /// <param name="color">Color for the prompt.</param>
    /// <returns>User input</returns>
    public static string PromptAndGetInput(string prompt, ConsoleColor color = ConsoleColor.White)
    {
        do
        {
            Console.ForegroundColor = color;
            Console.Write(prompt);
            Console.ResetColor();
            string? input = Console.ReadLine();
            if (input is null || input.Trim() == string.Empty)
            {
                PromptLine(ErrorMessages.EmptyInput, ConsoleColor.Yellow);
                continue;
            }
            else
            {
                return input.Trim();
            }
        }
        while (true);
    }

    /// <summary>
    /// Prompt the user with info with specified color.
    /// </summary>
    /// <param name="info">Info to shown.</param>
    /// <param name="color">Color for the prompt.</param>
    public static void Prompt(string info, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
        Console.Write(info);
        Console.ResetColor();
    }

    /// <summary>
    /// Prompt the user with info in new line with specified color.
    /// </summary>
    /// <param name="prompt">Prompt to show.</param>
    /// <param name="color">Color for the prompt. Default value is white.</param>
    public static void PromptLine(string prompt, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(prompt);
        Console.ResetColor();
    }

    /// <summary>
    /// Prints list of <see cref="Product"/>s as a table in the console.
    /// </summary>
    /// <param name="currentProductList">Product list to print.</param>
    public static void PrintAsTable(List<Product> currentProductList)
    {
        Dictionary<string, Type> productTemplate = Product.GetTemplate();
        string[] fields = new string[] { "Index" }.Concat(productTemplate.Keys.ToArray()).ToArray();
        ConsoleTable table = new ConsoleTable(fields);
        for (int index = 0; index < currentProductList.Count; index++)
        {
            object[] values = new object[fields.Length];
            values[0] = index + 1;
            for (int i = 1; i < fields.Length; i++)
            {
                values[i] = currentProductList[index][fields[i]];
            }

            table.AddRow(values);
        }

        table.Write();
    }

    /// <summary>
    /// Wait for the keypress and clear the console and navigate back to the menu.
    /// </summary>
    public static void WaitAndNavigateToMenu()
    {
        Console.WriteLine("\n\n\nPress any key to leave to menu...");
        Console.ReadKey();
        ConsoleUI.CreateNewPageFor("Menu");
    }

    /// <summary>
    /// Clears the console and create new page for the actions.
    /// </summary>
    /// <param name="action">Current action name.</param>
    public static void CreateNewPageFor(string action)
    {
        Console.Clear();
        Prompt($"Inventory manager - {action}", ConsoleColor.DarkBlue);
        Console.WriteLine("\n");
    }
}