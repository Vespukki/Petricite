using System;

namespace Petricite
{
    public class Query<ValueType, ObjectType>
    {
        public ObjectType source;
        public string stat;
        public ValueType baseValue;
        public ValueType value;

        public Query(ObjectType source, string stat, ValueType baseValue)
        {
            this.source = source;
            this.stat = stat;
            this.baseValue = baseValue;
            this.value = baseValue;
        }

        public static ValueType GetValue(ObjectType card, string stat, ValueType baseValue)
        {
            var query = new Query<ValueType, ObjectType>(card, stat, baseValue);

            RaiseQuery(query);

            return query.value;
        }

        public static event Action<Query<ValueType, ObjectType>> OnQuery;

        public static void RaiseQuery(Query<ValueType, ObjectType> query) => OnQuery?.Invoke(query);
    }
}
