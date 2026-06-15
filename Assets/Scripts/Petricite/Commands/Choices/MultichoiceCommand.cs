using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Petricite
{
    public class MultichoiceCommand<T> : ICommand where T : class, IChoosable
    {
        public List<T> selected = new();
        Filter<T>.testDelegate test;
        private int minChoices, maxChoices;
        public MultichoiceCommand(Filter<T>.testDelegate test, int minChoices, int maxChoices)
        {
            this.test = test;
            this.minChoices = minChoices;
            this.maxChoices = maxChoices;
        }

        private bool DoTest(T obj)
        {
            return test(obj) && !selected.Contains(obj);
        }

        public async Task Execute()
        {
            while (true)
            {
                FilterChoiceCommand<T> choiceCommand = new(new((obj) => DoTest(obj) ), $"Choose a thing", allowNull:true);
                await choiceCommand.Execute();
                var result = choiceCommand.result;

                if (result == null) break;

                selected.Add(result);

                if (selected.Count >= maxChoices) break;
            }
            
        }

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }
    }
}
