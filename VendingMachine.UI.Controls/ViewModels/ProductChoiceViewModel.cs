using System;
using System.ComponentModel;
using VendingMachine.Business.Contracts;

namespace VendingMachine.UI.Controls.ViewModels
{
    public class ProductChoiceViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public event EventHandler ChoiceMade = delegate { };

        private IProductChoice productChoice;
        public IProductChoice ProductChoice
        {
            get { return productChoice; }
            set
            {
                if (value != productChoice)
                {
                    productChoice = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(ProductChoice)));
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(ProductChoiceProductImagePath)));
                }
            }
        }

        public string ProductChoiceProductImagePath => $"./Images/Products/{ProductChoice.CatalogEntry.Product.Name.Replace(" ", "")}.png";

        public ProductChoiceViewModel(IProductChoice productChoice)
        {
            this.productChoice = productChoice;
        }

        public void MakeChoice()
        {
            ChoiceMade(this, EventArgs.Empty);
        }
    }
}
