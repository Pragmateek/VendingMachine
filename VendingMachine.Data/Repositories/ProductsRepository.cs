using System.Collections.Generic;
using System.Linq;
using VendingMachine.Business.Contracts;
using VendingMachine.Business.Implementation;

namespace VendingMachine.Data
{
    public class ProductsRepository
    {
        public static readonly IProduct Evian = new Product("Evian");
        public static readonly IProduct CocaCola = new Product("Coca Cola");
        public static readonly IProduct Fanta = new Product("Fanta");

        public static readonly IEnumerable<IProduct> AllProducts = new[] { Evian, CocaCola, Fanta };

        public static IProduct GetProductByName(string productName)
        {
            return AllProducts.FirstOrDefault(product => product.Name == productName);
        }
    }
}
