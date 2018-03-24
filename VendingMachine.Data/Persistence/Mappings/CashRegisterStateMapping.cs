using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace VendingMachine.Data
{
    public class CashRegisterStateMapping : ClassMapping<CashRegisterState>
    {
        public CashRegisterStateMapping()
        {
            Id(cr => cr.Id, m => m.Generator(Generators.Identity));
            //ManyToOne(cr => cr.SlotsStates);
        }
    }
}
