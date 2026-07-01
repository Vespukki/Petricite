using System;
using System.Collections.Generic;

namespace Petricite
{
    public class Card : IChoosable
    {
        public string name;
        public Player controller;
        public string id;
        public string Name => name;

        public static event Action<Card, Zone> OnZoneChange;
      
        private Zone zone;

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

        public Card(Zone zone, Player controller, string name = "UNNAMED CARD", string id = "NO_ID", Action<Card> initAction = null)
        {
            this.controller = controller;
            this.name = name;
            this.zone = zone;
            this.id = id;

            initAction ??= (_) => { };

            Filter<Card>.OnFilter += OnFilter;
            initAction(this);
        }

        private void OnFilter(Filter<Card> filter)
        {
            if (filter.test(this))
            {
                filter.value.Add(this);
            }
        }

      

    }

}
