using System.Linq;
using _Game.Core.Grid.Scripts;
using _Game.Utilities.Scripts;
using UnityEngine;

namespace _Game.Core.Board.Scripts
{
    public static class BoardHelper
    {
        public static GridData GetGridDataByCoordinate(Vector2 coordinate)
        {
            return BoardGlobalValues.TileDataList.FirstOrDefault(tileData =>
                VectorHelper.CheckOverlapWithDot(tileData.BottomLeft, tileData.TopRight, coordinate));
        }
        
        public static GridData[] GetNeighborGridDataListByCoordinate(Vector2 coordinate)
        {
            var array = VectorHelper.GetAsArray();
            var tileData = new GridData[array.Length];

            for (var i = 0; i < array.Length; i++)
            {
                var direction = array[i];
                var newCoordinate = coordinate + direction.DirectionToVector();

                tileData[i] = GetGridDataByCoordinate(newCoordinate);
            }

            return tileData;
        }
    }
}