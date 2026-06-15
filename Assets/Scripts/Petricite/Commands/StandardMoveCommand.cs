using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Petricite
{
    public class StandardMoveCommand : ICommand
    {
        private Player player;
        public StandardMoveCommand(Player player)
        {
            this.player = player;
        }

        private bool UnitTest(Location location, Unit unit)
        {
            if (unit.Zone != location && unit.controller == player && unit.Ready)
            {
                return true;
            }

            return false;
        }

        public async Task Execute()
        {
            Filter<Location> filter = new((loc) => loc.MaxCards > loc.CardCount && loc.owner == player);
            FilterChoiceCommand<Location> locationChoice = new(player, filter, $"Where to move?");
            await locationChoice.Execute();

            MultichoiceCommand<Unit> unitChoices = new((unit) => UnitTest(locationChoice.result, unit), 1, int.MaxValue, player);

            await unitChoices.Execute();

            foreach (var unit in unitChoices.selected)
            {
                unit.Ready = false;
                unit.Zone = locationChoice.result;
            }
        }

        public void Unexecute()
        {
        }
    }
}
