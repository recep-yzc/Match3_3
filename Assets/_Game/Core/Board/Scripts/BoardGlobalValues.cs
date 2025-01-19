using System.Collections.Generic;
using _Game.Core.Grid.Scripts;

namespace _Game.Core.Board.Scripts
{
    public abstract class BoardGlobalValues
    {
        public static List<GridData> TileDataList { get; } = new();
        public static Dictionary<float, List<GridData>> HorizontalTileDataList { get; } = new();
        public static Dictionary<float, List<GridData>> VerticalTileDataList { get; } = new();
    }
}