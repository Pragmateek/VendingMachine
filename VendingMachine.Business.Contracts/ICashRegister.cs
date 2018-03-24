using System.Collections.Generic;

namespace VendingMachine.Business.Contracts
{
    public interface ICashRegister
    {
        IEnumerable<ICashRegisterSlot> Slots { get; }
        decimal Amount { get; }

        void Put(ICoin coin);
        void Put(IEnumerable<ICoin> coins);

        bool TryGetChange(decimal amount, out IEnumerable<ICoin> cash);

        void Remove(IEnumerable<ICoin> coins);
    }
}
