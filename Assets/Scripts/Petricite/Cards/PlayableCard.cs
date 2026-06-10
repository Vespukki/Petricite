namespace Petricite
{
    /// <summary>
    /// Units, Gear, and Spells are playable cards
    /// </summary>
    public class PlayableCard : Card
    {
        protected int energyCost;
        protected int powerCost;
        public PlayableCard(Zone zone, string name = "UNNAMED PLAYABLE CARD", int powerCost = 0, int energyCost = 0) : base(zone, name)
        {
            this.energyCost = energyCost;
            this.powerCost = powerCost;
        }

        public int GetEnergyCost()
        {
            var query = new Query<int, PlayableCard>(this, "ENERGY_COST", energyCost);

            GameEvents<int, PlayableCard>.RaiseQuery(query);

            return query.value;
        }

        public int GetPowerCost()
        {
            var query = new Query<int, PlayableCard>(this, "POWER_COST", powerCost);

            GameEvents<int, PlayableCard>.RaiseQuery(query);

            return query.value;
        }
    }
}
