using System.Collections.Generic;
using System.Linq;
using _Game.GridSystem.GridModel.Scripts;
using _Game.TileSystem.TileModel.Scripts;
using UnityEngine;

namespace _Game.BoardSystem.BoardModel.Scripts
{
    public static class BoardHelper
    {
        public static TileData GetTileDataByCoordinate(List<TileData> tileDataList, Vector2 coordinate)
        {
            return tileDataList.FirstOrDefault(tileData =>
                GridHelper.CheckOverlapWithDot(tileData.BottomLeft, tileData.TopRight, coordinate));
        }
    }
}