using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Petricite
{
    public class MainPhaseCommand : ICommand
    {
        private Player player;
        private ChoiceCommand<Choice> choiceCommand;
        public MainPhaseCommand(Player player)
        {
            this.player = player;
        }

        public Task Execute()
        {

            var chosen = choiceCommand.result;
            CommandManager.PushCommand(chosen.command);

            return Task.CompletedTask;
        }

        public async Task PreExecute()
        {
            Choice standardMoveChoice = new(new StandardMoveCommand(player), "Standard Move");
            Choice endTurnChoice = new(new EndTurnCommand(player), "End Turn");

            List<Choice> choices = new() {standardMoveChoice};

            //Hand
            Filter<Hand> handFilter = new((hand) => hand.owner == player);
            List<Hand> hands = handFilter.GetValid();
            if (hands.Count > 0)
            {
                Filter<PlayableCard> playableFilter = new((card) => card.Zone == hands[0]);
                List<PlayableCard> playables = playableFilter.GetValid();

                foreach (var playable in playables)
                {
                    choices.Add(new(playable, playable.Name));
                }
            }

           /* //Abilities
            Filter<Ability> abilityFilter = new((a) => a.source.controller == player);
            List<Ability> abilities = abilityFilter.GetValid();
            foreach (var ability in abilities)
            {
                choices.Add(new(ability, ability.Name));
            }*/


            choices.Add(endTurnChoice);

            choiceCommand = new(player, choices, $"{player.Name}'s Main Phase", false);


            await choiceCommand.Execute();
        }

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }
    }
}
