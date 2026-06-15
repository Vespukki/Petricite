using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Petricite
{
    public class FilterChoiceCommand<T> : ChoiceCommand<T> where T : class, IChoosable
    {
        public FilterChoiceCommand(Player player, Filter<T> filter, string choiceTitle, bool allowNull = false) : base(player, filter.GetValid(), choiceTitle, allowNull)
        {
        }
    }
}