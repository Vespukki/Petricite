using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Petricite
{
    public class Filter<T>
    {
        public static event Action<Filter<T>> OnFilter;

        public Func<T, bool> test;

        public List<T> value;

        public Filter(Func<T, bool> test)
        {
            this.test = test;
            this.value = new List<T>();
        }

        public List<T> GetValid()
        {
            value.Clear();

            OnFilter?.Invoke(this);

            return value.Cast<T>().ToList();
        }
    }
}
