using System.Collections.Generic;
using System.Threading.Tasks;

namespace Petricite
{
    public class ChoiceCommand<T> : ICommand where T : class, IChoosable
    {
        private List<T> choices;
        private string choiceTitle;
        private bool allowNull;
        private Player player; //the player making the choice

        public T result;
        public ChoiceCommand(Player player, List<T> choices, string choiceTitle, bool allowNull = false)
        {
            this.choices = choices;
            this.choiceTitle = choiceTitle;
            this.allowNull = allowNull;
        }

        public async Task Execute()
        {
            if (allowNull) choices.Add(null);
            result = await ChoiceManager.DoChoice(player, choices, choiceTitle);
        }

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }

        public Task PreExecute()
        {
            return Task.CompletedTask;
        }
    }
}