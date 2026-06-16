using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Petrunity
{
    public class TestUIEvents : MonoBehaviour
    {
        [SerializeField] private UIDocument document;

        private Image image;


        private void Awake()
        {
            image = document.rootVisualElement.Q<Image>("TestImage");

            image.RegisterCallback<ClickEvent>(OnClick);

            image.RegisterCallback<PointerDownEvent>(evt =>
            {
                Debug.Log("Pointer down on image");
            });
        }

        private void OnClick(ClickEvent evt)
        {
            Debug.Log("CLICK REGISTERED");
        }
    }
}
