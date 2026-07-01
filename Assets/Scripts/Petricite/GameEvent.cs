using System;
using System.Threading.Tasks;

namespace Petricite
{
    public class GameEvent<T>
    {
        private event Action<T> Event;

        public GameEvent()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="action">the action to be performed when the event is fired</param>
        /// <returns></returns>
        public Rule CreateRule(object obj, Action<T> action)
        {
            void init() => Event += action;
            void uninit() => Event -= action;

            return new Rule(obj, init, uninit);
        }

        /// <summary>
        /// DON'T call this from any class that isn't the owner of the GameEvent
        /// </summary>
        /// <param name="arg"></param>
        public void Invoke(T arg)
        {
            Event?.Invoke(arg);
        }

        public static GameEvent<T> operator +(GameEvent<T> evt, Action<T> arg)
        {
            evt.Event += arg;
            return evt;
        }
        public static GameEvent<T> operator -(GameEvent<T> evt, Action<T> arg)
        {
            evt.Event -= arg;
            return evt;
        }

    }
}
