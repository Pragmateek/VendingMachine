using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using VendingMachine.UI.Controls.ViewModels;

namespace VendingMachine.UI.Controls
{
    public class ProductsChoicesView : UserControl
    {
        public ProductsChoicesViewModel Model { get; set; }

        public ProductsChoicesView(ProductsChoicesViewModel model)
        {
            Model = model;

            var n = Model.ProductsChoices.Count();

            var layout = new TableLayoutPanel
            {
                RowCount = n,
                ColumnCount = 1,
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.AliceBlue
            };

            var rowHeight = 100f / n;
            var row = 0;
            foreach (var productChoice in Model.ProductsChoices)
            {
                layout.RowStyles.Add(new RowStyle(SizeType.Percent, rowHeight));

                var productChoiceViewModel = new ProductChoiceViewModel(productChoice);
                var productChoiceView = new ProductChoiceView(productChoiceViewModel)
                {
                    Dock = DockStyle.Fill
                };
                layout.Controls.Add(productChoiceView, 0, row);

                row += 1;
            }

            Controls.Add(layout);
        }
    }
}
