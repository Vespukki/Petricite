using System.Collections.Generic;
using System.Threading.Tasks;

namespace Petricite
{
    public class ChoiceCommand<T> : ICommand where T : class, IChoosable
    {
        private List<T> choices;
        private string choiceTitle;
        private bool allowNull;

        public T result;
        public ChoiceCommand(List<T> filter, string choiceTitle, bool allowNull = false)
        {
            this.choices = filter;
            this.choiceTitle = choiceTitle;
            this.allowNull = allowNull;
        }

        public async Task Execute()
        {
            if (allowNull) choices.Add(null);
            result = await ChoiceManager.DoChoice(choices, choiceTitle);
        }

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }
    }
}