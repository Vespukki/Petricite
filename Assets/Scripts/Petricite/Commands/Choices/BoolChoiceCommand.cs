using System.Collections.Generic;
using UnityEngine;

namespace Petricite
{
    public class BoolChoiceCommand : ChoiceCommand<Choice>
    {
        public BoolChoiceCommand(Player player, string choiceTitle, Choice choice) : base(player, new(){choice}, choiceTitle, true)
        {
        }
    }
}
