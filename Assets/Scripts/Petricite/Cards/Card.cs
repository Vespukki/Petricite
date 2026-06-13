using System;

namespace Petricite
{
    public class Card
    {
        public string name;
        private Zone zone;

        public static event Action<Card, Zone> OnZoneChange;

        public Card(Zone zone, string name = "UNNAMED CARD")
        {
            this.name = name;
            this.zone = zone;
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
