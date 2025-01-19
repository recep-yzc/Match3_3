using UnityEngine;

namespace _Game.Core.Elements.Element.Scripts
{
    public abstract class ElementBase : MonoBehaviour, IElement
    {
        [Header("References")] [SerializeField]
        private SpriteRenderer spriteRenderer;

        #region Public

        private ElementId _elementId;

        #endregion

        public void SetElementId(ElementId elementId)
        {
            _elementId = elementId;
        }

        public ElementId GetElementId()
        {
            return _elementId;
        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }

        public void SetPosition(Vector2 coordinate)
        {
            transform.position = coordinate;
        }

        public void SetSprite(Sprite sprite)
        {
            spriteRenderer.sprite = sprite;
        }
    }
}