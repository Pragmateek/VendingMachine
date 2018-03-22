using System.Collections.Generic;

namespace VendingMachine.Business.Contracts
{
    public interface IVendingMachineControlPanel
    {
        decimal InsertedAmount { get; }

        IEnumerable<IProductChoice> ProductsChoices { get; }

        void Insert(ICoin coin);
        IEnumerable<ICoin> Refund();

        bool TryBuy(IVendingMachineProduct product);
    }
}
