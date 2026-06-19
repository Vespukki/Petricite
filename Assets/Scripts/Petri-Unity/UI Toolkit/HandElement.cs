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

        public override void OnGeometryChanged()
        {
            base.OnGeometryChanged();

            Debug.Log(childCount);
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
                var child = contentContainer[i];

                child.style.position = Position.Absolute;
                child.style.left = i * spacing;
            }
        }
    }
}
