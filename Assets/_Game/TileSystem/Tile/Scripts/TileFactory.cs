using UnityEngine;

namespace _Game.TileSystem.Tile.Scripts
{
    public abstract class TileFactory : MonoBehaviour, ITileFactory
    {
        protected abstract void Awake();
        public abstract GameObject Create(Level.Scripts.TileData tileData);
    }
}