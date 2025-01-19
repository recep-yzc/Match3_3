using System.Collections.Generic;
using System.Linq;
using _Game.Core.Grid.Scripts;

namespace _Game.Utilities.Scripts
{
    public static class DictionaryHelper
    {
        public static void AddTileDataToDictionary(Dictionary<float, List<GridData>> dictionary, float key,
            GridData gridData)
        {
            if (!dictionary.TryGetValue(key, out var tileDataList))
            {
                tileDataList = new List<GridData>();
                dictionary[key] = tileDataList;
            }

            tileDataList.Add(gridData);
        }

        public static void OrderByVertical(Dictionary<float, List<GridData>> verticalTileData)
        {
            foreach (var key in verticalTileData.Keys.ToList())
            {
                var list = verticalTileData[key];
                if (list.Count == 0) continue;
                verticalTileData[key] = list.OrderBy(tileData => tileData?.Coordinate.x).ToList();
            }
        }

        public static void OrderByHorizontal(Dictionary<float, List<GridData>> horizontalTileData)
        {
            foreach (var key in horizontalTileData.Keys.ToList())
            {
                var list = horizontalTileData[key];
                if (list.Count == 0) continue;
                horizontalTileData[key] = list.OrderBy(tileData => tileData?.Coordinate.y).ToList();
            }
        }
    }
}