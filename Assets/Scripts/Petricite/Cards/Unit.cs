using System;
using System.Collections.Generic;
using UnityEngine;

namespace Petricite
{
    public class Unit : Permanent
    {
        protected int might;
        
        public delegate void unitHandler(Unit unit);
        public static event unitHandler OnUnitCreated;

        public Unit(Zone zone, Player controller, string name = "UNNAMED UNIT", string id = "NO_ID", int powerCost = 0, int energyCost = 0,
            int might = 0, Action<Card> initAction = null) : base(zone,controller, name, id, powerCost, energyCost, initAction)
        {
            this.might = might;
            Filter<Unit>.OnFilter += OnFilter;
            OnUnitCreated?.Invoke(this);
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
