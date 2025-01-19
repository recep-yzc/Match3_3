using System.Collections.Generic;
using System.Threading.Tasks;
using _Game.Core.Abilities.Blast.Scripts;
using _Game.Core.Elements.Element.Scripts;
using _Game.Core.Elements.Gem.Scripts;
using _Game.Core.Grid.Scripts;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Game.Core.Board.Scripts
{
    [DefaultExecutionOrder(-1)]
    public class BoardBlastController : MonoBehaviour
    {
        #region Parameters

        private static int MinBlastAmount => 2;

        #endregion

        public async Task<HashSet<GridData>> TryBlast(GridData gridData)
        {
            if (gridData.GetGridComponents<IBlast>() is null) return default;

            var blastTileDataList =
                await HandleForBlast(gridData, gridData.GetGridComponents<IElement>().GetElementId());
            if (blastTileDataList is null) return default;

            foreach (var blastTileData in blastTileDataList)
            {
                blastTileData.SetIsEmpty(true);
                blastTileData.SetGameObject(null);
            }

            return blastTileDataList;
        }

        private async Task<HashSet<GridData>> HandleForBlast(GridData gridData, ElementId elementId)
        {
            var similarTiles = await GetSimilarTileDataList(gridData, elementId);
            if (similarTiles.Count < MinBlastAmount) return default;

            foreach (var similarTile in similarTiles) similarTile.GetGridComponents<IBlast>().Blast();

            return similarTiles;
        }

        private async UniTask<HashSet<GridData>> GetSimilarTileDataList(GridData gridData, ElementId elementId)
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