namespace VendingMachine.Business.Implementation
{
    public class VendingMachineItem : IVendingMachineItem
    {
        public string Name { get; private set; }

        public VendingMachineItem(string name)
        {
            Name = name;
        }
    }
}
