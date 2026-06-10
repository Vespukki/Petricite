using System;

namespace Petricite
{
    public static class GameEvents<T, O>
    {
        public static event Action<Query<T, O>> OnQuery;

        public static void RaiseQuery(Query<T, O> query) => OnQuery?.Invoke(query);

        public static event Action<GameAction> onBeforeAction; 
        public static event Action<GameAction> onAfterAction;

        public static void RaiseBeforeAction(GameAction action) => onBeforeAction?.Invoke(action);
        public static void RaiseAfterAction(GameAction action) => onAfterAction?.Invoke(action);

    }
}
