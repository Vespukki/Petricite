using System;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Petricite
{
    public class Ability : ICommand, IChoosable
    {
        private string name;
        public string Name => name;
        public Card source;
        Func<Ability, Task> abilityTask;
        Func<Ability, Task> preAbilityTask;
        public Cost cost;

        public Ability(string name, Func<Ability, Task> preAbilityTask, Func<Ability, Task> abilityTask, Cost cost = default)
        {
            this.name = name;
            this.preAbilityTask = preAbilityTask;
            this.abilityTask = abilityTask;
            this.cost = cost;

            Filter<Ability>.OnFilter += OnFilter;
        }

        private void OnFilter(Filter<Ability> filter)
        {
            if (filter.test(this))
            {
                filter.value.Add(this);
            }
        }

        public async Task Execute()
        {
            await abilityTask(this);
        }

        public async Task PreExecute()
        {
            await preAbilityTask(this);
        }

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }
    }
}
