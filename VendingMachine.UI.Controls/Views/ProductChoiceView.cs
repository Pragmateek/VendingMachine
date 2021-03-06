﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
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
            // Binding not working as expected here !
            // Fallback to manual update.
            // var binding = buyProductButton.DataBindings.Add("Enabled", this, "Model.ProductChoice.IsPossible");
            // binding.Format += (s, e) => logger.Debug($"Formatting value {e.Value} from product choice possibility.");
            buyProductButton.Enabled = Model.ProductChoice.IsPossible;
            ((INotifyPropertyChanged)Model.ProductChoice).PropertyChanged += (_1, _2) => buyProductButton.Enabled = Model.ProductChoice.IsPossible;

            buyProductButton.DataBindings.Add("Text", this, "Model.ProductChoice.CatalogEntry.Price");
            buyProductButton.Click += BuyProductButton_Click;

            var layout = new TableLayoutPanel
            {
                RowCount = 1,
                ColumnCount = 2,
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                //BackColor = Color.LightGoldenrodYellow
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f * 2 / 3));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f * 1 / 3));

            layout.Controls.Add(productView, 0, 0);
            layout.Controls.Add(buyProductButton, 1, 0);

            Controls.Add(layout);
        }

        private void BuyProductButton_Click(object sender, EventArgs e)
        {
            Model.MakeChoice();
        }

        private void productView_LoadCompleted(object sender, EventArgs e)
        {
            ((PictureBox)sender).Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
        }
    }
}
