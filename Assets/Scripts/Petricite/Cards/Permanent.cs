using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Petricite
{
    public class Permanent : PlayableCard, IReadyable
    {
        protected bool ready = true;

        private List<Ability> abilities = new();
        public List<Ability> Abilities => abilities;

        public static event Action<Permanent,bool> OnReadyChange;
        private Filter<Location> playableLocationFilter;
        private FilterChoiceCommand<Location> locationChoice;
        public bool Ready
        {
            get => ready;
            set 
            {
                OnReadyChange?.Invoke(this, value);
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

        
        public Permanent(Zone zone, Player controller, string name = "UNNAMED PLAYABLE CARD", int powerCost = 0, int energyCost = 0,
            List<Ability> abilities = null) : base(zone, controller, name, powerCost, energyCost)
        {
            abilities ??= new();

            foreach (var ability in abilities)
            {
                ability.source = this;
            }
            this.abilities = abilities;
        }

    }
}
