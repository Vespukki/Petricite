using UnityEngine;
using Petricite;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Petrunity
{
    public class CardObject : MonoBehaviour, IPointerClickHandler
    {
        public Card card;

        private Color baseColor;
        private Color HIGHLIGHT_COLOR = Color.cyan;
        private Color LOWLIGHT_COLOR = Color.gray;
        private Image image;

        public static event Action<Card> OnCardClicked;

        private void Awake()
        {
            ChoiceManager.OnChoices += OnChoices;
            ChoiceManager.OnChosen += OnChosen;
            image = GetComponentInChildren<Image>();
            baseColor = image.color;
        }

        private void OnChosen(IChoosable choosable)
        {
            Unhighlight();
        }

        private void OnChoices(IEnumerable<IChoosable> choices, string choiceTitle)
        {
            if (choices.Contains(card))
            {
                Highlight();
            }
            else
            {
                Lowlight();
            }
        }

        protected virtual void OnLeftClick()
        {
            OnCardClicked?.Invoke(card);
        }
        protected virtual void OnRightClick()
        {

        }

        public void Highlight()
        {
            image.color = HIGHLIGHT_COLOR;
        }

        public void Unhighlight()
        {
            image.color = baseColor;
        }

        public void Lowlight()
        {
            image.color = LOWLIGHT_COLOR;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnLeftClick();
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                OnRightClick();
            }
        }
    }
}
