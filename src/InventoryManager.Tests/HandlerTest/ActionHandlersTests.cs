using InventoryManager.Constants;
using InventoryManager.Handlers;
using InventoryManager.Models;
using InventoryManager.UI;
using Moq;
using Xunit;

namespace InventoryManager.Tests.HandlerTest;

/// <summary>
/// Provide test methods for <see cref="ActionHandler"/>
/// </summary>
public class ActionHandlersTests
{
    /// <summary>
    /// Tests whether HandleAddProduct adds the product
    /// </summary>
    [Fact]
    public void HandleAddProduct_ShouldAddProduct()
    {
        Mock<IUserInterface> uiMock = new Mock<IUserInterface>();
        Mock<IFormHandler> formHandlerMock = new Mock<IFormHandler>();

        uiMock.SetupSequence(ui => ui.PromptAndGetInput(It.IsAny<string>(), ConsoleColor.White))
            .Returns("1234567890")
            .Returns("Sankar")
            .Returns("2000.21")
            .Returns("21");

        ProductList productList = new ProductList();

        ActionHandler actionHandler = new ActionHandler(uiMock.Object, formHandlerMock.Object);

        actionHandler.HandleAddProduct(productList);

        Assert.Equal(1, productList.Count());
    }

    /// <summary>
    /// Tests whether HandleShowProduct shows the products if available.
    /// </summary>
    [Fact]
    public void HandleShowProduct_ShouldShowProduct_IfProductAvailable()
    {
        Mock<IUserInterface> uiMock = new Mock<IUserInterface>();
        Mock<IFormHandler> formHandlerMock = new Mock<IFormHandler>();
        ProductList productList = new ProductList();

        formHandlerMock.Setup(formHandler => formHandler.ShowProducts(productList)).Returns(true);

        ActionHandler actionHandler = new ActionHandler(uiMock.Object, formHandlerMock.Object);

        actionHandler.HandleShowProducts(productList);

        formHandlerMock.Verify(formHandler => formHandler.ShowProducts(productList));
        uiMock.Verify(ui => ui.WaitAndNavigateToMenu(), Times.Once);
    }

    /// <summary>
    /// Tests whether HandleShowProduct not shows the products if products unavailable.
    /// </summary>
    [Fact]
    public void HandleShowProduct_ShouldNotShowProduct_IfProductUnavailable()
    {
        Mock<IUserInterface> uiMock = new Mock<IUserInterface>();
        Mock<IFormHandler> formHandlerMock = new Mock<IFormHandler>();
        ProductList productList = new ProductList();

        formHandlerMock.Setup(formHandler => formHandler.ShowProducts(productList)).Returns(false);

        ActionHandler actionHandler = new ActionHandler(uiMock.Object, formHandlerMock.Object);

        actionHandler.HandleShowProducts(productList);

        formHandlerMock.Verify(formHandler => formHandler.ShowProducts(productList));
        uiMock.Verify(ui => ui.WaitAndNavigateToMenu(), Times.Never);
    }

    /// <summary>
    /// Tests whether HandleEditProduct edits the product.
    /// </summary>
    [Fact]
    public void HandleEditProduct_ShouldEditProduct()
    {
        Mock<IUserInterface> uiMock = new Mock<IUserInterface>();
        Mock<IFormHandler> formHandlerMock = new Mock<IFormHandler>();

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

        formHandlerMock.Setup(formHandler => formHandler.ShowProducts(productList)).Returns(true);
        formHandlerMock.Setup(formHandler => formHandler.GetIndex(productList)).Returns(1);
        formHandlerMock.Setup(formHandler => formHandler.GetFieldName(productList)).Returns(ProductFieldNames.Id);
        uiMock.Setup(ui => ui.PromptAndGetInput(It.IsAny<string>(), ConsoleColor.White))
            .Returns("1234567890");

        ActionHandler actionHandler = new ActionHandler(uiMock.Object, formHandlerMock.Object);

        actionHandler.HandleEditProduct(productList);

        Assert.Equal("1234567890", productList.Get()[0][ProductFieldNames.Id]);
    }

    /// <summary>
    /// Tests whether HandleEditProduct returns if products unavailable.
    /// </summary>
    [Fact]
    public void HandleEditProduct_ShouldNotShowProduct_IfProductUnavailable()
    {
        Mock<IUserInterface> uiMock = new Mock<IUserInterface>();
        Mock<IFormHandler> formHandlerMock = new Mock<IFormHandler>();
        ProductList productList = new ProductList();

        formHandlerMock.Setup(formHandler => formHandler.ShowProducts(productList)).Returns(false);

        ActionHandler actionHandler = new ActionHandler(uiMock.Object, formHandlerMock.Object);

        actionHandler.HandleEditProduct(productList);

        formHandlerMock.Verify(formHandler => formHandler.ShowProducts(productList));
        uiMock.Verify(ui => ui.WaitAndNavigateToMenu(), Times.Never);
    }

    /// <summary>
    /// Tests whether HandleDeleteProduct deletes the product.
    /// </summary>
    [Fact]
    public void HandleDeleteProduct_ShouldDeleteProduct()
    {
        Mock<IUserInterface> uiMock = new Mock<IUserInterface>();
        Mock<IFormHandler> formHandlerMock = new Mock<IFormHandler>();

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

        formHandlerMock.Setup(formHandler => formHandler.ShowProducts(productList)).Returns(true);
        formHandlerMock.Setup(formHandler => formHandler.GetIndex(productList)).Returns(1);

        ActionHandler actionHandler = new ActionHandler(uiMock.Object, formHandlerMock.Object);

        actionHandler.HandleDeleteProduct(productList);

        Assert.Equal(0, productList.Count());
    }

    /// <summary>
    /// Tests whether HandleDeleteProduct returns if products unavailable.
    /// </summary>
    [Fact]
    public void HandleDeleteProduct_ShouldNotShowProduct_IfProductUnavailable()
    {
        Mock<IUserInterface> uiMock = new Mock<IUserInterface>();
        Mock<IFormHandler> formHandlerMock = new Mock<IFormHandler>();
        ProductList productList = new ProductList();

        formHandlerMock.Setup(formHandler => formHandler.ShowProducts(productList)).Returns(false);

        ActionHandler actionHandler = new ActionHandler(uiMock.Object, formHandlerMock.Object);

        actionHandler.HandleDeleteProduct(productList);

        formHandlerMock.Verify(formHandler => formHandler.ShowProducts(productList));
        uiMock.Verify(ui => ui.WaitAndNavigateToMenu(), Times.Never);
    }

    /// <summary>
    /// Tests whether HandleSearchProduct returns if no products exist.
    /// </summary>
    [Fact]
    public void HandleSearchProduct_ShouldReturn_IFNoProductsAvailable()
    {
        Mock<IUserInterface> uiMock = new Mock<IUserInterface>();
        Mock<IFormHandler> formHandlerMock = new Mock<IFormHandler>();

        ProductList productList = new ProductList();

        uiMock.Setup(ui => ui.PromptAndGetInput(It.IsAny<string>(), ConsoleColor.White))
            .Returns("123");

        ActionHandler actionHandler = new ActionHandler(uiMock.Object, formHandlerMock.Object);

        actionHandler.HandleSearchProduct(productList);

        uiMock.Verify(ui => ui.PromptLine(ErrorMessages.NoProducts, ConsoleColor.Yellow), Times.Once());
    }

    /// <summary>
    /// Tests whether HandleSearchProduct says no matches found if keyword didn't mached with any.
    /// </summary>
    [Fact]
    public void HandleSearchProduct_ShouldFetchAndShowProductUnAvailable_IfMatchesNotFound()
    {
        Mock<IUserInterface> uiMock = new Mock<IUserInterface>();
        Mock<IFormHandler> formHandlerMock = new Mock<IFormHandler>();

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

        uiMock.Setup(ui => ui.PromptAndGetInput(It.IsAny<string>(), ConsoleColor.White))
            .Returns("123");

        ActionHandler actionHandler = new ActionHandler(uiMock.Object, formHandlerMock.Object);

        actionHandler.HandleSearchProduct(productList);

        uiMock.Verify(ui => ui.PrintAsTable(new List<Product> { product }), Times.Once());
    }

    /// <summary>
    /// Tests whether HandleSearchProduct should find and shows the product if matches found.
    /// </summary>
    [Fact]
    public void HandleSearchProduct_ShouldFetchAndShowProduct_IfMatchesFound()
    {
        Mock<IUserInterface> uiMock = new Mock<IUserInterface>();
        Mock<IFormHandler> formHandlerMock = new Mock<IFormHandler>();

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

        uiMock.Setup(ui => ui.PromptAndGetInput(It.IsAny<string>(), ConsoleColor.White))
            .Returns("xyz");

        ActionHandler actionHandler = new ActionHandler(uiMock.Object, formHandlerMock.Object);

        actionHandler.HandleSearchProduct(productList);

        uiMock.Verify(ui => ui.PrintAsTable(new List<Product> { product }), Times.Never());
    }

    /// <summary>
    /// Check whether app exits if user give "Y"
    /// </summary>
    [Fact]
    public async void ConfirmExit_ShouldExit_ForValidInput()
    {
        Mock<IUserInterface> uiMock = new Mock<IUserInterface>();
        Mock<IFormHandler> formHandlerMock = new Mock<IFormHandler>();

        uiMock.SetupSequence(ui => ui.PromptAndGetInput(ErrorMessages.AppClosingPrompt, ConsoleColor.Magenta))
            .Returns("k")
            .Returns("Y");

        ActionHandler actionHandler = new ActionHandler(uiMock.Object, formHandlerMock.Object);
        await actionHandler.ConfirmExitAsync();

        uiMock.Verify(ui => ui.PromptLine("Closing the app...", ConsoleColor.Magenta), Times.Once);
    }

    /// <summary>
    /// Check whether app go back to menu if user give "N"
    /// </summary>
    [Fact]
    public async void ConfirmExit_ShouldNotExit_ForValidInput()
    {
        Mock<IUserInterface> uiMock = new Mock<IUserInterface>();
        Mock<IFormHandler> formHandlerMock = new Mock<IFormHandler>();

        uiMock.SetupSequence(ui => ui.PromptAndGetInput(ErrorMessages.AppClosingPrompt, ConsoleColor.Magenta))
            .Returns("k")
            .Returns("N");

        ActionHandler actionHandler = new ActionHandler(uiMock.Object, formHandlerMock.Object);
        await actionHandler.ConfirmExitAsync();

        uiMock.Verify(ui => ui.CreateNewPageFor("Menu"), Times.Once);
    }
}
