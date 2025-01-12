using System.Collections.Generic;
using _Game.TileSystem.AbilityModel.Fall.Scripts;
using _Game.TileSystem.EmptyModel.Scripts;
using _Game.TileSystem.TileModel.Scripts;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Game.BoardSystem.Scripts
{
    public class BoardFallController : MonoBehaviour
    {
        #region Parameters

        [Inject] private FallDataSo _fallDataSo;

        #endregion

        public async UniTask TryFall()
        {
            var fallTileData = new List<TileData>();
            var fallTasks = new List<UniTask>();

            foreach (var horizontalTileData in BoardConstants.HorizontalTileDataList)
            {
                var tiles = horizontalTileData.Value;
                for (var i = 0; i < tiles.Count; i++)
                {
                    var currentTile = tiles[i];
                    if (currentTile is null || !currentTile.IsEmpty)
                    {
                        if (currentTile!.HasNeedFall)
                        {
                            fallTileData.Add(currentTile);
                        }
                        continue;
                    }

                    for (var j = i + 1; j < tiles.Count; j++)
                    {
                        var nextTile = tiles[j];

                        if (nextTile.IsEmpty) continue;
                        if (nextTile.GetTileComponents<IEmpty>() != null) break;

                        currentTile.SetGameObject(nextTile.GameObject);
                        nextTile.SetGameObject(null);

                        currentTile.SetIsEmpty(false);
                        nextTile.SetIsEmpty(true);

                        fallTileData.Add(currentTile);
                        break;
                    }
                }
            }

            foreach (var tileData in fallTileData)
            {
                var fallTask = tileData.GameObject.GetComponent<IFall>().FallAsync(tileData.Coordinate, _fallDataSo);
                tileData.SetHasNeedFall(false);
                fallTasks.Add(fallTask);
            }

            await UniTask.WhenAll(fallTasks);
        }
    }
}