using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Petricite
{
    public class BaseChoiceCommand
    {
        public static event Action<List<IChoosable>> OnChoiceFinished;
        public static event Action<IChoosable> OnNewChoice;

        protected static void InvokeOnChoiceFinished(List<IChoosable> chosen)
        {
            OnChoiceFinished?.Invoke(chosen);
        }
        protected static void InvokeOnNewChoice(IChoosable chosen)
        {
            OnNewChoice?.Invoke(chosen);
        }
    }
}
