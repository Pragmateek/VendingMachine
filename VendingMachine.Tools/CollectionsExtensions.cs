using System.Collections.Generic;
using System.Linq;

namespace VendingMachine.Tools
{
    public static class CollectionsExtensions
    {
        public static int GetElementsHashCode<T>(this IEnumerable<T> collection)
        {
            return collection.Aggregate(0, (hashCode, entry) => hashCode ^ entry.GetHashCode());
        }
    }
}
