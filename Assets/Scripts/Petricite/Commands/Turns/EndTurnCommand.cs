using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Petricite
{
    public class EndTurnCommand : ICommand
    {
        public static event Func<Player, Task> OnEndTurn;

        private Player player;

        public EndTurnCommand(Player player)
        {
            this.player = player;
        }

        public async Task Execute()
        {
            var listeners = OnEndTurn?.GetInvocationList();
            if (listeners != null)
            {
                foreach (Func<Player, Task> listener in listeners) //IN THE FUTURE WE WANNA CONVERT THIS TO A COMMAND THAT LETS THE PLAYER ORDER THE TRIGGERS
                {
                    await listener(player);
                }
            }

            //Then down here the expiration step would happen

            CommandManager.StartNextTurn();

        }

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }
    }
}
