using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace VendingMachine.Data
{
    public class StoreStateMapping : ClassMapping<StoreState>
    {
        public StoreStateMapping()
        {
            Id(ss => ss.Id, m => m.Generator(Generators.Identity));
            ManyToOne(x => x.VendingMachineState, x => { x.Column("VendingMachineState_Id"); x.NotNullable(true); });
            // Bag(ss => ss.SlotsStates, m => { m.Key(km => { km.Column("StoreState_Id"); km.ForeignKey("StoreState_Id"); }); m.Inverse(true); }, a => a.OneToMany());
            //Set(ss => ss.SlotsStates, m => { m.Key(km => { km.Column("StoreState_Id"); km.ForeignKey("StoreState_Id"); }); m.Inverse(true); }, a => a.OneToMany());
            Bag(ss => ss.SlotsStates, m => m.Cascade(Cascade.All), a => a.OneToMany());
        }
    }
}
