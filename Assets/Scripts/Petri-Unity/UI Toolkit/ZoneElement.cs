using Petricite;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Petrunity
{
    [UxmlElement]
    public partial class ZoneElement : VisualElement
    {
        [UxmlAttribute]
        public bool hiddenZone;
        [UxmlAttribute]
        public Sprite backSprite;


        /// <summary>
        /// override of the base add func so i can do visual stuff
        /// </summary>
        /// <param name="child"></param>
        public virtual new void Add(VisualElement child)
        {
            ZoneElement parent = null;

            if (child is CardElement card)
            {
                card.onSpriteUpdated += UpdateCardSprite;
                if (card.parent is ZoneElement)
                {
                    parent = card.parent as ZoneElement;
                }
            }

            base.Add(child);

            parent?.OnChildRemoved(child);
            OnChildAdded(child);
        }

        private void UpdateCardSprite(CardElement cardElement, Sprite sprite)
        {
            cardElement.sprite = hiddenZone ? backSprite : sprite;
        }

        public virtual void OnChildRemoved(VisualElement child)
        {

        }
        public virtual void OnChildAdded(VisualElement child)
        {
            if (child is CardElement card)
            {
                UpdateCardSprite(card, card.frontSprite);
            }
        }
    }
}
