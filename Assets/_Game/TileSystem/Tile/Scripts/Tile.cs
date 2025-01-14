using UnityEngine;

namespace _Game.TileSystem.Tile.Scripts
{
    public abstract class Tile : MonoBehaviour, ITile
    {
        [Header("References")] [SerializeField]
        private SpriteRenderer spriteRenderer;

        #region Public

        public TileId TileId { get; set; }

        #endregion

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }

        public void SetTileId(TileId tileId)
        {
            TileId = tileId;
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