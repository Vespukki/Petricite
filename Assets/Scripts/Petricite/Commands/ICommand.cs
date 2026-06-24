using System.Threading.Tasks;

namespace Petricite
{
    public interface ICommand
    {
        /// <summary>
        /// for things like targeting, costs/additional costs, and other decisions that are made before something begins pending.
        /// </summary>
        /// <returns></returns>
        public Task PreExecute();
        public Task Execute();

        public void Unexecute();
    }
}
