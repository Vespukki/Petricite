using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Petricite
{
    public class MultichoiceCommand<T> : BaseChoiceCommand, ICommand where T : class, IChoosable
    {
        public List<T> selected = new();
        Func<T, bool> test;
        private int minChoices, maxChoices;
        private Player player;

        public MultichoiceCommand(Func<T, bool> test, int minChoices, int maxChoices, Player player)
        {
            this.player = player;
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
                FilterChoiceCommand<T> choiceCommand = new(player, new((obj) => DoTest(obj) ), $"Choose a thing", allowNull:true);
                await choiceCommand.Execute();
                var result = choiceCommand.result;

                if (result == null) break;

                selected.Add(result);
                InvokeOnNewChoice(result);
                if (selected.Count >= maxChoices) break;
            }

            InvokeOnChoiceFinished(selected.Cast<IChoosable>().ToList());
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
