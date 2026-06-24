using System.Collections.Generic;

namespace Petricite
{
    public class Chain : GenericZone<IChainable>
    {
        public Chain() : base("The Chain", int.MaxValue)
        {
            
        }

        public Queue<IChainable> itemQueue = new();

        public void DoNextItem()
        {
            if (itemQueue.Count == 0) return;

            var nextItem = itemQueue.Dequeue();
        }
    }

}
