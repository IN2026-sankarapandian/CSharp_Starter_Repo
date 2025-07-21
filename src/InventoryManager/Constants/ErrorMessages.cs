namespace InventoryManager.Constants;

/// <summary>
/// Contains status messages.
/// </summary>
public class ErrorMessages
{
    /// <summary>
    /// Error message of invalid choice option.
    /// </summary>
    public const string InvalidActionChoice = "Invalid option. Please try again.";

    /// <summary>
    /// Error message for no products.
    /// </summary>
    public const string NoProducts = "No products available !";

    /// <summary>
    /// Error message for no matches.
    /// </summary>
    public const string NoMatches = "No matches found !";

    /// <summary>
    /// Error message for files loss warning.
    /// </summary>
    public const string AppCLosingPrompt = "Warning : Closing the app will erase all added product details. Are you sure you want to continue? (y/n) :";

    /// <summary>
    /// Error message for not valid index.
    /// </summary>
    public const string NotValidIndex = "Enter a valid index !";

    /// <summary>
    /// Error message for not valid field.
    /// </summary>
    public const string NotValidField = "Enter a valid field !";

    /// <summary>
    /// Error message for no products in given index.
    /// </summary>
    public const string NoProductAtGivenIndex = "No product is available in the specified index !";

    /// <summary>
    /// Error message for input must be a number.
    /// </summary>
    public const string NeedANumber = "Input must be a number";

    /// <summary>
    /// Error message for name exceeding 20 characters.
    /// </summary>
    public const string NameShouldNotExceedChar = $"{ProductFieldNames.Name} should not exceed 20 characters !";

    /// <summary>
    /// Error message for id not equal to 10 characters.
    /// </summary>
    public const string IdMustHave10Characters = $"{ProductFieldNames.Id} must have 10 characters ! ";

    /// <summary>
    /// Error message for id duplication.
    /// </summary>
    public const string IdDuplicate = $"Product with the {ProductFieldNames.Id} already exist";

    /// <summary>
    /// Error message for price is negative.
    /// </summary>
    public const string PriceIsNegative = $"{ProductFieldNames.Price} cannot be a negative value !";

    /// <summary>
    /// Error message for not valid quantity.
    /// </summary>
    public const string ZeroOrNegativeQuantity = $"Atleast one {ProductFieldNames.Quantity} is required !";

    /// <summary>
    /// Error message for empty input.
    /// </summary>
    public const string EmptyInput = "Input cannot be empty. Please try again.";
}
