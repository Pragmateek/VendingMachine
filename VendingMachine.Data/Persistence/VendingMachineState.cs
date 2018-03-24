using System;
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
        public virtual StoreState StoreState { get; set; }
        public virtual CashRegisterState CashRegisterState { get; set; }
        public virtual ControlPanelState ControlPanelState { get; set; }

        public VendingMachineState()
        {
        }

        public VendingMachineState(IVendingMachine vendingMachine, string name = null)
        {
            Name = name;
            AcceptedCoinsTypesNames = string.Join(",", vendingMachine.AcceptedCoinsTypes);
            CatalogState = string.Join(",", vendingMachine.Catalog.Select(entry => $"{entry.Product}:{entry.Price.Currency}{entry.Price.Amount.ToString(CultureInfo.InvariantCulture)}"));
            StoreState = new StoreState(vendingMachine.Store);
            CashRegisterState = new CashRegisterState(vendingMachine.CashRegister);
            ControlPanelState = new ControlPanelState(vendingMachine.ControlPanel);
        }

        public virtual IVendingMachine AsVendingMachine()
        {
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

            var vendingMachine = new Implementation.VendingMachine(catalog, 0, null, 0);

            return vendingMachine;
        }
    }
}
