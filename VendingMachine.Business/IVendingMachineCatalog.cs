﻿using System.Collections.Generic;

namespace VendingMachine.Business.Contracts
{
    public interface IVendingMachineCatalog : IEnumerable<IVendingMachineCatalogEntry>
    {
    }
}
