using System;
using System.Linq;
using System.Windows.Forms;
using VendingMachine.Business.Implementation;
using VendingMachine.Data;
using VendingMachine.UI.Controls;
using VendingMachine.UI.Controls.ViewModels;

namespace VendingMachine.UI.Client
{
    class Program
    {
        static void ShowCashRegisterSlot()
        {
            var cashRegisterSlot = new CashRegisterSlot(CoinsTypesRepository.FiftySwissFrancCents, 10);
            var cashRegisterSlotViewModel = new CashRegisterSlotViewModel(cashRegisterSlot);
            var cashRegisterSlotView = new CashRegisterSlotView
            {
                Model = cashRegisterSlotViewModel,
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle
            };

            var UI = new Form();
            UI.Controls.Add(cashRegisterSlotView);

            UI.ShowDialog();
        }

        static void ShowCashRegister()
        {
            var cashRegister = new CashRegister(CoinsTypesRepository.SwissFrancCoins, 10);
            var cashRegisterViewModel = new CashRegisterViewModel(cashRegister);
            var cashRegisterView = new CashRegisterView(cashRegisterViewModel)
            {                
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle
            };

            var UI = new Form();
            UI.Controls.Add(cashRegisterView);

            UI.ShowDialog();
        }

        static void ShowVendingMachineStoreSlot()
        {
            var FantaPrice = new Price(CurrenciesRepository.CHF, 1.60m);
            var catalogEntry = new VendingMachineCatalogEntry(VendingMachineProductsRepository.Fanta, FantaPrice);

            var slot = new VendingMachineStoreSlot(catalogEntry, 10);

            var items = Enumerable.Range(1, 5).Select(_ => new VendingMachineItem(VendingMachineProductsRepository.Fanta));
            slot.Feed(items);

            var vendingMachineStoreSlotViewModel = new VendingMachineStoreSlotViewModel(slot);
            var vendingMachineStoreSlotView = new VendingMachineStoreSlotView(vendingMachineStoreSlotViewModel)
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle
            };

            var UI = new Form();
            UI.Controls.Add(vendingMachineStoreSlotView);

            UI.ShowDialog();
        }

        [STAThread]
        static void Main(string[] args)
        {
            ShowVendingMachineStoreSlot();
        }
    }
}
