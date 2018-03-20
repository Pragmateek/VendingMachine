namespace VendingMachine.Business
{
    public interface ICoinType
    {
        string Name { get; }
        ICurrency Currency { get; }
        decimal FaceValue { get; }
    }
}
