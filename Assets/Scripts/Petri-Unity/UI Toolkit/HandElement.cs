using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Petrunity
{
    [UxmlElement]
    public partial class HandElement : ZoneElement
    {
        public HandElement()
        {
            //RegisterCallback<GeometryChangedEvent>((evt) => OnGeometryChanged());
        }

        public override void Add(VisualElement child)
        {
            base.Add(child);

        }

        public void ReadjustHandSize()
        {
            if (childCount == 0) return;

            float w = resolvedStyle.width;
            float childW = 150;//contentContainer.Children().ToList()[0].resolvedStyle.width;
            float spacing = childW;

            float totalChildW = childCount * childW;

            if (totalChildW > w) //they fit normal
            {
                float wDiff = totalChildW - w;
                float diffPerChild = wDiff / childCount;

                spacing = childW - diffPerChild;
            }

            for (int i = 0; i < childCount; i++)
            {
                var foundChild = contentContainer[i];

                foundChild.style.position = Position.Absolute;
                foundChild.style.left = i * spacing;
            }
        }

        public override void OnChildAdded(VisualElement child)
        {
            base.OnChildAdded(child);


            ReadjustHandSize();
        }

        public override void OnChildRemoved(VisualElement child)
        {
            base.OnChildRemoved(child);


            child.style.position = StyleKeyword.Null;
            child.style.left = StyleKeyword.Null;

            ReadjustHandSize();
        }
    }
}
