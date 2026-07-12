using System;
using UnityEngine;

namespace Petricite
{
    public class Rune : Card, IReadyable
    {
        public Rune(Zone zone, Player controller, string name = "UNNAMED CARD", string id = "NO_ID", Action<Card> initAction = null) : base(zone, controller, name, id, initAction)
        {
        }

        private bool ready = true;
        public bool Ready { get { return ready; } 
            set 
            {
                IReadyable.InvokeOnReady(this, value);
                ready = value; 
            } 
        }
    }
}
