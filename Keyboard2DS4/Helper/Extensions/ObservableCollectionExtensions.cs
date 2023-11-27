using System;
using System.Collections.ObjectModel;

namespace Keyboard2DS4.Helper.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static void ForEach<T>(this ObservableCollection<T> collection, Action<T> action)
        {
            foreach (T item in collection)
            {
                action(item);
            }
        }
    }
}
