using UnityEngine;
using UnityEngine.UIElements;

namespace Petrunity
{
    [UxmlElement]
    public partial class ZoneElement : VisualElement
    {
        [UxmlAttribute]
        public bool hiddenZone;


        /// <summary>
        /// override of the base add func so i can do visual stuff
        /// </summary>
        /// <param name="child"></param>
        public virtual new void Add(VisualElement child)
        {
            base.Add(child);

            if (child is CardElement card)
            {
                card.sprite = hiddenZone ? card.backSprite : card.frontSprite;
            }
        }
    }
}
