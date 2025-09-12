using InventoryManager.Constants;
using InventoryManager.Handlers;
using InventoryManager.Models;
using InventoryManager.UI;
using Moq;
using Xunit;

namespace InventoryManager.Tests.HandlerTest;

/// <summary>
/// Provide test methods for <see cref="FormHandler"/>
/// </summary>
public class FormHandlerTest
{
    /// <summary>
    /// Tests get index function gets input until getting valid input.
    /// </summary>
    [Fact]
    public void GetIndex_ShouldGetInput_UntilGetValidInput()
    {
        Mock<IUserInterface> uiMock = new Mock<IUserInterface>();

        uiMock.SetupSequence(ui => ui.PromptAndGetInput(It.IsAny<string>(), ConsoleColor.White))
            .Returns("wqewq")
            .Returns("12")
            .Returns("1");

        ProductList productList = new ProductList();
        Product product = new Product(
            new Dictionary<string, object>
            {
                { ProductFieldNames.Id, "1111111111" },
                { ProductFieldNames.Name, "Sankar" },
                { ProductFieldNames.Price, 2000.21M },
                { ProductFieldNames.Quantity, 21 },
            });
        productList.Add(product);

        FormHandler formHandler = new FormHandler(uiMock.Object);

        int result = formHandler.GetIndex(productList);

        Assert.Equal(1, result);
        uiMock.Verify(ui => ui.PromptAndGetInput("Enter the index of the product : ", ConsoleColor.White), Times.Exactly(3));
    }

    /// <summary>
    /// Tests get field function gets input until getting valid input.
    /// </summary>
    [Fact]
    public void GetField_ShouldGetField_UntilGetValidInput()
    {
        Mock<IUserInterface> uiMock = new Mock<IUserInterface>();

        uiMock.SetupSequence(ui => ui.PromptAndGetInput(It.IsAny<string>(), ConsoleColor.White))
            .Returns("wqewq")
            .Returns("12")
            .Returns("1");

        ProductList productList = new ProductList();

        FormHandler formHandler = new FormHandler(uiMock.Object);

        string result = formHandler.GetFieldName(productList);

        Assert.Equal(ProductFieldNames.Name, result);
        uiMock.Verify(ui => ui.PromptAndGetInput("\nEnter a field : ", ConsoleColor.White), Times.Exactly(3));
    }

    /// <summary>
    /// Tests shows products function shows product when products available
    /// </summary>
    [Fact]
    public void ShowProducts_ShouldShowProducts_WhenProductsAvailable()
    {
        Mock<IUserInterface> uiMock = new Mock<IUserInterface>();

        ProductList productList = new ProductList();
        Product product = new Product(
            new Dictionary<string, object>
            {
                { ProductFieldNames.Id, "1234567890" },
                { ProductFieldNames.Name, "Sankar" },
                { ProductFieldNames.Price, 2000.21M },
                { ProductFieldNames.Quantity, 21 },
            });
        productList.Add(product);

        FormHandler formHandler = new FormHandler(uiMock.Object);

        bool result = formHandler.ShowProducts(productList);

        Assert.True(result);
        uiMock.Verify(ui => ui.PrintAsTable(productList.Get()), Times.Once);
    }

    /// <summary>
    /// Tests shows products function should not shows product when products unavailable
    /// </summary>
    [Fact]
    public void ShowProducts_ShouldNotShowProducts_WhenProductsUnavailable()
    {
        Mock<IUserInterface> uiMock = new Mock<IUserInterface>();

        ProductList productList = new ProductList();

        FormHandler formHandler = new FormHandler(uiMock.Object);

        bool result = formHandler.ShowProducts(productList);

        Assert.False(result);
        uiMock.Verify(ui => ui.PromptLine(ErrorMessages.NoProducts, ConsoleColor.Yellow), Times.Once);
    }
}
