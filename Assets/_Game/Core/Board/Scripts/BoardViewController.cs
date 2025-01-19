using System.Collections.Generic;
using System.Threading.Tasks;
using _Game.Core.Elements.Element.Scripts;
using _Game.Core.Elements.Gem.Scripts;
using _Game.Core.Grid.Scripts;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Game.Core.Board.Scripts
{
    [DefaultExecutionOrder(-1)]
    public class BoardViewController : MonoBehaviour
    {
        [Inject] private GemController _gem;

        public async UniTask TryUpdateView()
        {
            List<GridData> updatedTileList = new();

            foreach (var horizontalTileData in BoardGlobalValues.HorizontalTileDataList)
            foreach (var tileData in horizontalTileData.Value)
            {
                if (updatedTileList.Contains(tileData)) continue;

                if (tileData is null) continue;
                if (tileData.IsEmpty) continue;

                var gem = tileData.GetGridComponents<Gem>();
                if (gem == null) continue;
                var gemId = gem.GetGemId();

                var task = GetSimilarTileDataList(tileData, ElementId.Gem);
                await task;

                var sprite = _gem.GetSprite(gemId, task.Result.Count);

                foreach (var result in task.Result)
                {
                    result.GetGridComponents<Gem>().SetSprite(sprite);
                    updatedTileList.Add(result);
                }
            }
        }

        private async Task<HashSet<GridData>> GetSimilarTileDataList(GridData gridData, ElementId elementId)
        {
            var similarTiles = new HashSet<GridData>();

            if (elementId == ElementId.Gem) await FindSimilarGemTileData(similarTiles, gridData);

            return similarTiles;
        }

        private async UniTask FindSimilarGemTileData(HashSet<GridData> similarTiles, GridData gridData)
        {
            if (gridData.IsEmpty || gridData.GetGridComponents<IGem>() is not { } tileGem) return;
            if (!similarTiles.Add(gridData)) return;

            foreach (var nTileData in gridData.NeighborGridData)
            {
                if (nTileData is null || nTileData.IsEmpty) continue;

                var nTileGem = nTileData.GetGridComponents<IGem>();
                if (nTileGem?.GetGemId() == tileGem.GetGemId()) await FindSimilarGemTileData(similarTiles, nTileData);
            }
        }
    }
}