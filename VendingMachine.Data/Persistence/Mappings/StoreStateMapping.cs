using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace VendingMachine.Data
{
    public class StoreStateMapping : ClassMapping<StoreState>
    {
        public StoreStateMapping()
        {
            Id(ss => ss.Id, m => m.Generator(Generators.Identity));
            Property(ss => ss.SlotsStates);
        }
    }
}
