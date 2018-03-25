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
            var catalogEntry = new CatalogEntry(ProductsRepository.Fanta, FantaPrice);

            var slot = new StoreSlot(catalogEntry, 10);

            var items = Enumerable.Range(1, 5).Select(_ => new Item(ProductsRepository.Fanta));
            slot.Store(items);

            var vendingMachineStoreSlotViewModel = new StoreSlotViewModel(slot);
            var vendingMachineStoreSlotView = new VendingMachineStoreSlotView(vendingMachineStoreSlotViewModel)
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle
            };

            var UI = new Form();
            UI.Controls.Add(vendingMachineStoreSlotView);

            UI.ShowDialog();
        }

        static void ShowVendingMachineStore()
        {
            var vendingMachine = new LombardOdierVendingMachine(10, 100);
            var items = ItemsFactory.Make(ProductsRepository.Evian, 7)
                .Then(ProductsRepository.CocaCola, 5)
                .Then(ProductsRepository.Fanta, 6);
            vendingMachine.Feed(items);

            var vendingMachineStoreViewModel = new StoreViewModel(vendingMachine.Store);
            var vendingMachineStoreView = new StoreView(vendingMachineStoreViewModel)
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle
            };

            var UI = new Form();
            UI.Controls.Add(vendingMachineStoreView);

            UI.ShowDialog();
        }

        static void ShowVendingMachineControlPanel()
        {
            var vendingMachine = new LombardOdierVendingMachine(10, 100);

            var controlPanelViewModel = new ControlPanelViewModel(vendingMachine.ControlPanel);
            var controlPanelView = new ControlPanelView(controlPanelViewModel)
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle
            };

            var UI = new Form();
            UI.Controls.Add(controlPanelView);

            UI.ShowDialog();
        }

        static void ShowVendingMachine()
        {
            var vendingMachine = new LombardOdierVendingMachine(10, 100);

            var vendingMachineViewModel = new VendingMachineViewModel(vendingMachine);
            var vendingMachineView = new VendingMachineView(vendingMachineViewModel)
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle
            };

            var UI = new Form();
            UI.Controls.Add(vendingMachineView);

            UI.ShowDialog();
        }

        [STAThread]
        static void Main(string[] args)
        {
            var mainWindowModel = new MainWindowModel
            {
                Configuration = new Configuration
                {
                    StoreSlotsCapacity = 10,
                    CashRegisterSlotsCapacity = 100,
                    InitialBottleCount = 5,
                    DatabasePath = VendingMachinesStatesRepository.DatabasePath
                }
            };

            new MainWindow(mainWindowModel)
            {
                Width = 800,
                Height = 800
            }.ShowDialog();
        }
    }
}
