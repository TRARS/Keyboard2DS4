using System.Collections.Generic;
using System.Linq;

namespace Keyboard2DS4.Helper.Extensions
{
    public static class ListExtensions
    {
        public static List<T> ReArrange<T>(this List<T> collection, int[] orderArray)
        {
            List<T> reorderedList = collection.Zip(orderArray, (value, index) => new { value, index })
                                              .OrderBy(pair => pair.index)
                                              .Select(pair => pair.value)
                                              .ToList();
            return reorderedList;
        }
    }
}
