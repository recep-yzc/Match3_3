using System.Collections.Generic;
using _Game.Core.Abilities.Fall.Scripts;
using _Game.Core.Elements.Empty.Scripts;
using _Game.Core.Grid.Scripts;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Game.Core.Board.Scripts
{
    [DefaultExecutionOrder(-1)]
    public class BoardFallController : MonoBehaviour
    {
        #region Parameters

        [Inject] private FallDataSo _fallDataSo;

        #endregion

        public async UniTask TryFall()
        {
            var fallTileData = new List<GridData>();
            var fallTasks = new List<UniTask>();

            foreach (var horizontalTileData in BoardGlobalValues.HorizontalTileDataList)
            {
                var tiles = horizontalTileData.Value;
                for (var i = 0; i < tiles.Count; i++)
                {
                    var currentTile = tiles[i];
                    if (currentTile is null || !currentTile.IsEmpty)
                    {
                        if (currentTile!.HasNeedFall) fallTileData.Add(currentTile);
                        continue;
                    }

                    for (var j = i + 1; j < tiles.Count; j++)
                    {
                        var nextTile = tiles[j];

                        if (nextTile.IsEmpty) continue;
                        if (nextTile.GetGridComponents<IEmpty>() != null) break;
                        if (nextTile.GetGridComponents<IFall>() == null) break;

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
                var fallTask = tileData.GetGridComponents<IFall>().FallAsync(tileData.Coordinate, _fallDataSo);
                tileData.SetHasNeedFall(false);
                fallTasks.Add(fallTask);
            }

            await UniTask.WhenAll(fallTasks);
        }
    }
}