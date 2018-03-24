using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace VendingMachine.Data
{
    public class VendingMachineStateMapping : ClassMapping<VendingMachineState>
    {
        public VendingMachineStateMapping()
        {
            Id(vms => vms.Id, m => m.Generator(Generators.Identity));
            Property(vms => vms.AcceptedCoinsTypesNames);
            OneToOne(vms => vms.StoreState, m => { });
            OneToOne(vms => vms.CashRegisterState, m => { });
            OneToOne(vms => vms.ControlPanelState, m => { });
        }
    }
}
