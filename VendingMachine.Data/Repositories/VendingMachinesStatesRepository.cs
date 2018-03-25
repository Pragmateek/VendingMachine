using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using VendingMachine.Business.Contracts;
using VendingMachine.Business.Implementation;

namespace VendingMachine.Data
{
    public class VendingMachinesStatesRepository : IDisposable
    {
        private static ISessionFactory sessionFactory;

        public static string DatabasePath { get; private set; }

        static VendingMachinesStatesRepository()
        {
            var configuration = new Configuration();
            configuration.Configure();

            DatabasePath = Regex.Match(configuration.GetProperty("connection.connection_string"), "Data Source=([^;]+);").Groups[1].Value;

            var modelMapper = new ModelMapper();
            modelMapper.AddMapping<VendingMachineStateMapping>();
            modelMapper.AddMapping<StoreStateMapping>();
            modelMapper.AddMapping<StoreSlotStateMapping>();
            modelMapper.AddMapping<CashRegisterStateMapping>();
            modelMapper.AddMapping<CashRegisterSlotStateMapping>();
            modelMapper.AddMapping<ControlPanelStateMapping>();
            configuration.AddDeserializedMapping(modelMapper.CompileMappingForAllExplicitlyAddedEntities(), "VendingMachineModelMapping");

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
                vendingMachineState = session.CreateCriteria<VendingMachineState>().SetFetchMode("StoreState", FetchMode.Join).Add(Restrictions.IdEq(vendingMachineStateId)).UniqueResult<VendingMachineState>();
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
