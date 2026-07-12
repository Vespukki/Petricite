using Petricite;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Petrunity
{
    [UxmlElement]
    public partial class CardElement : Image
    {
        public event Action<CardElement, Sprite> onSpriteUpdated;

        public CardElement()
        {
            AddToClassList("Card");
        }

        public CardElement(Sprite frontSprite) : this()
        {
            this.frontSprite = frontSprite;
            sprite = frontSprite;
        }

        

        [UxmlAttribute]
        public Sprite frontSprite;
       

        public void ReloadSprite(Sprite newFront)
        {
            frontSprite = newFront; //Ok now we gotta make a channel method yay
            onSpriteUpdated?.Invoke(this, newFront);
        }
    }
}
