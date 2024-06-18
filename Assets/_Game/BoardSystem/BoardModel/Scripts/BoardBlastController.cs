using System;
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
        public async Task TryBlast(TileData tileData)
        {
            await CheckBlast(tileData);
        }

        private async UniTask CheckBlast(TileData tileData)
        {
            var tileId = tileData.Tile.GetComponent<ITile>().TileId;

            switch (tileId)
            {
                case TileId.Gem:
                {
                    var gemId = tileData.Tile.GetComponent<IGem>().GemId;
                    var similarTiles = await GetSimilarGemTile(tileData, gemId);
                    if (similarTiles.Count < 2) return;
                    similarTiles.ForEach(x => ((IBlast)x).Blast());

                    break;
                }
                case TileId.Wood:
                {
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async UniTask<List<IGem>> GetSimilarGemTile(TileData tileData, GemId gemId)
        {
            var similarTiles = new List<IGem>();
            await FindSimilarTileAsComponent(similarTiles, tileData, gemId);
            return similarTiles;
        }

        private async UniTask FindSimilarTileAsComponent<T>(List<T> similarTiles, TileData tileData, GemId gemId)
        {
            var tileAsComponent = GetTileAsComponent<T>(tileData.Tile);
            if (tileAsComponent is null || similarTiles.Contains(tileAsComponent)) return;

            similarTiles.Add(tileAsComponent);

            foreach (var neighborTileData in tileData.NeighborTiles)
            {
                if (neighborTileData is not { Tile: not null }) continue;
                if (neighborTileData.Tile.GetComponent<IGem>()?.GemId == gemId)
                    await FindSimilarTileAsComponent(similarTiles, neighborTileData, gemId);
            }
        }

        private T GetTileAsComponent<T>(GameObject tile)
        {
            return tile.TryGetComponent(out T t) ? t : default;
        }
    }
}