using UnityEngine;

namespace _Game.TileSystem.TileModel.Scripts
{
    public interface ITile
    {
        public void SetPosition(Vector2 coordinate);
        public void SetParent(Transform parent);
    }
}