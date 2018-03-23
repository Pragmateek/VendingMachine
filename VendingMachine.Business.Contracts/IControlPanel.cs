using System.Collections.Generic;

namespace VendingMachine.Business.Contracts
{
    public interface IControlPanel
    {
        IEnumerable<ICoinType> AcceptedCoinsTypes { get; }
        IEnumerable<ICoin> InsertedCoins { get; }

        IEnumerable<IProductChoice> ProductsChoices { get; }

        void Insert(ICoin coin);
        IEnumerable<ICoin> Refund();

        bool TryBuy(IProduct product);
    }
}
