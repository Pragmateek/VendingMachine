﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class ControlPanel : IControlPanel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        //private readonly IVendingMachine controlledVendingMachine;

        private readonly IEnumerable<ICoinType> acceptedCoinsTypes;
        public IEnumerable<ICoinType> AcceptedCoinsTypes => acceptedCoinsTypes;

        private IList<ICoin> insertedCoins = new List<ICoin>();
        public IEnumerable<ICoin> InsertedCoins => insertedCoins;

        private readonly IEnumerable<ProductChoice> productsChoices;
        public IEnumerable<IProductChoice> ProductsChoices => productsChoices;

        public ControlPanel(ICatalog productsCatalog, IEnumerable<ICoinType> acceptedCoinsTypes/*IVendingMachine controlledVendingMachine*/)
        {
            this.productsChoices = productsCatalog.Select(catalogEntry => new ProductChoice(catalogEntry)).ToArray();
            this.acceptedCoinsTypes = acceptedCoinsTypes;
        }

        private void UpdateChoicesStates()
        {
            var insertedAmount = InsertedCoins.Sum(coin => coin.Type.FaceValue);

            foreach (var productChoice in productsChoices)
            {
                productChoice.IsPossible = insertedAmount >= productChoice.CatalogEntry.Price.Amount;
            }
        }

        public void Insert(ICoin coin)
        {
            if (!AcceptedCoinsTypes.Any(coinType => coinType == coin.Type))
            {
                throw new Exception($"'{coin}' is not accepted!");
            }

            insertedCoins.Add(coin);

            UpdateChoicesStates();

            PropertyChanged(this, new PropertyChangedEventArgs(nameof(InsertedCoins)));
        }

        public IEnumerable<ICoin> Refund()
        {
            var insertedCoins = this.insertedCoins.ToArray();

            this.insertedCoins.Clear();

            return insertedCoins;
        }

        public bool TryBuy(IProduct product)
        {
            throw new NotImplementedException();
        }
    }
}
