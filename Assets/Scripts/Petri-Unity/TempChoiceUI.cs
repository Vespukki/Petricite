using UnityEngine;
using Petricite;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine.UI;

namespace Petrunity
{
    public class TempChoiceUI : MonoBehaviour
    {
        public Button choiceButtonPrefab;
        public Transform buttonHolder;
        public TextMeshProUGUI title;

        private List<Button> spawnedButtons = new();

        private void Awake()
        {
            ChoiceManager.OnChoices += PromptChoices;
            ChoiceManager.OnChosen += ClearChoices;
        }

        private void ClearChoices(IChoosable chosen)
        {
            title.SetText("No active choice");

            foreach (var button in spawnedButtons)
            {
                Destroy(button.gameObject);
            }

            spawnedButtons.Clear();

        }

        private void PromptChoices(IEnumerable<IChoosable> choices, string choiceTitle, Player player)
        {

            title.SetText(choiceTitle);

            //IMPORTANT TO NOTE THAT CHOICE CAN BE NULL
            foreach (var choice in choices)
            {
                var choiceName = choice == null ? "Done" : choice.Name;

                var button = Instantiate(choiceButtonPrefab, buttonHolder).GetComponent<Button>();
                button.gameObject.SetActive(true);
                button.GetComponentInChildren<TextMeshProUGUI>().SetText(choiceName);

                button.onClick.AddListener(() => ChoiceManager.Choose(choice));

                spawnedButtons.Add(button);
            }
        }
    }
}
