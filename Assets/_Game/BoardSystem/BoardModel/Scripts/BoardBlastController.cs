using System.Collections.Generic;
using System.Threading.Tasks;
using _Game.TileSystem.TileModel.Scripts;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Game.BoardSystem.BoardModel.Scripts
{
    public class BoardBlastController : MonoBehaviour
    {
        #region Private

        private static int MinBlastAmount => 2;

        #endregion

        public async Task TryBlast(TileData tileData)
        {
            if (tileData.Blast is null) return;
            await HandleForBlast(tileData, tileData.Tile.TileId);
        }

        private async Task HandleForBlast(TileData tileData, TileId tileId)
        {
            var similarGemTiles = await GetSimilarTiles(tileData, tileId);
            if (similarGemTiles.Count < MinBlastAmount) return;

            foreach (var similarGemTile in similarGemTiles) similarGemTile.Blast.Blast();
        }

        private async UniTask<List<TileData>> GetSimilarTiles(TileData tileData, TileId tileId)
        {
            var similarTiles = new List<TileData>();

            switch (tileId)
            {
                case TileId.Gem:
                    await FindSimilarGemTiles(similarTiles, tileData);
                    break;
                case TileId.Wood:
                    await FindSimilarWoodTiles(similarTiles, tileData);
                    break;
            }

            return similarTiles;
        }

        private async UniTask FindSimilarGemTiles(ICollection<TileData> similarTiles, TileData tileData)
        {
            if (tileData.Gem is null) return;
            if (similarTiles.Contains(tileData)) return;

            similarTiles.Add(tileData);

            foreach (var nTileData in tileData.NeighborTileData)
                if (nTileData?.Gem?.GemId == tileData.Gem.GemId)
                    await FindSimilarGemTiles(similarTiles, nTileData);
        }

        private async UniTask FindSimilarWoodTiles(ICollection<TileData> similarTiles, TileData tileData)
        {
            if (tileData.Wood is null) return;
            if (similarTiles.Contains(tileData)) return;

            similarTiles.Add(tileData);

            foreach (var nTileData in tileData.NeighborTileData)
                if (nTileData?.Wood is not null)
                    await FindSimilarWoodTiles(similarTiles, nTileData);
        }
    }
}