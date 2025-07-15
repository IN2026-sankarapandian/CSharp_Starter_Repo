using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager
{
    internal class Validators
    {
        public static bool Validate(string field, object value)
        {
            switch (field)
            {
                case "Name":
                    return ValidateName((string)value);
                case "Id":
                    return ValidateId((string)value);
                case "Price":
                    return ValidatePrice((int)value);
                case "Quantity":
                    return ValidateQuantity((int)value);
                default:
                    return true;
            }
        }

        private static bool ValidateName(string name)
        {
            if (name.Length >= 20)
            {
                Console.WriteLine("Should not exceed 20 characters");
                return false;
            }
            return true;
        }

        private static bool ValidateId(string id)
        {
            if (id.Length != 10)
            {
                Console.WriteLine("Must be 10 character");
                return false;
            }

            return true;
        }

        private static bool ValidatePrice(int price)
        {
            if (price < 0)
            {
                Console.WriteLine("Price annot be a negative value");
                return false;
            }
            return true;
        }
        private static bool ValidateQuantity(int quantity)
        {
            if (quantity < 0)
            {
                Console.WriteLine("Atleast one quantity is required");
                return false;
            }
            return true;
        }
    }
}
