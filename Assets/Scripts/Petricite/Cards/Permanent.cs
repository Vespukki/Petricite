using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace Petricite
{
    public class Permanent : PlayableCard, IReadyable
    {
        protected bool ready;
        public bool Ready
        {
            get => ready; 
            set => ready = value;
            
        }

        
        public Permanent(Zone zone, Player controller, string name = "UNNAMED PLAYABLE CARD", int powerCost = 0, int energyCost = 0) : base(zone, controller, name, powerCost, energyCost)
        {
        }

    }
}
