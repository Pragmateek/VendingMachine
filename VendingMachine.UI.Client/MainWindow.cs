using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VendingMachine.Business.Implementation;
using VendingMachine.UI.Controls;
using VendingMachine.UI.Controls.ViewModels;

namespace VendingMachine.UI.Client
{
    public class MainWindow : Form
    {
        public MainWindow()
        {
            Menu = new MainMenu();

            var fileItem = new MenuItem("File");
            Menu.MenuItems.Add(fileItem);

            fileItem.MenuItems.Add("Open");
            fileItem.MenuItems.Add("Save");

            var vendingMachine = new LombardOdierVendingMachine();

            var vendingMachineViewModel = new VendingMachineViewModel(vendingMachine);
            var vendingMachineView = new VendingMachineView(vendingMachineViewModel)
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle
            };

            Controls.Add(vendingMachineView);
        }
    }
}
