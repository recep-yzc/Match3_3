using System.Collections.Generic;
using System.Threading.Tasks;
using _Game.TileSystem.AbilityModel.Blast.Scripts;
using _Game.TileSystem.GemModel.Scripts;
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
            var tileId = TileHelper.GetTileAsComponent<ITile>(tileData.Tile).TileId;

            switch (tileId)
            {
                case TileId.Gem:
                    await HandleForGem(tileData);
                    break;
                case TileId.Wood:
                    break;
            }
        }

        private async Task HandleForGem(TileData tileData)
        {
            var similarGemTiles = await GetSimilarGemTiles(tileData);
            if (similarGemTiles.Count < MinBlastAmount) return;
            similarGemTiles.ForEach(x => ((IBlast)x).Blast());
        }

        private async UniTask<List<IGem>> GetSimilarGemTiles(TileData tileData)
        {
            var gemTile = TileHelper.GetTileAsComponent<IGem>(tileData.Tile);

            var similarTiles = new List<IGem>();
            await FindSimilarGemTiles(similarTiles, tileData, gemTile);
            return similarTiles;
        }

        private async UniTask FindSimilarGemTiles(ICollection<IGem> similarTiles, TileData tileData, IGem gemTile)
        {
            if (gemTile is null || similarTiles.Contains(gemTile)) return;

            similarTiles.Add(gemTile);

            foreach (var nTileData in tileData.NeighborTileData)
            {
                if (nTileData.Tile is null) continue;
                var nGemTile = TileHelper.GetTileAsComponent<IGem>(nTileData.Tile);
                if (nGemTile?.GemId == gemTile.GemId)
                    await FindSimilarGemTiles(similarTiles, nTileData, nGemTile);
            }
        }
    }
}