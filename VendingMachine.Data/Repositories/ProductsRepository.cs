using System.Collections.Generic;
using System.Linq;
using VendingMachine.Business.Contracts;
using VendingMachine.Business.Implementation;

namespace VendingMachine.Data
{
    public class ProductsRepository
    {
        public static readonly IProduct EvianBottle = new Product("Evian");
        public static readonly IProduct VittelBottle = new Product("Vittel");
        public static readonly IProduct VolvicBottle = new Product("Volvic");
        public static readonly IProduct CocaColaBottle = new Product("Coca Cola");
        public static readonly IProduct FantaBottle = new Product("Fanta");

        public static readonly IEnumerable<IProduct> AllProducts = new[] { EvianBottle, VittelBottle, VolvicBottle, CocaColaBottle, FantaBottle };

        public static IProduct GetProductByName(string productName)
        {
            return AllProducts.FirstOrDefault(product => product.Name == productName);
        }
    }
}
