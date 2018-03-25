using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace VendingMachine.Data
{
    public class VendingMachineStateMapping : ClassMapping<VendingMachineState>
    {
        public VendingMachineStateMapping()
        {
            Id(vms => vms.Id, m => m.Generator(Generators.Identity));
            Property(vms => vms.Name);
            Property(vms => vms.CatalogState);
            Property(vms => vms.AcceptedCoinsTypesNames);
            Property(vms => vms.StoreSlotsCapacity);
            Property(vms => vms.CashRegisterSlotsCapacity);
            Property(vms => vms.StoreState);
            Property(vms => vms.CashRegisterState);
            Property(vms => vms.ControlPanelState);
        }
    }
}
