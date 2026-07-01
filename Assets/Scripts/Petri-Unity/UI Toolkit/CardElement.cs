using Petricite;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Petrunity
{
    [UxmlElement]
    public partial class CardElement : Image
    {
        public CardElement()
        {
            AddToClassList("Card");
        }

        public CardElement(Sprite frontSprite) : this()
        {
            ReloadSprite(frontSprite);
        }

        public CardElement(Sprite frontSprite, Sprite backSprite) : this(frontSprite)
        {
            this.backSprite = backSprite;
        }

        [UxmlAttribute]
        public Sprite frontSprite;
        [UxmlAttribute]
        public Sprite backSprite;

        public void ReloadSprite(Sprite newFront)
        {
            frontSprite = newFront;
            sprite = newFront;
        }
    }
}
