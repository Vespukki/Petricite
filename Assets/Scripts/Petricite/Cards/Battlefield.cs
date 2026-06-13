using System;
using UnityEngine;

namespace Petricite
{
    public class Battlefield : IChoosable
    {
        private string name;
        public string ChoiceName => name;

        public Battlefield(string name)
        {
            this.name = name;

            Filter<Battlefield>.OnFilter += OnFilter;
        }

        private void OnFilter(Filter<Battlefield> filter)
        {
            if (filter.test(this))
            {
                filter.value.Add(this);
            }
        }
    }
}
