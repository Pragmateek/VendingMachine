using VendingMachine.Business.Contracts;
using VendingMachine.Business.Implementation;

namespace VendingMachine.Data
{
    public class ProductsRepository
    {
        public static readonly IProduct Evian = new Product("Evian");
        public static readonly IProduct CocaCola = new Product("Coca Cola");
        public static readonly IProduct Fanta = new Product("Fanta");
    }
}
