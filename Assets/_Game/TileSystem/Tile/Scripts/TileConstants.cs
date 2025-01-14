using System.Collections.Generic;

namespace _Game.TileSystem.Tile.Scripts
{
    public static class TileConstants
    {
        public static readonly Dictionary<TileId, ITileFactory> FactoryList = new();
        public static ElementDataSo SelectedElementDataSo { get; set; }
    }
}