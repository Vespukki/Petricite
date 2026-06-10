using System.Collections.Generic;

namespace Petricite
{
    public class GameAction
    {
        public string type;
        public Card source;
        public List<Card> targets;

        public int value;
        public bool cancelled = false;

        public GameAction(string type, Card source, List<Card> targets, int value)
        {
            this.type = type;
            this.source = source;
            this.targets = targets;
            this.value = value;
        }
    }
}
