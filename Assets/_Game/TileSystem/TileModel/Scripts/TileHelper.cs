using UnityEngine;

namespace _Game.TileSystem.TileModel.Scripts
{
    public static class TileHelper
    {
        public static T GetTileAsComponent<T>(GameObject tile)
        {
            return tile.TryGetComponent(out T t) ? t : default;
        }
    }
}