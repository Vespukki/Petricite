using System.Threading.Tasks;
using UnityEngine;

namespace Petricite
{
    public class AccelerateCommand : ICommand
    {
        Card permanent;
        public AccelerateCommand(Card permanent)
        {
            this.permanent = permanent;
        }
        public async Task Execute()
        {
            Debug.Log("ACCLERATED EXECUTED KITTY CATED");
        }

        public async Task PreExecute()
        {
            Debug.Log("ACCELERATE ON PLAY");
            async Task preExecute()
            {
                Debug.Log("Accelerate PreExecute");
                return;
            }
            async Task execute()
            {
                Debug.Log("Accelerate Execute");
                return;
            }

            GenericCommand genCom = new(preExecute, execute);


            BoolChoiceCommand accelerateChoice = new(permanent.controller, "Accelerate?", new(genCom, $"Accelerate {permanent.Name}"));

            await accelerateChoice.Execute();

            if (accelerateChoice.result != null)
            {
                CommandManager.PushCommand(accelerateChoice.result.command);
            }
        }

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }
    }
}
