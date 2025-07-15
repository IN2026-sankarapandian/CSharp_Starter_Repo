using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager
{
    /// <summary>
    /// Represent a product with name, id
    /// </summary>
    internal class Product
    {
        private Dictionary<string, object> _product;

        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        /// <param name="newProduct">New product details</param>
        public Product(Dictionary<string, object> newProduct)
        {
            this._product = newProduct;
        }

        /// <summary>
        /// Indexer for easy access of values of <see cref="_product"/> with object
        /// </summary>
        /// <param name="key">Key of the dictionary</param>
        /// <returns>value for the key</returns>
        public object? this[string key]
        {
            get => this._product.ContainsKey(key) ? this._product[key] : null;
            set => this._product[key] = value!;
        }

        /// <summary>
        /// Holds the template of product details
        /// </summary>
        /// <returns>Template of product</returns>
        public static Dictionary<string, object> GetEmptyContact()
        {
            return new Dictionary<string, object>
            {
                {"Name ", string.Empty },
                {"Id", string.Empty },
                {"Price", 0D },
                {"Quantity", 0D },
            };
        }

        /// <summary>
        /// Holds the template of product details
        /// </summary>
        /// <returns>Template of product</returns>
        public static Dictionary<string, Type> GetTemplate()
        {
            return new Dictionary<string, Type>
            {
                {"Name", typeof(string) },
                {"Id", typeof(string) },
                {"Price", typeof(int) },
                {"Quantity", typeof(int) },
                {"Time", typeof(int) },
            };
        }
    }
}
