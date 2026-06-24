using Petricite;
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
            sprite = frontSprite;
            this.frontSprite = frontSprite;
        }

        public CardElement(Sprite frontSprite, Sprite backSprite) : this(frontSprite)
        {
            this.backSprite = backSprite;
        }

        [UxmlAttribute]
        public Sprite frontSprite;
        [UxmlAttribute]
        public Sprite backSprite;
    }
}
