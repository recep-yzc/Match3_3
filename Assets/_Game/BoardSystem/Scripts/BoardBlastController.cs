using System.Collections.Generic;
using System.Threading.Tasks;
using _Game.TileSystem.AbilityModel.Blast.Scripts;
using _Game.TileSystem.GemModel.Scripts;
using _Game.TileSystem.TileModel.Scripts;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Game.BoardSystem.Scripts
{
    public class BoardBlastController : MonoBehaviour
    {
        #region Parameters

        private static int MinBlastAmount => 2;

        #endregion

        public async Task<HashSet<TileData>> TryBlast(TileData tileData)
        {
            if (tileData.GetTileComponents<IBlast>() is null) return default;

            var blastTileDataList = await HandleForBlast(tileData, tileData.GetTileComponents<ITile>().TileId);
            if (blastTileDataList is null) return default;

            foreach (var blastTileData in blastTileDataList)
            {
                blastTileData.SetIsEmpty(true);
                blastTileData.SetGameObject(null);
            }

            return blastTileDataList;
        }

        private async Task<HashSet<TileData>> HandleForBlast(TileData tileData, TileId tileId)
        {
            var similarTiles = await GetSimilarTileDataList(tileData, tileId);
            if (similarTiles.Count < MinBlastAmount) return default;

            foreach (var similarTile in similarTiles) similarTile.GetTileComponents<IBlast>().Blast();

            return similarTiles;
        }

        private async UniTask<HashSet<TileData>> GetSimilarTileDataList(TileData tileData, TileId tileId)
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
                if (nTileGem?.GemId == tileGem.GemId) await FindSimilarGemTileData(similarTiles, nTileData);
            }
        }
    }
}