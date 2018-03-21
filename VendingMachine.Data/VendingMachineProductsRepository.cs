using VendingMachine.Business.Contracts;
using VendingMachine.Business.Implementation;

namespace VendingMachine.Data
{
    public class VendingMachineProductsRepository
    {
        public static readonly IVendingMachineProduct Evian = new VendingMachineProduct("Evian");
        public static readonly IVendingMachineProduct CocaCola = new VendingMachineProduct("Coca Cola");
        public static readonly IVendingMachineProduct Fanta = new VendingMachineProduct("Fanta");
    }
}
