using InventoryManager.Models;
using InventoryManager.Validators;
using Xunit;

namespace InventoryManager.Tests.Validators;

/// <summary>
/// Provide test methods for <see cref="Validator"/>
/// </summary>
public class ValidatorTest
{
    [Fact]
    public void IsValid_ShouldReturnFalse_WhenValueIsNull()
    {
        ProductList productList = new ProductList();
        bool result = Validator.IsValid(productList, "Name", null, out string? error);

        Assert.False(result);
        Assert.Null(error);
    }

    [Theory]
    [InlineData("Sankar", true)]
    [InlineData("HiBroHowAreYouDoingDude", false)]
    public void IsValid_ShouldPassForValidName_ShouldFailForInvalidName(string name, bool expected)
    {
        ProductList productList = new ProductList();
        bool result =  Validator.IsValid(productList, "Name", name, out string? error);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("1234567890", true)]
    [InlineData("12345678900", false)]
    [InlineData("1111111111", false)]
    public void IsValid_ShouldPassForValidId_ShouldFailForInvalidId(string id, bool expected)
    {
        ProductList productList = new ProductList();
        Product product = new Product(new Dictionary<string, object> { { "Id", "1111111111" } } );
        productList.Add(product);
        bool result = Validator.IsValid(productList, "Id", id, out string? error);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(20, true)]
    [InlineData(-10, false)]
    public void IsValid_ShouldPassForValidPrice_ShouldFailForInvalidPrice(decimal price, bool expected)
    {
        ProductList productList = new ProductList();
        bool result = Validator.IsValid(productList, "Price", price, out string? error);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(20, true)]
    [InlineData(-10, false)]
    public void IsValid_ShouldPassForValidQuantity_ShouldFailForInvalidQuantity(int quantity, bool expected)
    {
        ProductList productList = new ProductList();
        bool result = Validator.IsValid(productList, "Quantity", quantity, out string? error);

        Assert.Equal(expected, result);
    }
}
