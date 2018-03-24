using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace VendingMachine.Data
{
    public class _VendingMachineStateMapping : ClassMapping<VendingMachineState>
    {
        public _VendingMachineStateMapping()
        {
            // string s = nameof(StoreState.VendingMachineState);

            Id(vms => vms.Id, m => m.Generator(Generators.Identity));
            Property(vms => vms.AcceptedCoinsTypesNames);
            OneToOne(vms => vms.StoreState, m =>
            {
                m.Lazy(LazyRelation.NoLazy);
                //m.ForeignKey("VendingMachineState_Id");
                //m.PropertyReference(typeof(StoreState).GetProperty(s));
                m.Cascade(Cascade.All);
                //m.ForeignKey("VendingMachine_Id");
            });
            OneToOne(vms => vms.CashRegisterState, m => { });
            OneToOne(vms => vms.ControlPanelState, m => { });
        }
    }
}
