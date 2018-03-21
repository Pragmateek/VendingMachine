using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VendingMachine.UI.Controls.ViewModels;

namespace VendingMachine.UI.Controls
{
    public class VendingMachineStoreSlotView : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private VendingMachineStoreSlotViewModel model;
        public VendingMachineStoreSlotViewModel Model
        {
            get { return model; }
            set
            {
                if (value != model)
                {
                    model = value;
                    model.Slot.ItemsChanged += Slot_ItemsChanged;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Model)));
                    RegenerateItemsViews();
                }
            }
        }

        private readonly TableLayoutPanel itemsPicturesLayout;

        private void RegenerateItemsViews()
        {
            foreach (PictureBox pictureBox in itemsPicturesLayout.Controls)
            {
                pictureBox.LoadCompleted -= ItemPicture_LoadCompleted;
            }

            itemsPicturesLayout.Controls.Clear();

            // Possible overflow
            itemsPicturesLayout.RowCount = (int)Model.Slot.Capacity;

            for (int i = 1; i <= Model.Slot.Capacity - Model.Slot.Count; i++)
            {
                var filler = new Label
                {
                    Text = "-",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    BorderStyle = BorderStyle.FixedSingle
                };

                itemsPicturesLayout.Controls.Add(filler);
            }

            var n = Model.Slot.Capacity - 1;
            foreach (var item in Model.Slot)
            {
                var itemPicture = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Dock = DockStyle.Fill,
                    BorderStyle = BorderStyle.FixedSingle
                };
                itemPicture.LoadCompleted += ItemPicture_LoadCompleted;
                itemPicture.DataBindings.Add("ImageLocation", this, "Model.SlotProductImagePath");

                // Possible overflow
                itemsPicturesLayout.Controls.Add(itemPicture, 0, (int)n);

                n -= 1;
            }

            itemsPicturesLayout.Controls.Add(new Control { Height = 0 }, 0, (int)Model.Slot.Capacity);
        }

        private void Slot_ItemsChanged(object sender, EventArgs e)
        {
            RegenerateItemsViews();
        }

        public VendingMachineStoreSlotView()
        {
            itemsPicturesLayout = new TableLayoutPanel
            {
                ColumnCount = 1,
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.LightCoral
            };

            var priceLabel = new Label
            {
                Font = new Font(Label.DefaultFont, FontStyle.Bold),
                Dock = DockStyle.Bottom,
                TextAlign = ContentAlignment.MiddleCenter,
                BorderStyle = BorderStyle.FixedSingle
            };
            priceLabel.DataBindings.Add("Text", this, "Model.ProductPriceText");

            var countCapacityLabel = new Label
            {
                Font = new Font(Label.DefaultFont, FontStyle.Bold),
                Dock = DockStyle.Bottom,
                TextAlign = ContentAlignment.MiddleCenter,
                BorderStyle = BorderStyle.FixedSingle
            };
            countCapacityLabel.DataBindings.Add("Text", this, "Model.CountCapacityText");

            Controls.Add(itemsPicturesLayout);
            Controls.Add(priceLabel);
            Controls.Add(countCapacityLabel);
        }

        private void ItemPicture_LoadCompleted(object sender, EventArgs e)
        {
            ((PictureBox)sender).Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
        }

        public VendingMachineStoreSlotView(VendingMachineStoreSlotViewModel model) : this()
        {
            Model = model;
        }
    }
}
