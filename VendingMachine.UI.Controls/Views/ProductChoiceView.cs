using System;
using System.Drawing;
using System.Windows.Forms;
using VendingMachine.Business.Implementation;
using VendingMachine.UI.Controls.ViewModels;

namespace VendingMachine.UI.Controls
{
    public class ProductChoiceView : UserControl
    {
        public ProductChoiceViewModel Model { get; set; }

        public ProductChoiceView(ProductChoiceViewModel model)
        {
            Model = model;

            var productView = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0)
            };
            productView.LoadCompleted += productView_LoadCompleted;
            productView.DataBindings.Add("ImageLocation", this, "Model.ProductChoiceProductImagePath");

            var buyProductButton = new Button
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            buyProductButton.DataBindings.Add("Enabled", this, "Model.ProductChoice.IsPossible");

            //((ProductChoice)Model.ProductChoice).PropertyChanged += ProductChoice_PropertyChanged;

            var layout = new TableLayoutPanel
            {
                RowCount = 1,
                ColumnCount = 2,
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.LightGoldenrodYellow
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f * 2 / 3));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f * 1 / 3));

            layout.Controls.Add(productView, 0, 0);
            layout.Controls.Add(buyProductButton, 1, 0);

            Controls.Add(layout);
        }

        //private void ProductChoice_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //}

        private void productView_LoadCompleted(object sender, EventArgs e)
        {
            ((PictureBox)sender).Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
        }
    }
}
