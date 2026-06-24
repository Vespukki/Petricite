using UnityEngine;

namespace Petricite
{
    public class Hand : Zone
    {
        public Hand(string name, int maxCards, Player owner) : base(name, maxCards, owner)
        {
            Filter<Hand>.OnFilter += OnFilter;
        }

        public void OnFilter(Filter<Hand> filter)
        {
            if (filter.test(this))
            {
                filter.value.Add(this);
            }
        }
    }
}
