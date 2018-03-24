using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace VendingMachine.Data
{
    public class StoreSlotStateMapping : ClassMapping<StoreSlotState>
    {
        public StoreSlotStateMapping()
        {
            Id(sss => sss.Id, m => m.Generator(Generators.Identity));
            Property(sss => sss.CatalogProductName);
            Property(sss => sss.Capacity);
            Property(sss => sss.Count);
        }
    }
}
