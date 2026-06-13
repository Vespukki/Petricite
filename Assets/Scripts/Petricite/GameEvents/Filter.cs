using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Petricite
{
    public class Filter<T>
    {
        public delegate void filterDelegate(Filter<T> filter);
        public delegate bool testDelegate(T item);

        public static event filterDelegate OnFilter;

        public testDelegate test;

        public List<T> value;

        public Filter(testDelegate test)
        {
            this.test = test;
            this.value = new List<T>();
        }

        public List<T> GetValid()
        {
            value.Clear();

            OnFilter?.Invoke(this);

            return value;
        }
    }
}
