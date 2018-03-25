using System;

namespace VendingMachine.Tools
{
    public class ValueEventArg<T> : EventArgs
    {
        public T Value { get; private set; }

        public ValueEventArg(T value)
        {
            Value = value;
        }
    }
}
