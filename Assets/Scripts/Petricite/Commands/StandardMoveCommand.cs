using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Petricite
{
    public class StandardMoveCommand : ICommand
    {
        private Player player;
        private MultichoiceCommand<Unit> unitChoices;
        private FilterChoiceCommand<Location> locationChoice;
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

        public Task Execute()
        {
            foreach (var unit in unitChoices.selected)
            {
                unit.Ready = false;
                unit.Zone = locationChoice.result;
            }

            return Task.CompletedTask;
        }

        public void Unexecute()
        {
        }

        public async Task PreExecute()// maybe should be restricted to only having one choice per task? like this double one might cause problems
        {
            Filter<Location> filter = new((loc) => loc.MaxCards > loc.CardCount && loc.owner == player);
            locationChoice = new(player, filter, $"Where to move?");

            await locationChoice.Execute();

            unitChoices = new((unit) => UnitTest(locationChoice.result, unit), 1, int.MaxValue, player);

            await unitChoices.Execute();
        }
    }
}
