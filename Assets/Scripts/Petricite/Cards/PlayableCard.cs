using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Petricite
{
    /// <summary>
    /// Units, Gear, and Spells are playable cards
    /// </summary>
    public abstract class PlayableCard : Card, ICommand //The command is called when you play the card (move it from a playable zone to a board zone)
    {
        protected int energyCost;
        protected int powerCost;

        public static GameEvent<PlayableCard> OnCardPlayed = new();


        protected void InvokeOnCardPlayed(PlayableCard card) => OnCardPlayed?.Invoke(card);

        /// <summary>
        /// note that instantiating a card is playing the card?? for now at least
        /// </summary>
        public PlayableCard(Zone zone, Player controller, string name = "UNNAMED PLAYABLE CARD", string id = "NO_ID", int powerCost = 0, int energyCost = 0, Action<Card> initAction = null)
            : base(zone, controller, name, id, initAction)
        {
            this.energyCost = energyCost;
            this.powerCost = powerCost;
            this.controller = controller;

            Filter<PlayableCard>.OnFilter += OnFilter;

        }

        private void OnFilter(Filter<PlayableCard> filter)
        {
            if (filter.test(this))
            {
                filter.value.Add(this);
            }
        }

        public int GetEnergyCost()
        {
            var query = new Query<int, PlayableCard>(this, "ENERGY_COST", energyCost);

            Query<int, PlayableCard>.RaiseQuery(query);

            return query.value;
        }

        public int GetPowerCost()
        {
            var query = new Query<int, PlayableCard>(this, "POWER_COST", powerCost);

            Query<int, PlayableCard>.RaiseQuery(query);

            return query.value;
        }

        public abstract Task PreExecute();

        public abstract Task Execute();

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }

    }
}
