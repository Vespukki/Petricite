using System;

namespace Petricite
{
    public class Permanent : PlayableCard, IReadyable
    {
        protected bool ready = true;

        public static event Action<Permanent,bool> OnReadyChange;

        public bool Ready
        {
            get => ready;
            set 
            {
                OnReadyChange?.Invoke(this, value);
                ready = value;
            } 
            
        }

        
        public Permanent(Zone zone, Player controller, string name = "UNNAMED PLAYABLE CARD", int powerCost = 0, int energyCost = 0) : base(zone, controller, name, powerCost, energyCost)
        {
        }

    }
}
