namespace Petricite
{
    public class Query<T, O>
    {
        public O source;
        public string stat;
        public T baseValue;
        public T value;

        public Query(O source, string stat, T baseValue)
        {
            this.source = source;
            this.stat = stat;
            this.baseValue = baseValue;
            this.value = baseValue;
        }

        public static T GetValue(O card, string stat, T baseValue)
        {
            var query = new Query<T, O>(card, stat, baseValue);

            GameEvents<T, O>.RaiseQuery(query);

            return query.value;
        }
    }
}
