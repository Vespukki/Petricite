using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Petricite
{
    public class GenericCommand : ICommand
    {
        Func<Task> preExecute;
        Func<Task> execute;
        public GenericCommand(Func<Task> preExecute, Func<Task> execute)
        {
            this.preExecute = preExecute;
            this.execute = execute;
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
