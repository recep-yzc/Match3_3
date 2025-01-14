using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Game.Level.Scripts.Scriptable;
using _Game.TileSystem.Elements.Abilities.Fall.Scripts;
using _Game.TileSystem.Elements.Empty.Scripts;
using _Game.TileSystem.Elements.Gem.Scripts;
using _Game.TileSystem.Elements.Wood.Scripts;
using _Game.TileSystem.Tile.Scripts;
using _Game.Utilities.Scripts;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;
using TileData = _Game.Level.Scripts.TileData;

namespace _Game.Board.Scripts
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
            var tileLevelData = _levelDataSo.tileDataList;

            foreach (var data in tileLevelData)
            {
                GameObject tile = null;
                TileSystem.Tile.Scripts.TileData tileData = null;

                var tileId = data.TileId;
                var coordinate = data.coordinate;

                tile = TileConstants.FactoryList[tileId].Create(data);
                if (tile == null) continue;
                tileData = new TileSystem.Tile.Scripts.TileData(coordinate, tile);

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
                if (!horizontalTileDataList.Value.Last().IsEmpty) continue;
                var emptyTileGroups = new Dictionary<float, List<TileSystem.Tile.Scripts.TileData>>();

                for (var i = horizontalTileDataList.Value.Count - 1; i >= 0; i--)
                {
                    var currentTileData = horizontalTileDataList.Value[i];
                    if (!currentTileData.IsEmpty)
                    {
                        if (currentTileData.GetTileComponents<IFall>() == null) break;
                        continue;
                    }

                    if (!emptyTileGroups.TryGetValue(currentTileData.Coordinate.x, out var tileGroup))
                    {
                        tileGroup = new List<TileSystem.Tile.Scripts.TileData>();
                        emptyTileGroups[currentTileData.Coordinate.x] = tileGroup;
                    }

                    tileGroup.Add(currentTileData);
                }

                foreach (var tileGroup in emptyTileGroups) tileGroup.Value.Reverse();

                foreach (var tileGroup in emptyTileGroups.Values)
                {
                    var topTile = tileGroup.Last();

                    for (var i = 0; i < tileGroup.Count; i++)
                    {
                        var currentTileData = tileGroup[i];
                        var tileLevelData = new TileData
                        {
                            coordinate = currentTileData.Coordinate,
                            TileId = TileId.Gem,
                            elementData = new GemElementData
                            {
                                tileId = TileId.Gem,
                                gemId = (GemId)Random.Range(0, 6)
                            }
                        };

                        var tile = _gemFactory.Create(tileLevelData);
                        tile.transform.position = topTile.Coordinate + Vector2.up * (i + 1);

                        currentTileData.SetHasNeedFall(true);
                        currentTileData.SetGameObject(tile);
                        currentTileData.SetIsEmpty(false);
                    }
                }
            }

            await UniTask.DelayFrame(1);
        }

        private TileSystem.Tile.Scripts.TileData[] GetNeighborTiles(Vector2 coordinate)
        {
            var array = VectorHelper.GetAsArray();
            var tileData = new TileSystem.Tile.Scripts.TileData[array.Length];

            for (var i = 0; i < array.Length; i++)
            {
                var direction = array[i];
                var newCoordinate = coordinate + direction.DirectionToVector();

                tileData[i] = BoardHelper.GetTileDataByCoordinate(newCoordinate);
            }

            return tileData;
        }

        private void FetchGridDataSo()
        {
            _levelDataSo = _levelReferenceDataSo.GetCurrentLevelDataSo();
        }

        #region Parameters

        #region Level

        [Inject] private LevelReferenceDataSo _levelReferenceDataSo;
        private LevelDataSo _levelDataSo;

        #endregion

        #region Ability

        [Inject] private GemFactory _gemFactory;
        [Inject] private WoodFactory _woodFactory;
        [Inject] private EmptyFactory _emptyFactory;

        #endregion

        #endregion
    }
}