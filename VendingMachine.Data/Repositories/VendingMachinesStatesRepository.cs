using log4net;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Text.RegularExpressions;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Data
{
    public class VendingMachinesStatesRepository : IDisposable
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(VendingMachinesStatesRepository));

        private static ISessionFactory sessionFactory;

        public static string DatabasePath { get; private set; }

        private static void EnsureDBIsSetup()
        {
            if (File.Exists(DatabasePath))
            {
                logger.Info($"Database '{DatabasePath}' already exists.");

                return;
            }

            logger.Info($"Setuping database '{DatabasePath}'.");

            using (var connection = new SQLiteConnection($"Data Source={DatabasePath};Version=3;"))
            {
                connection.Open();

                using (var createSchemaCommand = connection.CreateCommand())
                {
                    createSchemaCommand.CommandText = File.ReadAllText("./Persistence/CreateModelSchema.sql");

                    logger.Info($"Creating database '{DatabasePath}' schema from SQL script.");

                    createSchemaCommand.ExecuteNonQuery();
                }
            }

            logger.Info($"Database '{DatabasePath}' has been successfully setup.");
        }

        public static void ResetDatabase()
        {
            logger.Info($"Reseting database '{DatabasePath}'.");

            File.Delete(DatabasePath);

            EnsureDBIsSetup();
        }

        static VendingMachinesStatesRepository()
        {
            logger.Info($"Configuring ORM.");

            var configuration = new Configuration();
            configuration.Configure();

            DatabasePath = Regex.Match(configuration.GetProperty("connection.connection_string"), "Data Source=([^;]+);").Groups[1].Value;

            EnsureDBIsSetup();

            var modelMapper = new ModelMapper();
            modelMapper.AddMapping<VendingMachineStateMapping>();
            modelMapper.AddMapping<StoreStateMapping>();
            modelMapper.AddMapping<StoreSlotStateMapping>();
            modelMapper.AddMapping<CashRegisterStateMapping>();
            modelMapper.AddMapping<CashRegisterSlotStateMapping>();
            modelMapper.AddMapping<ControlPanelStateMapping>();
            configuration.AddDeserializedMapping(modelMapper.CompileMappingForAllExplicitlyAddedEntities(), "VendingMachineModelMapping");

            sessionFactory = configuration.BuildSessionFactory();

            logger.Info($"Configuration of ORM done.");
        }

        public void Save(VendingMachineState vendingMachineState)
        {
            using (var session = sessionFactory.OpenSession())
            {
                session.SaveOrUpdate(vendingMachineState);
                session.Flush();
            }
        }

        public int SaveStateOf(IVendingMachine vendingMachine, string name = null, int id = 0)
        {
            var state = new VendingMachineState(vendingMachine, name, id);

            Save(state);

            return state.Id;
        }

        public IEnumerable<VendingMachineState> GetAllStates()
        {
            using (var session = sessionFactory.OpenSession())
            {
                return session
                    .CreateCriteria<VendingMachineState>()
                    .List<VendingMachineState>();
            }
        }

        public VendingMachineState GetStateById(int vendingMachineStateId)
        {
            VendingMachineState vendingMachineState;

            using (var session = sessionFactory.OpenSession())
            {
                vendingMachineState = session
                    .CreateCriteria<VendingMachineState>()
                    .SetFetchMode("StoreState", FetchMode.Join)
                    .Add(Restrictions.IdEq(vendingMachineStateId))
                    .UniqueResult<VendingMachineState>();
            }

            return vendingMachineState;
        }

        public static IVendingMachine GetVendingMachineFromState(VendingMachineState vendingMachineState)
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
