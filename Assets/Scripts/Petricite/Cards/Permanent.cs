using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Petricite
{
    public class Permanent : PlayableCard, IReadyable
    {
        protected bool ready = true;

        public static event Action<Permanent,bool> OnReadyChange;
        private Filter<Location> playableLocationFilter;
        private FilterChoiceCommand<Location> locationChoice;
        public bool Ready
        {
            get => ready;
            set 
            {
                IReadyable.InvokeOnReady(this, value);
                ready = value;
            } 
            
        }

        public override async Task PreExecute()
        {
            playableLocationFilter = new((loc) => loc.MaxCards > loc.CardCount && loc.owner == controller);
            locationChoice = new(controller, playableLocationFilter, "Where to play unit");

            await locationChoice.Execute();
        }

        public override Task Execute()
        {
            Zone = locationChoice.result;

            InvokeOnCardPlayed(this);
            return Task.CompletedTask;
        }

        
        public Permanent(Zone zone, Player controller, string name = "UNNAMED PLAYABLE CARD", string id = "NO_ID", int powerCost = 0, int energyCost = 0,
            Action<Card> initAction = null) : base(zone, controller, name, id, powerCost, energyCost, initAction)
        {
        }

    }
}
