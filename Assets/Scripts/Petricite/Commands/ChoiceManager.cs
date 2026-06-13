using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Petricite
{
    public class ChoiceManager //This is where im gonna have to interface with Unity, because you gotta interface with something to make choices u know
    {
        private static ChoiceManager instance;

        /// <summary>
        /// this is the variable that is set when a choice has been made
        /// </summary>
        private static IChoosable chosen;

        public delegate void choiceDelegate(IEnumerable<IChoosable> choices, string choiceTitle);
        public static event choiceDelegate OnChoices;
        public static event Action<IChoosable> OnChosen;

        public ChoiceManager()
        {
            if (instance != null)
            {
                return;
            }

            instance = this;

            //OnChoices += TempRandomChoice;
        }

        private void TempRandomChoice(IEnumerable<IChoosable> choices, string title)
        {
            var rand = new System.Random();
            Debug.Log(choices.ToList().Count); //ok next thing to do is link choice to next command and also fix bug where theres compounding choices every time filter is run.
            Choose(choices.ToList()[rand.Next(choices.ToList().Count)]);
        }

        public static async Task<T> DoChoice<T>(IEnumerable<T> options, string title)
        {
            OnChoices?.Invoke((IEnumerable<IChoosable>)options, title);
            await WaitForChoice(); //beyond here chosen != null
            Debug.Log($"Choice made!! we chose {chosen.ChoiceName}");

            OnChosen?.Invoke(chosen);
            IChoosable final = chosen;
            chosen = null;

            return (T)final;
        }

        /// <summary>
        /// this is what should be called by outside scripts to confirm the choice
        /// </summary>
        /// <param name="choice"></param>
        public static void Choose(IChoosable choice)
        {
            chosen = choice;
        }

        private static async Task WaitForChoice()
        {
            while (chosen == null)
            {
                await Task.Yield();
            }
        }
    }
}
