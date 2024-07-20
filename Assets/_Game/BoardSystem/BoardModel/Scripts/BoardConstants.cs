using System.Collections.Generic;
using _Game.TileSystem.TileModel.Scripts;

namespace _Game.BoardSystem.BoardModel.Scripts
{
    public static class BoardConstants
    {
        public static List<TileData> TileData { get; set; } = new();
        public static Dictionary<float, List<TileData>> HorizontalTileData { get; set; } = new();
        public static Dictionary<float, List<TileData>> VerticalTileData { get; set; } = new();
    }
}