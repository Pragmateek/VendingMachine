using System.Collections.Generic;

namespace VendingMachine.Business.Contracts
{
    public interface IVendingMachine
    {
        uint Capacity { get; }

        IVendingMachineStore Store { get; }

        ICurrency Currency { get; }
        decimal InsertedAmount { get; }

        IVendingMachine Insert(ICoin coin);
        IEnumerable<ICoin> Refund();

        bool TryBuyItem(IVendingMachineItem item);
    }
}
