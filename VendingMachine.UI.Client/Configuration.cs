using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.UI.Client
{
    public class Configuration
    {
        public uint StoreSlotsCapacity { get; set; }
        public uint CashRegisterSlotsCapacity { get; set; }
        public uint InitialBottleCount { get; set; }
    }
}
