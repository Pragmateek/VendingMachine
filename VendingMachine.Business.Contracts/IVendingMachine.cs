using System.Collections.Generic;

namespace VendingMachine.Business.Contracts
{
    /// <summary>
    /// Acts as a facade for the underlying parts: store, cash-register, control-panel...
    /// </summary>
    public interface IVendingMachine
    {
        ICatalog Catalog { get; }
        IEnumerable<ICoinType> AcceptedCoinsTypes { get; }
        decimal InsertedAmount { get; }
        IStore Store { get; }
        ICashRegister CashRegister { get; }
        IControlPanel ControlPanel { get; }

        void Feed(IEnumerable<IItem> items);
        void Insert(ICoin coin);
        void Insert(IEnumerable<ICoin> coins);
        bool TryBuyItem(IProduct product, out IItem item);
    }
}
