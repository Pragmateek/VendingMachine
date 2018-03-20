namespace VendingMachine.Business.Implementation
{
    public class Currency : ICurrency
    {
        public string Name { get; private set; }

        public Currency(string name)
        {
            Name = name;
        }
    }
}
