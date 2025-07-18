using InventoryManager.Constants;
using InventoryManager.Models;

namespace InventoryManager.Validators;

/// <summary>
/// Provides generic validators for product fields using a <see cref="Validate"/> method.
/// </summary>
public class Validator
{
    /// <summary>
    /// Validate the value based on the field name and will not validate if it is an unhandled field.
    /// and out the appropriate error message if validation failed.
    /// </summary>
    /// <param name="list">Give access to user list</param>
    /// <param name="field">Field type to validate.</param>
    /// <param name="value">Value to validate.</param>
    /// <param name="error">Contains the error message of why validation failed, if validation passed error will be empty.</param>
    /// <returns><see cref="true"/> If value passes the specific field validation or unhandled fields; otherwise <see cref="false"/>.</returns>
    public static bool Validate(ProductList list, string field, object? value, out string error)
    {
        error = string.Empty;
        if (value is null)
        {
            return false;
        }

        switch (field)
        {
            case ProductFieldNames.Name:
                return ValidateName((string)value, out error);
            case ProductFieldNames.Id:
                return ValidateId(list, (string)value, out error);
            case ProductFieldNames.Price:
                return ValidatePrice((int)value, out error);
            case ProductFieldNames.Quantity:
                return ValidateQuantity((int)value, out error);
            default:
                error = string.Empty;
                return true;
        }
    }

    // Validates name and out the error
    private static bool ValidateName(string name, out string error)
    {
        if (name.Length >= 20)
        {
            error = ErrorMessages.NameShouldNotExceedChar;
            return false;
        }

        error = string.Empty;
        return true;
    }

    // Validates id and out the error
    private static bool ValidateId(ProductList list, string id, out string error)
    {
        if (id.Length != 10)
        {
            error = ErrorMessages.IdMustHave10Characters;
            return false;
        }
        else if (list.HasDuplicate(ProductFieldNames.Id, id))
        {
            error = ErrorMessages.IdDuplicate;
            return false;
        }

        error = string.Empty;
        return true;
    }

    // Validates price and out the error
    private static bool ValidatePrice(int price, out string error)
    {
        if (price < 0)
        {
            error = ErrorMessages.PriceIsNegative;
            return false;
        }

        error = string.Empty;
        return true;
    }

    // Validates quantity and out the error
    private static bool ValidateQuantity(int quantity, out string error)
    {
        if (quantity < 0)
        {
            error = ErrorMessages.ZeroOrNegativeQuantity;
            return false;
        }

        error = string.Empty;
        return true;
    }
}