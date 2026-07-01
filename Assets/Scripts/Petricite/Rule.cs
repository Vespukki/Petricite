using System;

namespace Petricite
{
    public class Rule
    {
        object source;
        Action unsubscribeAction;
        public Rule(object source, Action subscribeAction, Action unsubscribeAction)
        {
            this.source = source;
            this.unsubscribeAction = unsubscribeAction;
            subscribeAction();
        }


        public void Unsub()
        {
            unsubscribeAction();
        }
    }
}
