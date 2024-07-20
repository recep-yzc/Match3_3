using System.Collections.Generic;
using System.Threading.Tasks;
using _Game.TileSystem.AbilityModel.Blast.Scripts;
using _Game.TileSystem.GemModel.Scripts;
using _Game.TileSystem.TileModel.Scripts;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace _Game.BoardSystem.BoardModel.Scripts
{
    public class BoardBlastController : MonoBehaviour
    {
        #region Private

        private static int MinBlastAmount => 2;

        #endregion

        public async Task<List<TileData>> TryBlast(TileData tileData)
        {
            if (tileData.GetTileComponents<IBlast>() is null) return null;
            var result = await HandleForBlast(tileData, tileData.GetTileComponents<ITile>().TileId);
            return result;
        }

        private async Task<List<TileData>> HandleForBlast(TileData tileData, TileId tileId)
        {
            var similarTiles = await GetSimilarTiles(tileData, tileId);
            if (similarTiles.Count < MinBlastAmount) return null;

            foreach (var similarTile in similarTiles)
            {
                similarTile.GetTileComponents<IBlast>().Blast();
            }

            return similarTiles;
        }

        private async UniTask<List<TileData>> GetSimilarTiles(TileData tileData, TileId tileId)
        {
            var similarTiles = new List<TileData>();

            if (tileId == TileId.Gem)
            {
                await FindSimilarGemTiles(similarTiles, tileData);
            }

            return similarTiles;
        }

        private async UniTask FindSimilarGemTiles(ICollection<TileData> similarTiles, TileData tileData)
        {
            if (tileData.IsEmpty) return;
            if (tileData.GetTileComponents<IGem>() is null) return;

            if (similarTiles.Contains(tileData)) return;

            similarTiles.Add(tileData);

            foreach (var nTileData in tileData.NeighborTileData)
            {
                if (nTileData is null) continue;
                if (nTileData.IsEmpty) continue;

                var nTileGemId = nTileData.GetTileComponents<IGem>()?.GemId;
                var tileGemId = tileData.GetTileComponents<IGem>()?.GemId;
                
                if (nTileGemId == tileGemId)
                    await FindSimilarGemTiles(similarTiles, nTileData);
            }
        }
    }
}