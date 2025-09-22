using ConsoleTables;
using InventoryManager.Constants;
using InventoryManager.Models;

namespace InventoryManager.UI;

/// <summary>
/// Has functions related to console UI manipulations.
/// </summary>
public class ConsoleUI : IUserInterface
{
    /// <inheritdoc/>
    public string PromptAndGetInput(string prompt, ConsoleColor color = ConsoleColor.White)
    {
        do
        {
            Console.ForegroundColor = color;
            Console.Write(prompt);
            Console.ResetColor();
            string? userInput = Console.ReadLine();
            if (string.IsNullOrEmpty(userInput))
            {
                this.PromptLine(ErrorMessages.EmptyInput, ConsoleColor.Yellow);
                continue;
            }
            else
            {
                return userInput.Trim();
            }
        }
        while (true);
    }

    /// <inheritdoc/>
    public void Prompt(string info, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
        Console.Write(info);
        Console.ResetColor();
    }

    /// <inheritdoc/>
    public void PromptLine(string? prompt, ConsoleColor color = ConsoleColor.White)
    {
        if (string.IsNullOrEmpty(prompt))
        {
            return;
        }

        Console.ForegroundColor = color;
        Console.WriteLine(prompt);
        Console.ResetColor();
    }

    /// <inheritdoc/>
    public void PrintAsTable(List<Product> currentProductList)
    {
        Dictionary<string, Type> productTemplate = Product.GetTemplate();
        string[] fields = new string[] { "Index" }.Concat(productTemplate.Keys.ToArray()).ToArray();
        ConsoleTable table = new (fields);
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

    /// <inheritdoc/>
    public void WaitAndNavigateToMenu()
    {
        Console.WriteLine("\n\n\nPress any key to leave to menu...");
        Console.ReadKey();
        this.CreateNewPageFor("Menu");
    }

    /// <inheritdoc/>
    public void CreateNewPageFor(string action)
    {
        Console.Clear();
        this.Prompt($"Inventory manager - {action}", ConsoleColor.DarkBlue);
        Console.WriteLine("\n");
    }
}