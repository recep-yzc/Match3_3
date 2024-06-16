using UnityEngine;

namespace _Game.TileSystem.TileModel.Scripts
{
    public abstract class TileFactory : MonoBehaviour
    {
        public abstract GameObject CreateTile(Vector2 coordinate);
    }
}