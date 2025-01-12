using System.Linq;
using _Game.GridSystem.Scripts;
using _Game.TileSystem.TileModel.Scripts;
using UnityEngine;

namespace _Game.BoardSystem.Scripts
{
    public static class BoardHelper
    {
        public static TileData GetTileDataByCoordinate(Vector2 coordinate)
        {
            return BoardConstants.TileDataList.FirstOrDefault(tileData =>
                GridHelper.CheckOverlapWithDot(tileData.BottomLeft, tileData.TopRight, coordinate));
        }
    }
}