using UnityEngine;
using Petricite;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

namespace Petrunity
{
    public class CardObject : MonoBehaviour, IPointerClickHandler
    {
        public Card card;

        private Color baseColor;
        private Color HIGHLIGHT_COLOR = Color.white;
        private Color LOWLIGHT_COLOR = Color.gray;
        private Image image;

        public static event Action<Card> OnCardClicked;

        private void Awake()
        {
            image = GetComponentInChildren<Image>();
            baseColor = image.color;
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
