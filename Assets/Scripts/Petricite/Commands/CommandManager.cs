using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Petricite
{
    public class CommandManager
    {
        private static Stack<ICommand> commandStack = new();
        private static LinkedList<Player> turnOrder = new();

        public static Player currentTurnPlayer;
        private List<Player> players;

        public CommandManager(List<Player> players)
        {
            this.players = players; //note that it is a reference to the list
            for (int i = 0; i < players.Count; i++)
            {
                EnqueueTurn(players[i]);
            }
        }

        public static Player PopTurn()
        {
            var final = turnOrder.First;
            turnOrder.RemoveFirst();
            return final.Value;
        }

        /// <summary>
        /// this adds a turn to the end of the queue/bottom of the stack
        /// </summary>
        public static void EnqueueTurn(Player player)
        {
            turnOrder.AddLast(player);
        }

        public static void PushCommand(ICommand command)
        {
            commandStack.Push(command);
        }

        public async void ProcessCommand()
        {
            if (commandStack.Count == 0)
            {
                PushCommand(GetTurnCommand());//Now i must add player turns so its only 1 persons turn at a time. this includes choices being sent to a particular player
            }
            await commandStack.Pop().Execute();

            ProcessCommand();
        }

        private ICommand GetTurnCommand()
        {
            return new MainPhaseCommand(currentTurnPlayer);
        }

        public static void StartNextTurn()
        {
            currentTurnPlayer = PopTurn();
            PushCommand(new AwakePhaseCommand(currentTurnPlayer));
            //PushCommand(new BeginningPhaseCommand(currentTurnPlayer));
            //PushCommand(new ChannelPhaseCommand(currentTurnPlayer));
            //PushCommand(new DrawPhaseCommand(currentTurnPlayer));
        }
    }
}
