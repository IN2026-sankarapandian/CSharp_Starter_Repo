using ConsoleTables;
using InventoryManager.Models;

namespace InventoryManager.UI;
/// <summary>
/// Has functions related to console manipulations
/// </summary>
public class ConsoleUI
{
    /// <summary>
    /// Gets the prompt and returns the user input for the prompt
    /// </summary>
    /// <param name="prompt">Prompt shown to the user</param>
    /// <returns>User input</returns>
    public static string? PromptAndGetInput(string prompt)
    {
        Console.Write(prompt);
        string? input = Console.ReadLine();
        return input;
    }

    /// <summary>
    /// Prompt the user with info with color
    /// </summary>
    /// <param name="info">Info to shown</param>
    /// <param name="color">Color to set</param>
    public static void PromptInfo(string info, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(info);
        Console.ResetColor();
    }

    /// <summary>
    /// Print the product list as a table
    /// </summary>
    /// <param name="userProductList"><see cref="ProductList"/> list to print</param>
    public static void PrintTable(List<Product> userProductList)
    {
        Dictionary<string, Type> template = Product.GetTemplate();
        string[] fields = template.Keys.ToArray();
        var table = new ConsoleTable(fields);
        foreach (Product product in userProductList)
        {
            object[] values = new object[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                values[i] = product[fields[i]];
            }

            table.AddRow(values);
            table.Write();
        }
    }
}
