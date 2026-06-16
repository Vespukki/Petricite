using UnityEngine;
using UnityEngine.UIElements;

namespace Petrunity
{
    [UxmlElement]
    public partial class PlayerBoardElement : VisualElement
    {
        [UxmlAttribute]
        public string playerBase;
    }
}
