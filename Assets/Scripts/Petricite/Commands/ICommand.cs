using System.Threading.Tasks;

namespace Petricite
{
    public interface ICommand
    {
        public Task Execute();

        public void Unexecute();
    }
}
