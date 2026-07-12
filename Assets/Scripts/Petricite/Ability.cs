using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Petricite
{
    public class Ability : ICommand, IChoosable, IFilterable<Ability>
    {
        public Card source;
        public string Name { get; set; }
        Func<Task> execute;
        Func<Task> preExecute;
        public Func<bool> validTest { get; set; }

        public Ability(Card source, string name, Func<Task> execute, Func<Task> preExecute, Func< bool> validTest)
        {
            this.source = source;
            this.Name = name;
            this.execute = execute;
            this.preExecute = preExecute;
            this.validTest = validTest;

            Filter<Ability>.OnFilter += OnFilter;
        }

        public void OnFilter(Filter<Ability> filter)
        {
            if (filter.test(this) && validTest())
            {
                filter.value.Add(this);
            }
        }

        public async Task Execute()
        {
            await execute();
        }

        public async Task PreExecute()
        {
            await preExecute();
        }

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }
    }
}
