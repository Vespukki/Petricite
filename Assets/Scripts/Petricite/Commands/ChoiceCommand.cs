using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Petricite
{
    public class ChoiceCommand<T> : ICommand where T : class, IChoosable
    {
        private Filter<T> filter;
        private string choiceTitle;
        private bool allowNull;

        public T result;
        public ChoiceCommand(Filter<T> filter, string choiceTitle, bool allowNull = false)
        {
            this.filter = filter;
            this.choiceTitle = choiceTitle;
            this.allowNull = allowNull;
        }

        public async Task Execute()
        {
            List<T> valid = filter.GetValid();
            if (allowNull) valid.Add(null);
            result = await ChoiceManager.DoChoice(valid, choiceTitle);
        }

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }
    }
}
