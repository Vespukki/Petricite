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

        private static bool choiceMade = false;
        public static bool currentlyChoosing = false;

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
            Debug.Log(choices.ToList().Count);
            Choose(choices.ToList()[rand.Next(choices.ToList().Count)]);
        }

        public static async Task<T> DoChoice<T>(IEnumerable<T> options, string title) where T : IChoosable
        {
            currentlyChoosing = true;
            OnChoices?.Invoke((IEnumerable<IChoosable>)options, title);
            await WaitForChoice(); //beyond here chosen is the value
            Debug.Log($"Choice made!! we chose {chosen?.Name}");

            OnChosen?.Invoke(chosen);
            IChoosable final = chosen;
            chosen = null;
            choiceMade = false;

            currentlyChoosing = false;

            return (T)final;
        }

        /// <summary>
        /// this is what should be called by outside scripts to confirm the choice
        /// </summary>
        /// <param name="choice"></param>
        public static void Choose(IChoosable choice)
        {
            choiceMade = true;
            chosen = choice;
        }

        private static async Task WaitForChoice()
        {
            while (choiceMade == false)
            {
                await Task.Yield();
            }
        }
    }
}
