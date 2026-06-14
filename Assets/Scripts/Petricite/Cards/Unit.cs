using System;
using UnityEngine;

namespace Petricite
{
    public class Unit : Permanent
    {
        protected int might;
        
        public delegate void unitHandler(Unit unit);
        public static event unitHandler OnUnitPlayed;

        public Unit(Zone zone, Player controller, string name = "UNNAMED PLAYABLE CARD", int powerCost = 0, int energyCost = 0, int might = 0) : base(zone,controller, name, powerCost, energyCost)
        {
            this.might = might;
            Filter<Unit>.OnFilter += OnFilter;
            OnUnitPlayed?.Invoke(this);
        }

        public void OnFilter(Filter<Unit> filter)
        {
            if (filter.test(this))
            {
                filter.value.Add(this);
            }
        }

        public int GetMight()
        {
            var query = new Query<int, Card>(this, "MIGHT", might);

            Query<int, Card>.RaiseQuery(query);

            return query.value;
        }


        
    }
}
