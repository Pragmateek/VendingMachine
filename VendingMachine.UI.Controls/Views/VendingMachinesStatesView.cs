using System.Windows.Forms;
using VendingMachine.Data;
using VendingMachine.UI.Controls.ViewModels;

namespace VendingMachine.UI.Controls.Views
{
    public class VendingMachinesStatesView : UserControl
    {
        public VendingMachinesStatesViewModel Model { get; set; }

        public VendingMachinesStatesView(VendingMachinesStatesViewModel model)
        {
            Model = model;

            var allStatesList = new ListBox
            {
                DataSource = Model.VendingMachinesStates,
                ValueMember = nameof(VendingMachineState.Name),
                DisplayMember = nameof(VendingMachineState.Name),
                Dock = DockStyle.Fill
            };
            allStatesList.DataBindings.Add(nameof(ListBox.SelectedItem), this, "Model.SelectedState");

            var openSelectedStateButton = new Button
            {
                Text = "Open",
                Dock = DockStyle.Bottom
            };
            openSelectedStateButton.Click += OpenSelectedStateButton_Click;

            Controls.Add(allStatesList);
            Controls.Add(openSelectedStateButton);
        }

        private void OpenSelectedStateButton_Click(object sender, System.EventArgs e)
        {
            Model.OpenSelectedState();
        }
    }
}
