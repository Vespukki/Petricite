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
            ZoneElement parent = null;

            if (child is CardElement card)
            {

                card.sprite = hiddenZone ? card.backSprite : card.frontSprite;
                if (card.parent is ZoneElement)
                {
                    parent = card.parent as ZoneElement;
                }
            }

            base.Add(child);

            parent?.OnChildRemoved(child);
            OnChildAdded(child);
        }

        public virtual void OnChildRemoved(VisualElement child)
        {

        }
        public virtual void OnChildAdded(VisualElement child)
        {

        }
    }
}
