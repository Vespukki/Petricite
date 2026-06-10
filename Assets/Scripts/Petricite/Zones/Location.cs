using UnityEngine;

namespace Petricite
{
    public class Location : Zone
    {
        public Location(string name, int maxCards) : base(name, maxCards)
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

        public override bool CanAcceptCard(Card card)
        {
            return true;
        }
    }
}
