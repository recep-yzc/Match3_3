using UnityEngine;

namespace _Game.TileSystem.TileModel.Scripts
{
    public abstract class TileFactory : MonoBehaviour
    {
        public abstract Tile CreateTile(Vector2 coordinate);
    }
}