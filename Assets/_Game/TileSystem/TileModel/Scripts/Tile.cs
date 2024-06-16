using UnityEngine;

namespace _Game.TileSystem.TileModel.Scripts
{
    public abstract class Tile : MonoBehaviour, ITile
    {
        [Header("References")] [SerializeField]
        private SpriteRenderer spriteRenderer;

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