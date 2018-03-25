using System;
using System.Linq;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VendingMachine.UI.Controls.ViewModels;

namespace VendingMachine.UI.Controls
{
    public class VendingMachineStoreSlotView : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private StoreSlotViewModel model;
        public StoreSlotViewModel Model
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
            foreach (var pictureBox in itemsPicturesLayout.Controls.OfType<PictureBox>())
            {
                pictureBox.LoadCompleted -= ItemPicture_LoadCompleted;
            }

            itemsPicturesLayout.Controls.Clear();
            itemsPicturesLayout.RowStyles.Clear();

            // Possible overflow
            itemsPicturesLayout.RowCount = (int)Model.Slot.Capacity;

            var rowsHeight = 100f / itemsPicturesLayout.RowCount;

            for (int i = 1; i <= Model.Slot.Capacity; i++)
            {
                var rowsStyle = new RowStyle(SizeType.Percent, rowsHeight);
                itemsPicturesLayout.RowStyles.Add(rowsStyle);
            }

            for (int i = 1; i <= Model.Slot.Capacity - Model.Slot.Count; i++)
            {
                var filler = new Label
                {
                    //Text = "-",
                    //TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0),
                    //BorderStyle = BorderStyle.FixedSingle
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
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(0)
                };
                itemPicture.LoadCompleted += ItemPicture_LoadCompleted;
                itemPicture.DataBindings.Add("ImageLocation", this, "Model.SlotProductImagePath");

                // Possible overflow
                itemsPicturesLayout.Controls.Add(itemPicture, 0, (int)n);

                n -= 1;
            }

            //itemsPicturesLayout.Controls.Add(new Control { Height = 0 }, 0, (int)Model.Slot.Capacity);
        }

        private void Slot_ItemsChanged(object sender, EventArgs e)
        {
            RegenerateItemsViews();
        }

        private Label GetTexView(string targetPropertyPath)
        {
            var textView = new Label
            {
                Font = new Font(Label.DefaultFont, FontStyle.Bold),
                Dock = DockStyle.Bottom,
                TextAlign = ContentAlignment.MiddleCenter,
                BorderStyle = BorderStyle.FixedSingle
            };
            textView.DataBindings.Add("Text", this, targetPropertyPath);

            return textView;
        }

        public VendingMachineStoreSlotView()
        {
            itemsPicturesLayout = new TableLayoutPanel
            {
                ColumnCount = 1,
                //AutoSize = true,
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                //BackColor = Color.LightCoral
            };

            var productNameView = GetTexView("Model.ProductNameText");
            var priceView = GetTexView("Model.ProductPriceText");
            var countCapacityView = GetTexView("Model.CountCapacityText");

            Controls.Add(itemsPicturesLayout);
            Controls.Add(productNameView);
            Controls.Add(priceView);
            Controls.Add(countCapacityView);
        }

        private void ItemPicture_LoadCompleted(object sender, EventArgs e)
        {
            ((PictureBox)sender).Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
        }

        public VendingMachineStoreSlotView(StoreSlotViewModel model) : this()
        {
            Model = model;
        }
    }
}
