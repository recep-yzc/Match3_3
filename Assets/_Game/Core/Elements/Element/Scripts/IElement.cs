using UnityEngine;

namespace _Game.Core.Elements.Element.Scripts
{
    public interface IElement
    {
        public void SetElementId(ElementId elementId);
        public ElementId GetElementId();

        public void SetPosition(Vector2 coordinate);
        public void SetParent(Transform parent);
    }
}