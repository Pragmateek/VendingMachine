using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Business.Contracts
{
    public interface IProductChoice
    {
        IVendingMachineCatalogEntry CatalogEntry { get; }
        bool IsPossible { get; }
    }
}
