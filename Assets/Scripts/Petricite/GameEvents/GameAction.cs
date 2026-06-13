using System;
using System.Collections.Generic;

namespace Petricite
{

    public class GameAction<ValueType, ObjectType> : GameAction<ValueType, ObjectType, ObjectType>
    {
        public GameAction(string type, ObjectType source, List<ObjectType> targets, ValueType value) : base(type, source, targets, value)
        {
        }
    }

    public class GameAction<ValueType, SourceType, TargetType>
    {
        public string type;
        public SourceType source;
        public List<TargetType> targets;

        public ValueType value;
        public bool cancelled = false;

        public GameAction(string type, SourceType source, List<TargetType> targets, ValueType value)
        {
            this.type = type;
            this.source = source;
            this.targets = targets;
            this.value = value;
        }

        public static event Action<GameAction<ValueType, SourceType, TargetType>> onBeforeAction;
        public static event Action<GameAction<ValueType, SourceType, TargetType>> onAfterAction;
        public static void RaiseBeforeAction(GameAction<ValueType, SourceType, TargetType> action) => onBeforeAction?.Invoke(action);
        public static void RaiseAfterAction(GameAction<ValueType, SourceType, TargetType> action) => onAfterAction?.Invoke(action);
    }
}
