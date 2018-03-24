using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace VendingMachine.Data
{
    public class CashRegisterSlotStateMapping : ClassMapping<CashRegisterSlotState>
    {
        public CashRegisterSlotStateMapping()
        {
            Id(crs => crs.Id, m => m.Generator(Generators.Identity));
            Property(crs => crs.CoinTypeName);
            Property(crs => crs.Capacity);
            Property(crs => crs.Count);
        }
    }
}
