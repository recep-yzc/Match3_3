using System.Collections.Generic;
using _Game.TileSystem.Tile.Scripts;

namespace _Game.Board.Scripts
{
    public static class BoardConstants
    {
        public static List<TileData> TileDataList { get; set; } = new();
        public static Dictionary<float, List<TileData>> HorizontalTileDataList { get; set; } = new();
        public static Dictionary<float, List<TileData>> VerticalTileDataList { get; set; } = new();
    }
}