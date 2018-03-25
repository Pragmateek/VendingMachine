using System;
using System.Drawing;
using System.Windows.Forms;

namespace VendingMachine.UI.Client
{
    public class ConfigurationView : UserControl
    {
        public ConfigurationViewModel Model { get; set; }

        //private readonly NumericUpDown storeSlotsCapacityInput;
        //private readonly NumericUpDown cashRegisterSlotsCapacityInput;
        //private readonly NumericUpDown initialBottlesCountInput;

        private GroupBox MakeCapacitiesSection()
        {
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

            var storeSlotsCapacityInput = new NumericUpDown
            {
                Minimum = 0,
                Maximum = decimal.MaxValue,
                Increment = 1,
                Dock = DockStyle.Fill
            };
            storeSlotsCapacityInput.DataBindings.Add(nameof(NumericUpDown.Value), this, "Model.Configuration.StoreSlotsCapacity");

            var cashRegisterSlotsCapacityInput = new NumericUpDown
            {
                Minimum = 0,
                Maximum = decimal.MaxValue,
                Increment = 1,
                Dock = DockStyle.Fill
            };
            cashRegisterSlotsCapacityInput.DataBindings.Add(nameof(NumericUpDown.Value), this, "Model.Configuration.CashRegisterSlotsCapacity");

            var capacityLayout = new TableLayoutPanel
            {
                RowCount = 2,
                ColumnCount = 2,
                AutoSize = true,
                Dock = DockStyle.Fill
            };

            //capacityLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            //capacityLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            capacityLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            capacityLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            capacityLayout.Controls.Add(storeSlotsCapacityLabel, 0, 0);
            capacityLayout.Controls.Add(storeSlotsCapacityInput, 1, 0);
            capacityLayout.Controls.Add(cashRegisterSlotsCapacityLabel, 0, 1);
            capacityLayout.Controls.Add(cashRegisterSlotsCapacityInput, 1, 1);

            var capacitiesSection = new GroupBox
            {
                Text = "Capacities",
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Fill
            };

            capacitiesSection.Controls.Add(capacityLayout);

            return capacitiesSection;
        }

        private GroupBox MakeItemsSection()
        {
            var initialBottlesCountLabel = new Label
            {
                Text = "Initial Bottles Count",
                AutoSize = true,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight
            };
            var initialBottlesCountInput = new NumericUpDown
            {
                Minimum = 0,
                Increment = 1,
                AutoSize = true,
                Dock = DockStyle.Fill
            };
            initialBottlesCountInput.DataBindings.Add(nameof(NumericUpDown.Maximum), this, "Model.Configuration.StoreSlotsCapacity");

            var itemsLayout = new TableLayoutPanel
            {
                RowCount = 1,
                ColumnCount = 2,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Fill
            };

            itemsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            itemsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            itemsLayout.Controls.Add(initialBottlesCountLabel, 0, 0);
            itemsLayout.Controls.Add(initialBottlesCountInput, 1, 0);

            var itemsSection = new GroupBox
            {
                Text = "Items",
                Dock = DockStyle.Fill,
                AutoSize = true
            };

            itemsSection.Controls.Add(itemsLayout);

            return itemsSection;
        }

        private GroupBox MakeDatabaseSection()
        {
            var databasePathLabel = new Label
            {
                Text = "Path",
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Fill
            };
            var databasePathView = new Label
            {
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Fill
            };
            databasePathView.Font = new Font(databasePathView.Font, FontStyle.Bold | FontStyle.Underline);
            databasePathView.DataBindings.Add(nameof(Label.Text), this, "Model.Configuration.DatabasePath");

            var layout = new TableLayoutPanel
            {
                RowCount = 1,
                ColumnCount = 2,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Fill
            };

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            layout.Controls.Add(databasePathLabel, 0, 0);
            layout.Controls.Add(databasePathView, 1, 0);

            var databaseSection = new GroupBox
            {
                Text = "Database",
                AutoSize = true,
                Dock = DockStyle.Fill
            };

            databaseSection.Controls.Add(layout);

            return databaseSection;
        }

        public ConfigurationView(ConfigurationViewModel model)
        {
            Model = model;

            var capacitiesSection = MakeCapacitiesSection();
            var itemsSection = MakeItemsSection();
            var databaseSection = MakeDatabaseSection();

            var saveButton = new Button
            {
                Text = "Save",
                AutoSize = true,
                Dock = DockStyle.Fill
            };
            saveButton.Click += SaveButton_Click;

            var layout = new TableLayoutPanel
            {
                RowCount = 4,
                AutoSize = true,
                Dock = DockStyle.Fill
            };
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            layout.Controls.Add(capacitiesSection);
            layout.Controls.Add(itemsSection);
            layout.Controls.Add(databaseSection);
            layout.Controls.Add(saveButton);

            Controls.Add(layout);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Model.Save();
        }
    }
}
