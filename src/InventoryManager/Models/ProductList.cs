using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager
{
    internal class ProductList
    {
        private List<Product> _productList = new List<Product>();

        public void Add(Product newProduct)
        {
            this._productList.Add(newProduct);
            Console.WriteLine("Added..");
        }

        public void Show()
        {
            foreach (Product product in this._productList)
            {
                Console.WriteLine(product["Name"]);
            }
        }

    }
}
