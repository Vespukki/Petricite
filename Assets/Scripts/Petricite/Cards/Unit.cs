using UnityEngine;

namespace Petricite
{
    public class Unit : Permanent
    {
        protected int might;
        
        public delegate void unitHandler(Unit unit);
        public static event unitHandler OnUnitPlayed;

        public Unit(Zone zone, Player controller, string name = "UNNAMED PLAYABLE CARD", int powerCost = 0, int energyCost = 0, int might = 0) : base(zone,controller, name, powerCost, energyCost)
        {
            this.might = might;

            OnUnitPlayed?.Invoke(this);
        }

        public int GetMight()
        {
            var query = new Query<int, Card>(this, "MIGHT", might);

            Query<int, Card>.RaiseQuery(query);

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
