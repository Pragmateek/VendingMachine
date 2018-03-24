using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace VendingMachine.Data
{
    public class ControlPanelStateMapping : ClassMapping<ControlPanelState>
    {
        public ControlPanelStateMapping()
        {
            Id(cps => cps.Id, m => m.Generator(Generators.Identity));
            Property(cps => cps.InsertedCoinsTypesNames);
        }
    }
}
