using System;

namespace VendingMachine.UI.Client
{
    public class ConfigurationViewModel
    {
        public event EventHandler ConfigurationSaved = delegate { };
        public event EventHandler EditionCancelled = delegate { };

        private Configuration sourceConfiguration;
        public Configuration Configuration { get; private set; }

        public ConfigurationViewModel(Configuration configuration)
        {
            sourceConfiguration = configuration;

            Configuration = new Configuration
            {
                StoreSlotsCapacity = sourceConfiguration.StoreSlotsCapacity,
                CashRegisterSlotsCapacity = sourceConfiguration.CashRegisterSlotsCapacity,
                InitialBottleCount = sourceConfiguration.InitialBottleCount,
                DatabasePath = sourceConfiguration.DatabasePath
            };
        }

        public void Save()
        {
            sourceConfiguration.StoreSlotsCapacity = Configuration.StoreSlotsCapacity;
            sourceConfiguration.CashRegisterSlotsCapacity = Configuration.CashRegisterSlotsCapacity;
            sourceConfiguration.InitialBottleCount = Configuration.InitialBottleCount;

            ConfigurationSaved(this, EventArgs.Empty);
        }
    }
}
