using System.Collections.Generic;
using System.Threading.Tasks;
using _Game.TileSystem.Elements.Gem.Scripts;
using _Game.TileSystem.Tile.Scripts;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Game.Board.Scripts
{
    [DefaultExecutionOrder(-1)]
    public class BoardViewController : MonoBehaviour
    {
        [Inject] private GemController _gemController;

        public async UniTask TryUpdateView()
        {
            List<TileData> updatedTileList = new();

            foreach (var horizontalTileData in BoardConstants.HorizontalTileDataList)
            foreach (var tileData in horizontalTileData.Value)
            {
                if (updatedTileList.Contains(tileData)) continue;

                if (tileData is null) continue;
                if (tileData.IsEmpty) continue;

                var gem = tileData.GetTileComponents<Gem>();
                if (gem == null) continue;
                var gemId = gem.GetGemId();

                var task = GetSimilarTileDataList(tileData, TileId.Gem);
                await task;

                var sprite = _gemController.GetSprite(gemId, task.Result.Count);

                foreach (var result in task.Result)
                {
                    result.GetTileComponents<Gem>().SetSprite(sprite);
                    updatedTileList.Add(result);
                }
            }
        }

        private async Task<HashSet<TileData>> GetSimilarTileDataList(TileData tileData, TileId tileId)
        {
            var similarTiles = new HashSet<TileData>();

            if (tileId == TileId.Gem) await FindSimilarGemTileData(similarTiles, tileData);

            return similarTiles;
        }

        private async UniTask FindSimilarGemTileData(HashSet<TileData> similarTiles, TileData tileData)
        {
            if (tileData.IsEmpty || tileData.GetTileComponents<IGem>() is not { } tileGem) return;
            if (!similarTiles.Add(tileData)) return;

            foreach (var nTileData in tileData.NeighborTileData)
            {
                if (nTileData is null || nTileData.IsEmpty) continue;

                var nTileGem = nTileData.GetTileComponents<IGem>();
                if (nTileGem?.GetGemId() == tileGem.GetGemId()) await FindSimilarGemTileData(similarTiles, nTileData);
            }
        }
    }
}