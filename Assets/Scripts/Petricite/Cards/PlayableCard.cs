namespace Petricite
{
    /// <summary>
    /// Units, Gear, and Spells are playable cards
    /// </summary>
    public class PlayableCard : Card
    {
        protected int energyCost;
        protected int powerCost;
        public Player controller;

        public delegate void cardPlayedHandler(PlayableCard card);
        public static event cardPlayedHandler OnCardPlayed;

        /// <summary>
        /// note that instantiating a card is playing the card?? for now at least
        /// </summary>
        public PlayableCard(Zone zone, Player controller, string name = "UNNAMED PLAYABLE CARD", int powerCost = 0, int energyCost = 0) : base(zone, name)
        {
            this.energyCost = energyCost;
            this.powerCost = powerCost;
            this.controller = controller;

            OnCardPlayed?.Invoke(this);
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
    }
}
