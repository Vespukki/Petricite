using System.Collections.Generic;
using System.Threading.Tasks;

namespace Petricite
{
    public class CommandManager
    {
        private static Queue<ICommand> commandQueue = new();

        public static void EnqueueCommand(ICommand command)
        {
            commandQueue.Enqueue(command);
        }

        public async void ProcessCommand()
        {
            while (commandQueue.Count == 0)
            {
                await Task.Yield();
            }
            await commandQueue.Dequeue().Execute();

            ProcessCommand();
        }
    }
}
