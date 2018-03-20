using VendingMachine.Business;
using VendingMachine.Business.Implementation;

namespace VendingMachine.Data
{
    public class VendingMachineItemsRepository
    {
        public static readonly IVendingMachineItem Water = new VendingMachineItem("Water");
        public static readonly IVendingMachineItem CocaCola = new VendingMachineItem("Coca Cola");
        public static readonly IVendingMachineItem Fanta = new VendingMachineItem("Fanta");
    }
}
