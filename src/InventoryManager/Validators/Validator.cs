using InventoryManager.Constants;
using InventoryManager.Models;

namespace InventoryManager.Validators;

/// <summary>
/// Provides generic validators for product fields using a <see cref="IsValid"/> method.
/// </summary>
public class Validator
{
    /// <summary>
    /// Validate the value based on the field name and return true with null out message if validation passed
    /// and return false with appropriate error message as out string if validation failed.
    /// It will not validate and return true with null string as out error message if it is an unhandled field.
    /// </summary>
    /// <param name="list">Give access to user list</param>
    /// <param name="field">Field type to validate.</param>
    /// <param name="value">Value to validate.</param>
    /// <param name="errorMessage">Contains the error message of why validation failed, if validation passed error will be empty.</param>
    /// <returns><see cref="true"/> If value passes the specific field validation or unhandled fields; otherwise <see cref="false"/>.</returns>
    public static bool IsValid(ProductList list, string field, object? value, out string? errorMessage)
    {
        if (value is null)
        {
            errorMessage = null;
            return false;
        }
        else if (field.Equals(ProductFieldNames.Name) && value is string)
        {
            return IsValidName((string)value, out errorMessage);
        }
        else if (field.Equals(ProductFieldNames.Id) && value is string)
        {
            return IsValidId(list, (string)value, out errorMessage);
        }
        else if (field.Equals(ProductFieldNames.Price) && value is decimal)
        {
            return IsValidPrice((decimal)value, out errorMessage);
        }
        else if (field.Equals(ProductFieldNames.Quantity) && value is int)
        {
            return IsValidQuantity((int)value, out errorMessage);
        }
        else
        {
            errorMessage = null;
            return true;
        }
    }

    // Validates name and out the error
    private static bool IsValidName(string name, out string? errorMessage)
    {
        if (name.Length >= 20)
        {
            errorMessage = ErrorMessages.NameShouldNotExceedChar;
            return false;
        }

        errorMessage = null;
        return true;
    }

    // Validates id and out the error
    private static bool IsValidId(ProductList list, string id, out string? errorMessage)
    {
        if (id.Length != 10)
        {
            errorMessage = ErrorMessages.IdMustHave10Characters;
            return false;
        }
        else if (list.HasDuplicate(ProductFieldNames.Id, id))
        {
            errorMessage = ErrorMessages.IdDuplicate;
            return false;
        }

        errorMessage = null;
        return true;
    }

    // Validates price and out the error
    private static bool IsValidPrice(decimal price, out string? errorMessage)
    {
        if (price < 0)
        {
            errorMessage = ErrorMessages.PriceIsNegative;
            return false;
        }

        errorMessage = null;
        return true;
    }

    // Validates quantity and out the error
    private static bool IsValidQuantity(int quantity, out string? errorMessage)
    {
        if (quantity < 0)
        {
            errorMessage = ErrorMessages.ZeroOrNegativeQuantity;
            return false;
        }

        errorMessage = null;
        return true;
    }
}