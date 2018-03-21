using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class VendingMachineProduct : IVendingMachineProduct
    {
        public string Name { get; private set; }

        public VendingMachineProduct(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
