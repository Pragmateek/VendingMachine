using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VendingMachine.UI.Client
{
    public class ConfigurationView : UserControl
    {
        public ConfigurationViewModel Model { get; set; }

        private readonly NumericUpDown storeSlotsCapacityInput;
        private readonly NumericUpDown cashRegisterSlotsCapacityInput;
        private readonly NumericUpDown initialBottlesCountInput;

        public ConfigurationView(ConfigurationViewModel model)
        {
            Model = model;

            var storeSlotsCapacityLabel = new Label
            {
                Text = "Store Slots",
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleRight
            };

            var cashRegisterSlotsCapacityLabel = new Label
            {
                Text = "Cash-Register Slots",
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleRight
            };

            storeSlotsCapacityInput = new NumericUpDown
            {
                Minimum = 0,
                Maximum = decimal.MaxValue,
                Increment = 1,
                Dock = DockStyle.Fill
            };
            storeSlotsCapacityInput.DataBindings.Add(nameof(NumericUpDown.Value), this, "Model.Configuration.StoreSlotsCapacity");

            cashRegisterSlotsCapacityInput = new NumericUpDown
            {
                Minimum = 0,
                Maximum = decimal.MaxValue,
                Increment = 1,
                Dock = DockStyle.Fill
            };
            cashRegisterSlotsCapacityInput.DataBindings.Add(nameof(NumericUpDown.Value), this, "Model.Configuration.CashRegisterSlotsCapacity");

            initialBottlesCountInput = new NumericUpDown
            {
                Minimum = 0,
                Increment = 1,
                Dock = DockStyle.Fill
            };
            initialBottlesCountInput.DataBindings.Add(nameof(NumericUpDown.Maximum), storeSlotsCapacityInput, "Value");

            var capacityLayout = new TableLayoutPanel
            {
                RowCount = 2,
                ColumnCount = 2,
                Dock= DockStyle.Fill
            };

            capacityLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            capacityLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            capacityLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            capacityLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            capacityLayout.Controls.Add(storeSlotsCapacityLabel, 0, 0);
            capacityLayout.Controls.Add(storeSlotsCapacityInput, 1, 0);
            capacityLayout.Controls.Add(cashRegisterSlotsCapacityLabel, 0, 1);
            capacityLayout.Controls.Add(cashRegisterSlotsCapacityInput, 1, 1);

            var capacitiesSection = new GroupBox
            {
                Text = "Capacities",
                Dock = DockStyle.Fill
            };

            capacitiesSection.Controls.Add(capacityLayout);

            var itemsSection = new GroupBox
            {
                Text = "Items",
                Dock = DockStyle.Bottom
            };

            itemsSection.Controls.Add(initialBottlesCountInput);

            var saveButton = new Button
            {
                Text = "Save",
                Dock = DockStyle.Bottom
            };
            saveButton.Click += SaveButton_Click;

            Controls.Add(capacitiesSection);
            Controls.Add(itemsSection);
            Controls.Add(saveButton);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Model.Save();
        }
    }
}
