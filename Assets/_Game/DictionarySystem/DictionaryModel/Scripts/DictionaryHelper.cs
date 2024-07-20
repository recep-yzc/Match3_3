using System.Collections.Generic;
using System.Linq;
using _Game.TileSystem.TileModel.Scripts;

namespace _Game.DictionarySystem.DictionaryModel.Scripts
{
    public static class DictionaryHelper
    {
        public static void AddTileDataToDictionary(Dictionary<float, List<TileData>> dictionary, float key,
            TileData tileData)
        {
            if (!dictionary.TryGetValue(key, out var tileDataList))
            {
                tileDataList = new List<TileData>();
                dictionary[key] = tileDataList;
            }

            tileDataList.Add(tileData);
        }

        public static void OrderByVertical(Dictionary<float, List<TileData>> verticalTileData)
        {
            foreach (var key in verticalTileData.Keys.ToList())
            {
                var list = verticalTileData[key];
                if (list.Count == 0) continue;
                verticalTileData[key] = list.OrderBy(tileData => tileData?.Coordinate.x).ToList();
            }
        }

        public static void OrderByHorizontal(Dictionary<float, List<TileData>> horizontalTileData)
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