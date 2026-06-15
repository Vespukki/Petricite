using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Petricite
{
    public class MainPhaseCommand : ICommand
    {
        public MainPhaseCommand()
        {
            
        }

        public async Task Execute()//ok so kinda works but now we gotta make mainPhaseCommand ask the player "what u wanna do" instead of just going into a move.
        {

            Choice standardMoveChoice = new(new StandardMoveCommand(), "Standard Move");

            ChoiceCommand<Choice> choiceCommand = new(new List<Choice>() {standardMoveChoice }, "Main Phase", false);


            await choiceCommand.Execute();

            var chosen = choiceCommand.result;
            await chosen.command.Execute();

        }

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }
    }
}
