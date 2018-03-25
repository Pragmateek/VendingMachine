using System;
using System.Collections.Generic;
using System.ComponentModel;
using VendingMachine.Business.Contracts;
using VendingMachine.Tools;

namespace VendingMachine.UI.Controls.ViewModels
{
    public class ProductsChoicesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public event EventHandler<ValueEventArgs<IProductChoice>> ChoiceMade = delegate { };

        private IEnumerable<IProductChoice> productsChoices;
        public IEnumerable<IProductChoice> ProductsChoices
        {
            get { return productsChoices; }
            set
            {
                if (value != productsChoices)
                {
                    productsChoices = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(ProductsChoices)));
                }
            }
        }

        public ProductsChoicesViewModel(IEnumerable<IProductChoice> productsChoices)
        {
            this.productsChoices = productsChoices;
        }

        public void MakeChoice(IProductChoice productChoice)
        {
            ChoiceMade(this, new ValueEventArgs<IProductChoice>(productChoice));
        }
    }
}
