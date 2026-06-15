using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Petricite
{
    public class FilterChoiceCommand<T> : ChoiceCommand<T> where T : class, IChoosable
    {
        public FilterChoiceCommand(Filter<T> filter, string choiceTitle, bool allowNull = false) : base(filter.GetValid(), choiceTitle, allowNull)
        {
        }
    }
}