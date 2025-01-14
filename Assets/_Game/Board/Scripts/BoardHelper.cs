using System.Linq;
using _Game.TileSystem.Tile.Scripts;
using _Game.Utilities.Scripts;
using UnityEngine;

namespace _Game.Board.Scripts
{
    public static class BoardHelper
    {
        public static TileData GetTileDataByCoordinate(Vector2 coordinate)
        {
            return BoardConstants.TileDataList.FirstOrDefault(tileData =>
                VectorHelper.CheckOverlapWithDot(tileData.BottomLeft, tileData.TopRight, coordinate));
        }
    }
}