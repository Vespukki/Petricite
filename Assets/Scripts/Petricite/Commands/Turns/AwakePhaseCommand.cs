using System.Threading.Tasks;

namespace Petricite
{
    public class AwakePhaseCommand : ICommand
    {
        Player player;

        public AwakePhaseCommand(Player player)
        {
            this.player = player;
        }

        public async Task Execute()
        {
            CommandManager.EnqueueTurn(player);
        }

        public Task PreExecute()
        {
            return Task.CompletedTask;
        }

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }
    }
}
