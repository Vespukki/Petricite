using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

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
            if (commandQueue.Count == 0)
            {
                EnqueueCommand(GetTurnCommand());//Now i must add player turns so its only 1 persons turn at a time. this includes choices being sent to a particular player
            }
            await commandQueue.Dequeue().Execute();

            ProcessCommand();
        }

        private ICommand GetTurnCommand()
        {
            return new MainPhaseCommand();
        }
    }
}
