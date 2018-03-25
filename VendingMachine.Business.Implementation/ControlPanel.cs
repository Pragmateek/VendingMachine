using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using VendingMachine.Business.Contracts;
using VendingMachine.Tools;

namespace VendingMachine.Business.Implementation
{
    public class ControlPanel : IControlPanel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private readonly ICatalog catalog = null;
        private readonly IStore store = null;

        private readonly IEnumerable<ICoinType> acceptedCoinsTypes;
        public IEnumerable<ICoinType> AcceptedCoinsTypes => acceptedCoinsTypes;

        public decimal InsertedAmount { get; private set; }

        //private IList<ICoin> insertedCoins = new List<ICoin>();
        //public IEnumerable<ICoin> InsertedCoins => insertedCoins;

        private readonly IEnumerable<ProductChoice> productsChoices;
        public IEnumerable<IProductChoice> ProductsChoices => productsChoices;

        private readonly ICashRegister cashRegister;

        public ControlPanel(ICatalog productsCatalog, IEnumerable<ICoinType> acceptedCoinsTypes, IStore store, ICashRegister cashRegister)
        {
            this.catalog = productsCatalog;
            this.productsChoices = productsCatalog.Select(catalogEntry => new ProductChoice(catalogEntry)).ToArray();
            this.acceptedCoinsTypes = acceptedCoinsTypes;
            this.store = store;
            this.cashRegister = cashRegister;
        }

        private void NotifyMoneyChanged()
        {
            //PropertyChanged(this, new PropertyChangedEventArgs(nameof(InsertedCoins)));
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(InsertedAmount)));
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(CanRefund)));
        }

        private void UpdateChoicesStates()
        {
            foreach (var productChoice in productsChoices)
            {
                var isProductAvailable = store.Has(productChoice.CatalogEntry.Product);
                var isNotTooExpensive = productChoice.CatalogEntry.Price.Amount <= InsertedAmount;
                productChoice.IsPossible = isProductAvailable && isNotTooExpensive;
            }
        }

        public bool TryInsert(ICoin coin)
        {
            if (!AcceptedCoinsTypes.Any(coinType => coinType == coin.Type))
            {
                //throw new Exception($"'{coin}' is not accepted!");
                return false;
            }

            if (!cashRegister.TryPut(coin))
            {
                return false;
            }

            InsertedAmount += coin.Type.FaceValue;

            UpdateChoicesStates();

            NotifyMoneyChanged();

            return true;
        }

        public void Insert(IEnumerable<ICoin> coins)
        {
            foreach (var coin in coins)
            {
                TryInsert(coin);
            }
        }

        public bool CanRefund => cashRegister.CanGetChangeOn(InsertedAmount);

        public IEnumerable<ICoin> Refund()
        {
            //var insertedCoins = this.insertedCoins.ToArray();

            IEnumerable<ICoin> refundCoins;
            if (!cashRegister.TryGetChange(InsertedAmount, out refundCoins))
            {
                return refundCoins;
            }

            //this.insertedCoins.Clear();
            InsertedAmount = 0;
            NotifyMoneyChanged();

            UpdateChoicesStates();

            return refundCoins;
        }

        public bool TryBuy(IProduct product, out IItem item)
        {
            item = null;

            var catalogEntry = catalog.GetEntryFor(product);

            if (catalogEntry.Price.Amount > InsertedAmount)
            {
                return false;
            };

            if (!store.TryGet(product, out item))
            {
                return false;
            }

            InsertedAmount -= catalogEntry.Price.Amount;

            NotifyMoneyChanged();
            UpdateChoicesStates();

            return true;
        }

        public override bool Equals(object obj)
        {
            var otherControlPanel = obj as ControlPanel;

            return otherControlPanel != null &&
                otherControlPanel.AcceptedCoinsTypes.SequenceEqual(AcceptedCoinsTypes) &&
                //otherControlPanel.InsertedCoins.SequenceEqual(InsertedCoins) &&
                otherControlPanel.ProductsChoices.SequenceEqual(ProductsChoices);
        }

        public override int GetHashCode()
        {
            return acceptedCoinsTypes.GetElementsHashCode();
        }
    }
}
