using UnityEngine;

namespace Petricite
{
    public class Unit : PlayableCard
    {
        protected int might;
        public Unit(Zone zone, string name = "UNNAMED PLAYABLE CARD", int powerCost = 0, int energyCost = 0, int might = 0) : base(zone, name, powerCost, energyCost)
        {
            this.might = might;
        }

        public int GetMight()
        {
            var query = new Query<int, Card>(this, "MIGHT", might);

            GameEvents<int, Card>.RaiseQuery(query);

            return query.value;
        }


        public void StandardMove()
        {
            Filter<Location> filter = new((loc) => loc.MaxCards > loc.CardCount && loc != Zone);
            ChoiceCommand<Location> locationChoice = new(filter, $"Where to move {name}?");
            StandardMoveCommand moveCommand = new(this, locationChoice);

            CommandManager.EnqueueCommand(locationChoice);
            CommandManager.EnqueueCommand(moveCommand);

        }
    }
}
