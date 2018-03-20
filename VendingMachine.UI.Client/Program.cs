using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VendingMachine.Data;
using VendingMachine.Implementation;
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

        [STAThread]
        static void Main(string[] args)
        {
            ShowCashRegister();
        }
    }
}
