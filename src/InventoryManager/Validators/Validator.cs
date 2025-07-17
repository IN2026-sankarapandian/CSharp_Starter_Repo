using InventoryManager.UI;

namespace InventoryManager.Validators;

/// <summary>
/// Provides validators for product fields via a <see cref="Validate"/> method.
/// </summary>
public class Validators
{
    /// <summary>
    /// Validate the value based on the field name and will not validate if it is an unhandled field.
    /// out the appropriate error message if validation failed.
    /// </summary>
    /// <param name="field">Field to type to validate.</param>
    /// <param name="value">Value to validate.</param>
    /// <param name="error">Contains the error message of why validation failed, if validation passed error will be empty.</param>
    /// <returns><see cref="true"/> If value passes the specific field validation or unhandled fields; otherwise <see cref="false"/>.</returns>
    public static bool Validate(string field, object? value, out string error)
    {
        if (value is null)
        {
            error = "Value cannot be null !";
            return false;
        }

        switch (field)
        {
            case "Name":
                return ValidateName((string)value, out error);
            case "Id":
                return ValidateId((string)value, out error);
            case "Price":
                return ValidatePrice((int)value, out error);
            case "Quantity":
                return ValidateQuantity((int)value, out error);
            default:
                error = string.Empty;
                return true;
        }
    }

    private static bool ValidateName(string name, out string error)
    {
        if (name.Length >= 20)
        {
            error = "Name should not exceed 20 characters !";
            return false;
        }

        error = string.Empty;
        return true;
    }

    private static bool ValidateId(string id, out string error)
    {
        if (id.Length != 10)
        {
            error = "Id must have 10 characters ! ";
            return false;
        }

        error = string.Empty;
        return true;
    }

    private static bool ValidatePrice(int price, out string error)
    {
        if (price < 0)
        {
            error = "Price cannot be a negative value !";
            return false;
        }

        error = string.Empty;
        return true;
    }

    private static bool ValidateQuantity(int quantity, out string error)
    {
        if (quantity < 0)
        {
            error = "Atleast one quantity is required !";
            return false;
        }

        error = string.Empty;
        return true;
    }
}