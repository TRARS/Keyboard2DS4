using System;
using System.Collections.Generic;
using System.Threading;

namespace Keyboard2DS4.Helper
{
    public partial class CounterAccessor
    {
        private static readonly Lazy<CounterAccessor> lazyObject = new(() => new CounterAccessor());
        public static CounterAccessor Instance => lazyObject.Value;
    }

    public partial class CounterAccessor
    {
        public sealed class InternalCounter
        {
            private int count = 1;

            public void Increment()
            {
                Interlocked.Increment(ref count);
            }

            public void Decrement()
            {
                if (count > 0)
                {
                    Interlocked.Decrement(ref count);
                }
            }

            public int GetCount()
            {
                return count;
            }
        }
    }

    public partial class CounterAccessor
    {
        private readonly Dictionary<string, InternalCounter> internalList = new();

        public InternalCounter this[string key]
        {
            get
            {
                if (internalList.ContainsKey(key) is false)
                {
                    internalList.Add(key, new());
                }
                return internalList[key];
            }
        }
    }
}
