using System;
using System.Collections.Generic;

namespace Petricite
{
    public class Card : IChoosable
    {
        public string name;
        public Player controller;
        public string Name => name;

        private Zone zone;

        public static event Action<Card, Zone> OnZoneChange;

        public Card(Zone zone, string name = "UNNAMED CARD")
        {

            this.name = name;
            this.zone = zone;

            Filter<Card>.OnFilter += OnFilter;
        }

        private void OnFilter(Filter<Card> filter)
        {
            if (filter.test(this))
            {
                filter.value.Add(this);
            }
        }

        public Zone Zone
        {
            get
            {
                return zone;
            }
            set
            {
                zone = value;
                OnZoneChange?.Invoke(this, value);
            }
        }

    }

}
