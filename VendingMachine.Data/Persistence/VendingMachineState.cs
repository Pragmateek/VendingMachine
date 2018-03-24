using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using VendingMachine.Business.Contracts;
using VendingMachine.Business.Implementation;
using Implementation = VendingMachine.Business.Implementation;

namespace VendingMachine.Data
{
    public class VendingMachineState
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string CatalogState { get; set; }
        public virtual string AcceptedCoinsTypesNames { get; set; }
        public virtual int StoreSlotsCapacity { get; set; }
        public virtual int CashRegisterSlotsCapacity { get; set; }
        public virtual string StoreState { get; set; }
        public virtual string CashRegisterState { get; set; }
        public virtual string ControlPanelState { get; set; }

        public VendingMachineState()
        {
        }

        public VendingMachineState(IVendingMachine vendingMachine, string name = null)
        {
            Name = name;
            AcceptedCoinsTypesNames = string.Join(",", vendingMachine.AcceptedCoinsTypes.Select(coinType => coinType.Name));
            StoreSlotsCapacity = (int)vendingMachine.Store.Slots.First().Capacity;
            CashRegisterSlotsCapacity = (int)vendingMachine.CashRegister.Slots.First().Capacity;
            CatalogState = string.Join(",", vendingMachine.Catalog.Select(entry => $"{entry.Product}:{entry.Price.Currency}{entry.Price.Amount.ToString(CultureInfo.InvariantCulture)}"));
            StoreState = string.Join(",", vendingMachine.Store.Slots.Select(slot => $"{slot.CatalogEntry.Product.Name}:{slot.Count}/{slot.Capacity}"));
            CashRegisterState = string.Join(",", vendingMachine.CashRegister.Slots.Select(slot => $"{slot.CoinType.Name}:{slot.Count}/{slot.Capacity}"));
            ControlPanelState = string.Join(",", vendingMachine.ControlPanel.InsertedCoins.Select(coin => coin.Type.Name));
        }

        public virtual IVendingMachine AsVendingMachine()
        {
            var acceptedCoinsTypes = AcceptedCoinsTypesNames.Split(',').Select(token => CoinsTypesRepository.GetCoinTypeByName(token));

            var catalog = new Catalog();
            var catalogEntriesStates = CatalogState.Split(',');
            foreach (var entryState in catalogEntriesStates)
            {
                var tokens = entryState.Split(':');

                var productName = tokens[0];
                var product = ProductsRepository.GetProductByName(productName);

                var priceState = tokens[1];
                var currencyName = priceState.Substring(0, 3);
                var currency = CurrenciesRepository.GetCurrencyByName(currencyName);
                var amount = decimal.Parse(priceState.Substring(3), CultureInfo.InvariantCulture);
                var price = new Price(currency, amount);

                catalog.ReferenceProduct(product, price);
            }

            var vendingMachine = new Implementation.VendingMachine(catalog, (uint)StoreSlotsCapacity, acceptedCoinsTypes, (uint)CashRegisterSlotsCapacity);

            //var storeSlots = new List<StoreSlot>();
            var storeSlotsStates = StoreState.Split(',');
            var store = new Store(catalog, (uint)storeSlotsStates.Length);
            foreach (var storeSlotState in storeSlotsStates)
            {
                var tokens = storeSlotState.Split(':');

                var productName = tokens[0];
                var product = ProductsRepository.GetProductByName(productName);

                var countCapacityTokens = tokens[1].Split('/');
                var count = uint.Parse(countCapacityTokens[0]);
                var capacity = uint.Parse(countCapacityTokens[1]);

                var items = ItemsFactory.Make(product, count);

                vendingMachine.Feed(items);
            }

            var cashRegisterSlotsStates = CashRegisterState.Split(',');
            var cashRegister = new CashRegister(acceptedCoinsTypes, (uint)cashRegisterSlotsStates.Length);
            foreach (var cashRegisterSlotState in cashRegisterSlotsStates)
            {
                var tokens = cashRegisterSlotState.Split(':');

                var coinTypeName = tokens[0];
                var coinType = CoinsTypesRepository.GetCoinTypeByName(coinTypeName);

                var countCapacityTokens = tokens[1].Split('/');
                var count = uint.Parse(countCapacityTokens[0]);
                var capacity = uint.Parse(countCapacityTokens[1]);

                var coins = CoinsFactory.Make(coinType, count);

                vendingMachine.Insert(coins);
            }

            return vendingMachine;
        }
    }
}
