using Petricite;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

namespace Petrunity
{
    [UxmlElement]
    public partial class PoolElement : VisualElement
    {
        public Image image;
        public Label label;
        public PoolElement()
        {
            this.image = new Image();
            this.label = new Label("0");

            contentContainer.Add(image);
            image.contentContainer.Add(label);

            image.AddToClassList("PoolImage");
            label.AddToClassList("PoolLabel");
        }

        public void Set(int amount)
        {
            label.text = amount.ToString();
        }

        public PoolElement(Domain domain) : this()
        {
            Initialize(domain);
        }

        public void Initialize(Domain domain)
        {
            Addressables.LoadAssetAsync<Sprite>(domain.ToString() + "Power").Completed += (handle) => 
            {
                if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
                {
                    image.sprite = handle.Result;
                }
            };
        }
    }
}
