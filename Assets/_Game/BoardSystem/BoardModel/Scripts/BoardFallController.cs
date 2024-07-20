using System;
using _Game.TileSystem.AbilityModel.Fall.Scripts;
using _Game.TileSystem.GemModel.Scripts;
using UnityEngine;

namespace _Game.BoardSystem.BoardModel.Scripts
{
    public class BoardFallController : MonoBehaviour
    {
        public void TryFall()
        {
            foreach (var horizontalTileData in BoardConstants.HorizontalTileData)
            {
                var tiles = horizontalTileData.Value;

                for (var i = 0; i < tiles.Count; i++)
                {
                    if (tiles[i] is null || !tiles[i].IsEmpty)
                    {
                        continue;
                    }

                    for (var j = i + 1; j < tiles.Count; j++)
                    {
                        if (tiles[j].IsEmpty) continue;
                        var nextGameObject = tiles[j].GameObject;

                        tiles[i].SetGameObject(nextGameObject);
                        tiles[j].SetGameObject(null);

                        tiles[i].SetIsEmpty(false);
                        tiles[j].SetIsEmpty(true);

                        nextGameObject.GetComponent<IFall>().Fall(tiles[i].Coordinate);
                        break;
                    }
                }
            }
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
                
                Gizmos.DrawCube(tileData.Coordinate, Vector3.one*0.5f);
            }
        }
    }
}