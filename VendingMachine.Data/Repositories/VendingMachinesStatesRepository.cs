using NHibernate;
using NHibernate.Cfg;
using System;
using VendingMachine.Business.Contracts;
using VendingMachine.Business.Implementation;

namespace VendingMachine.Data
{
    public class VendingMachinesStatesRepository : IDisposable
    {
        private ISessionFactory sessionFactory;

        public VendingMachinesStatesRepository()
        {
            var configuration = new Configuration();
            configuration.Configure();

            sessionFactory = configuration.BuildSessionFactory();
        }

        public int Save(VendingMachineState vendingMachineState)
        {
            int id;

            using (var session = sessionFactory.OpenSession())
            {
                id = (int)session.Save(vendingMachineState);
            }

            return id;
        }

        public int SaveStateOf(IVendingMachine vendingMachine)
        {
            var state = new VendingMachineState(vendingMachine);

            return Save(state);
        }

        public VendingMachineState GetStateById(int vendingMachineStateId)
        {
            VendingMachineState vendingMachineState;

            using (var session = sessionFactory.OpenSession())
            {
                vendingMachineState = session.Get<VendingMachineState>(vendingMachineStateId);
            }

            return vendingMachineState;
        }

        public IVendingMachine GetVendingMachineFromState(VendingMachineState vendingMachineState)
        {
            var vendingMachine = vendingMachineState.AsVendingMachine();

            return vendingMachine;
        }

        private bool hasBeenDisposed;
        public void Dispose(bool isAnExplicitCall)
        {
            if (isAnExplicitCall && !hasBeenDisposed)
            {
                sessionFactory.Dispose();
                hasBeenDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }


        ~VendingMachinesStatesRepository()
        {
            Dispose(false);
        }
    }
}
