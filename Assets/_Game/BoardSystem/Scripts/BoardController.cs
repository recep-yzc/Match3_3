using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Game.GridSystem.Scripts;
using _Game.TileSystem.DirectionModel.Scripts;
using _Game.TileSystem.EmptyModel.Scripts;
using _Game.TileSystem.GemModel.Scripts;
using _Game.TileSystem.TileModel.Scripts;
using _Game.TileSystem.WoodModel.Scripts;
using _Game.UtilitySystem.Scripts;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Game.BoardSystem.Scripts
{
    public class BoardController : MonoBehaviour
    {
        private void Start()
        {
            Application.targetFrameRate = 60;
            FetchGridDataSo();
            ClearTile();
            CreateTile();
            FetchNeighbor();
        }
        
        private void ClearTile()
        {
            BoardConstants.TileDataList.Clear();
            BoardConstants.VerticalTileDataList.Clear();
            BoardConstants.HorizontalTileDataList.Clear();
        }

        private void CreateTile()
        {
            var tileLevelData = _gridDataSo.tileLevelData;

            foreach (var data in tileLevelData)
            {
                GameObject tile = null;
                TileData tileData = null;

                var tileId = data.tileId;
                var coordinate = data.coordinate;

                switch (tileId)
                {
                    case TileId.Empty:
                        tile = _emptyFactory.CreateTile(data);
                        tileData = new TileData(coordinate, tile);
                        break;
                    case TileId.Gem:
                        tile = _gemFactory.CreateTile(data);
                        tileData = new TileData(coordinate, tile);
                        break;
                    case TileId.Wood:
                        break;
                }

                DictionaryHelper.AddTileDataToDictionary(BoardConstants.HorizontalTileDataList, coordinate.x, tileData);
                DictionaryHelper.AddTileDataToDictionary(BoardConstants.VerticalTileDataList, coordinate.y, tileData);
                BoardConstants.TileDataList.Add(tileData);
            }

            DictionaryHelper.OrderByHorizontal(BoardConstants.HorizontalTileDataList);
            DictionaryHelper.OrderByVertical(BoardConstants.VerticalTileDataList);
        }

        private void FetchNeighbor()
        {
            Parallel.ForEach(BoardConstants.TileDataList, tileData =>
            {
                var neighborTileList = GetNeighborTiles(tileData.Coordinate);
                tileData.SetNeighborTileData(neighborTileList);
            });
        }

        public async UniTask TryCreate()
        {
            foreach (var horizontalTileDataList in BoardConstants.HorizontalTileDataList)
            {
                var emptyTileGroups = new Dictionary<float, List<TileData>>();

                foreach (var tileData in horizontalTileDataList.Value)
                {
                    if (!tileData.IsEmpty) continue;
                    
                    if (!emptyTileGroups.TryGetValue(tileData.Coordinate.x, out var tileGroup))
                    {
                        tileGroup = new List<TileData>();
                        emptyTileGroups[tileData.Coordinate.x] = tileGroup;
                    }
                    tileGroup.Add(tileData);
                }

                foreach (var tileGroup in emptyTileGroups.Values)
                {
                    var topTile = tileGroup.Last();

                    for (var i = 0; i < tileGroup.Count; i++)
                    {
                        var currentTileData = tileGroup[i];

                        var tileLevelData = new TileLevelData
                        {
                            tileId = (TileId)Random.Range(0, 4),
                            coordinate = currentTileData.Coordinate,
                            gemId = (GemId)Random.Range(0, 6),
                        };

                        var tile = _gemFactory.CreateTile(tileLevelData);
                        tile.transform.position = topTile.Coordinate + Vector2.up * (i + 1);

                        currentTileData.SetHasNeedFall(true);
                        currentTileData.SetGameObject(tile);
                        currentTileData.SetIsEmpty(false);
                    }
                }
            }

            await UniTask.DelayFrame(1);
        }
        
        private TileData[] GetNeighborTiles(Vector2 coordinate)
        {
            var array = DirectionHelper.GetAsArray();
            var tileData = new TileData[array.Length];

            for (var i = 0; i < array.Length; i++)
            {
                var direction = array[i];
                var newCoordinate = coordinate + direction.ToVector();

                tileData[i] = BoardHelper.GetTileDataByCoordinate(newCoordinate);
            }

            return tileData;
        }

        private void FetchGridDataSo()
        {
            _gridDataSo = _gridLevelDataSo.GetCurrentGridDataSo();
        }

        #region Parameters

        #region Level

        [Inject] private GridLevelDataSo _gridLevelDataSo;
        private GridDataSo _gridDataSo;

        #endregion

        #region Ability

        [Inject] private GemFactory _gemFactory;
        [Inject] private WoodFactory _woodFactory;
        [Inject] private EmptyFactory _emptyFactory;

        #endregion

        #endregion
    }
}