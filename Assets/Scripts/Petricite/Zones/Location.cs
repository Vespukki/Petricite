using UnityEngine;

namespace Petricite
{
    public class Location : Zone
    {
        public Location(string name, int maxCards, Player owner) : base(name, maxCards, owner, true)
        {
            Filter<Location>.OnFilter += OnFilter;
        }

        public void OnFilter(Filter<Location> filter)
        {
            if (filter.test(this))
            {
                filter.value.Add(this);
            }
        }
    }
}
