using UnityEngine;

namespace _Game.TileSystem.TileModel.Scripts
{
    public interface ITile
    {
        public TileId TileId { get; set; }

        public void SetTileId(TileId tileId);
        public void SetPosition(Vector2 coordinate);
        public void SetParent(Transform parent);
    }
}