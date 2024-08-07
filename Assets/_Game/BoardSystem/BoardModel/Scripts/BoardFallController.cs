using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Game.TileSystem.AbilityModel.Fall.Scripts;
using _Game.TileSystem.EmptyModel.Scripts;
using _Game.TileSystem.GemModel.Scripts;
using _Game.TileSystem.TileModel.Scripts;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Game.BoardSystem.BoardModel.Scripts
{
    public class BoardFallController : MonoBehaviour
    {
        #region Private

        [Inject] private FallDataSo _fallDataSo;

        #endregion

        public async UniTask TryFall()
        {
            var fallTileData = new List<TileData>();
            var fallTasks = new List<UniTask>();

            foreach (var horizontalTileData in BoardConstants.HorizontalTileData)
            {
                var tiles = horizontalTileData.Value;

                for (var i = 0; i < tiles.Count; i++)
                {
                    if (tiles[i] is null || !tiles[i].IsEmpty) continue;

                    for (var j = i + 1; j < tiles.Count; j++)
                    {
                        if (tiles[j].IsEmpty) continue;

                        var bottomTile = tiles[i];
                        var topTile = tiles[j];

                        if (topTile.GetTileComponents<IEmpty>() != null) break;

                        bottomTile.SetGameObject(topTile.GameObject);
                        topTile.SetGameObject(null);

                        bottomTile.SetIsEmpty(false);
                        topTile.SetIsEmpty(true);

                        fallTileData.Add(bottomTile);
                        break;
                    }
                }
            }

            foreach (var tileData in fallTileData)
            {
                var fallTask = tileData.GameObject.GetComponent<IFall>().FallAsync(tileData.Coordinate, _fallDataSo);
                fallTasks.Add(fallTask);
            }

            await UniTask.WhenAll(fallTasks);
        }

        private void OnDrawGizmos()
        {
            foreach (var tileData in BoardConstants.TileData)
            {
                if (tileData.IsEmpty)
                {
                    Gizmos.color = Color.black;
                }
                else
                {
                    var iGem = tileData.GetTileComponents<IGem>();
                    if (iGem != null)
                    {
                        switch (iGem.GemId)
                        {
                            case GemId.Blue:
                                Gizmos.color = Color.blue;
                                break;
                            case GemId.Green:
                                Gizmos.color = Color.green;
                                break;
                            case GemId.Pink:
                                ColorUtility.TryParseHtmlString("0.5f,1f,0.5f", out Color pink);
                                Gizmos.color = pink;
                                break;
                            case GemId.Purple:
                                ColorUtility.TryParseHtmlString("0.5f,0.5f,0.5f", out Color purple);
                                Gizmos.color = purple;
                                break;
                            case GemId.Red:
                                Gizmos.color = Color.red;
                                break;
                            case GemId.Yellow:
                                Gizmos.color = Color.yellow;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }

                Gizmos.DrawCube(tileData.Coordinate, Vector3.one * 0.5f);
            }
        }
    }
}