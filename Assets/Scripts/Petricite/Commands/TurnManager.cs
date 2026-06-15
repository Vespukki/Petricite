namespace Petricite
{
    public class TurnManager
    {
        public TurnManager()
        {

        }

        public void TurnCycle()
        {
            CommandManager.EnqueueCommand(new MainPhaseCommand());
        }
    }
}
