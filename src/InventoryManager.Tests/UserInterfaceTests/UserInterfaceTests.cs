using InventoryManager.Constants;
using InventoryManager.Models;
using InventoryManager.UI;
using Xunit;

namespace InventoryManager.Tests.UserInterfaceTests;

/// <summary>
/// Provide test methods to test user interface
/// </summary>
public class UserInterfaceTests
{
    /// <summary>
    /// Tests whether prompt and get input gets the input from user
    /// </summary>
    [Fact]
    public void PromptAndGetInput_ShouldReturnNotNullInput()
    {
        StringReader stringWriter = new StringReader("\nHelo");

        Console.SetIn(stringWriter);

        IUserInterface userInterface = new ConsoleUI();

        string input = userInterface.PromptAndGetInput("Enter : ", ConsoleColor.White);
        Assert.Equal("Helo", input);
    }

    /// <summary>
    /// Tests whether print table function prints all the data
    /// </summary>
    [Fact]
    public void PrintTable_PrintsAllData()
    {
        ProductList productList = new ProductList();
        PopulateProductList(productList);

        StringWriter stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        IUserInterface userInterface = new ConsoleUI();

        userInterface.PrintAsTable(productList.Get());

        Assert.Contains("1234567890", stringWriter.ToString());
        Assert.Contains("1111111111", stringWriter.ToString());
        Assert.Contains("Mobile", stringWriter.ToString());
        Assert.Contains("Phone", stringWriter.ToString());
    }

    /// <summary>
    /// Tests whether print prompt function prints specified prompt.
    /// </summary>
    [Fact]
    public void PrintPrompt_DisplaysPrompt()
    {
        StringWriter stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        IUserInterface userInterface = new ConsoleUI();

        userInterface.Prompt("Information", ConsoleColor.White);

        Assert.Contains("Information", stringWriter.ToString());
    }

    /// <summary>
    /// Tests whether prompt line function prints all the specified data
    /// </summary>
    [Fact]
    public void PromptLine_ShouldReturn_IfPromptIsNull()
    {
        StringWriter stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        IUserInterface userInterface = new ConsoleUI();

        userInterface.PromptLine(null, ConsoleColor.White);

        Assert.Contains(string.Empty, stringWriter.ToString());
    }

    /// <summary>
    /// Populate the specified list with sample products.
    /// </summary>
    /// <param name="productList">Product list to populate.</param>
    private static void PopulateProductList(ProductList productList)
    {
        Product product1 = new Product(
                    new Dictionary<string, object>
                    {
                { ProductFieldNames.Id, "1234567890" },
                { ProductFieldNames.Name, "Mobile" },
                { ProductFieldNames.Price, 2000.21M },
                { ProductFieldNames.Quantity, 21 },
                    });
        Product product2 = new Product(
            new Dictionary<string, object>
            {
                { ProductFieldNames.Id, "1111111111" },
                { ProductFieldNames.Name, "Phone" },
                { ProductFieldNames.Price, 2000.21M },
                { ProductFieldNames.Quantity, 21 },
            });
        productList.Add(product1);
        productList.Add(product2);
    }
}
