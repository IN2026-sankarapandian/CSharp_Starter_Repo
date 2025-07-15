using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignments;
using InventoryManager.Models;

namespace InventoryManager
{
    internal class FeatureHandlers
    {
        public static void AddProduct(ProductList list)
        {
            Dictionary<string, object>? newProductDetails = new Dictionary<string, object>();
            Dictionary<string, Type>? productTemplate = Product.GetTemplate();
            foreach (var field in productTemplate)
            {
                object result = null;
                string? input;
                do
                {
                    input = ConsoleUI.PromptAndGetInput($"{field.Key} : ");
                    if (input is null)
                    {
                        continue;
                    }
                }
                while (!(TryParseValue(input, field.Value, out result) && Validators.Validate(field.Key, result)));
                newProductDetails[field.Key] = result;
            }
            Product newProduct = new Product(newProductDetails);
            list.Add(newProduct);
        }

        /// <summary>
        /// Try to parse the value according to data type
        /// </summary>
        /// <param name="input">Input string</param>
        /// <param name="type">Type of the object</param>
        /// <param name="result">Parsed value</param>
        /// <returns>true if can parse, false if cant parsed</returns>
        public static bool TryParseValue(string input, Type type, out object result)
        {
            result = null;
            if (type == typeof(string))
            {
                result = input;
                return true;
            }
            if (type == typeof(int))
            {
                bool status = int.TryParse(input, out int number);
                result = number;
                Console.WriteLine("Give a valid input !");
                return status;
            }
            return false;
        }
    }
}
