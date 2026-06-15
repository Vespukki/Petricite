using System.Threading.Tasks;

namespace Petricite
{
    /// <summary>
    /// these are for storing a task as an option instead of getting a value
    /// </summary>
    public class Choice : IChoosable
    {
        public ICommand command;

        string name;
        public string Name => name;

        public Choice(ICommand command, string name)
        {
            this.command = command;
            this.name = name;
        }
    }
}
