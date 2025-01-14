using UnityEngine;

namespace _Game.TileSystem.Tile.Scripts
{
    public interface ITileFactory
    {
        public GameObject Create(Level.Scripts.TileData tileData);
    }
}