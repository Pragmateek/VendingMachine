using System.Drawing;
using System.Windows.Forms;
using VendingMachine.Business.Contracts;
using VendingMachine.Business.Implementation;
using VendingMachine.Tools;
using VendingMachine.UI.Controls.ViewModels;

namespace VendingMachine.UI.Controls
{
    public class ControlPanelView : UserControl
    {
        public ControlPanelViewModel Model { get; set; }

        private readonly ComboBox coinsInput;

        public ControlPanelView(ControlPanelViewModel model)
        {
            Model = model;

            var insertedAmountView = new Label
            {
                Font = new Font(Label.DefaultFont, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                BorderStyle = BorderStyle.FixedSingle
            };
            insertedAmountView.DataBindings.Add("Text", this, "Model.InsertedAmountText");

            coinsInput = new ComboBox
            {
                DataSource = Model.ControlPanel.AcceptedCoinsTypes,
                Dock = DockStyle.Fill
            };
            coinsInput.SelectionChangeCommitted += CoinsInput_SelectionChangeCommitted;

            var refundInput = new Button
            {
                Dock = DockStyle.Fill
            };
            refundInput.DataBindings.Add("Enabled", this, "Model.ControlPanel.CanRefund");
            refundInput.Click += RefundInput_Click;

            var productsChoicesViewModel = new ProductsChoicesViewModel(Model.ControlPanel.ProductsChoices);
            productsChoicesViewModel.ChoiceMade += ProductsChoicesViewModel_ChoiceMade;

            var productsChoicesView = new ProductsChoicesView(productsChoicesViewModel)
            {
                Dock = DockStyle.Fill
            };

            var layout = new TableLayoutPanel
            {
                RowCount = 3,
                ColumnCount = 1,
                Dock = DockStyle.Fill
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 80));

            layout.Controls.Add(insertedAmountView, 0, 0);
            layout.Controls.Add(coinsInput, 0, 1);
            layout.Controls.Add(refundInput, 0, 2);
            layout.Controls.Add(productsChoicesView, 0, 3);

            Controls.Add(layout);
        }

        private void ProductsChoicesViewModel_ChoiceMade(object sender, ValueEventArg<IProductChoice> e)
        {
            IItem item;
            Model.ControlPanel.TryBuy(e.Value.CatalogEntry.Product, out item);
        }

        private void RefundInput_Click(object sender, System.EventArgs e)
        {
            Model.ControlPanel.Refund();
        }

        private void CoinsInput_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            if (coinsInput.SelectedItem == null)
            {
                return;
            }

            var selectedCoinType = (ICoinType)coinsInput.SelectedItem;
            var coin = CoinsFactory.Make(selectedCoinType);

            Model.ControlPanel.Insert(coin);

            coinsInput.SelectedItem = null;
        }
    }
}
