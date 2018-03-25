using System.Collections.Generic;

namespace VendingMachine.Business.Contracts
{
    public interface IControlPanel
    {
        IEnumerable<ICoinType> AcceptedCoinsTypes { get; }
        IEnumerable<ICoin> InsertedCoins { get; }

        decimal InsertedAmount { get; }

        IEnumerable<IProductChoice> ProductsChoices { get; }

        void Insert(ICoin coin);
        void Insert(IEnumerable<ICoin> coins);

        IEnumerable<ICoin> Refund();

        bool TryBuy(IProduct product, out IItem item);
    }
}
