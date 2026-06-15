using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Petricite
{
    public class StandardMoveCommand : ICommand
    {
        public StandardMoveCommand()
        {
        }

        private bool UnitTest(Location location, Unit unit)
        {
            if (unit.Zone != location)
            {
                return true;
            }

            return false;
        }

        public async Task Execute()
        {
            Filter<Location> filter = new((loc) => loc.MaxCards > loc.CardCount);
            FilterChoiceCommand<Location> locationChoice = new(filter, $"Where to move?");
            await locationChoice.Execute();

            MultichoiceCommand<Unit> unitChoices = new((unit) => UnitTest(locationChoice.result, unit), 1, int.MaxValue);
            //StandardMoveCommand moveCommand = new(this, locationChoice);

            //CommandManager.EnqueueCommand(moveCommand);


            await unitChoices.Execute();

            foreach (var unit in unitChoices.selected)
            {
                unit.Zone = locationChoice.result;
            }
        }

        public void Unexecute()
        {
        }
    }
}
