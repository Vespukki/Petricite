using System.Threading.Tasks;

namespace Petricite
{
    public class StandardMoveCommand : ICommand
    {
        private Unit unit;
        private ChoiceCommand<Location> locationChoice;
        public StandardMoveCommand(Unit unit, ChoiceCommand<Location> locationChoice)
        {
            this.unit = unit;
            this.locationChoice = locationChoice;
        }

        public async Task Execute()
        {
            unit.Zone = locationChoice.result;
        }



        public void Unexecute()
        {
        }
    }
}
