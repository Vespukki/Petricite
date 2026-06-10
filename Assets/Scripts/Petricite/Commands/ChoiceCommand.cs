using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Petricite
{
    public class ChoiceCommand<T> : ICommand where T : IChoosable
    {
        private Filter<T> filter;
        private string choiceTitle;
        public T result;
        public ChoiceCommand(Filter<T> filter, string choiceTitle)
        {
            this.filter = filter;
            this.choiceTitle = choiceTitle;
        }

        public async Task Execute()
        {
            List<T> valid = filter.GetValid();
            result  = await ChoiceManager.DoChoice(valid, choiceTitle);
        }

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }
    }
}
