using System;
using System.ComponentModel;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class ProductChoice : IProductChoice, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private readonly ICatalogEntry catalogEntry;
        public ICatalogEntry CatalogEntry => catalogEntry;

        private bool isPossible;
        public bool IsPossible
        {
            get { return isPossible; }
            set
            {
                if (value != isPossible)
                {
                    isPossible = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(IsPossible)));
                }
            }
        }

        public ProductChoice(ICatalogEntry catalogEntry)
        {
            this.catalogEntry = catalogEntry;
        }

        public override bool Equals(object obj)
        {
            var otherProductChoice = obj as ProductChoice;

            return otherProductChoice != null &&
                Equals(otherProductChoice.CatalogEntry, CatalogEntry) &&
                otherProductChoice.IsPossible == IsPossible;
        }
    }
}
