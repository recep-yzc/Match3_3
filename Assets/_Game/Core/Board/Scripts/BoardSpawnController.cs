using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Game.Core.Abilities.Fall.Scripts;
using _Game.Core.Elements.Element.Scripts;
using _Game.Core.Elements.Gem.Scripts;
using _Game.Core.Grid.Scripts;
using _Game.Level.Scripts;
using _Game.Level.Scripts.Scriptable;
using _Game.Utilities.Scripts;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Game.Core.Board.Scripts
{
    [DefaultExecutionOrder(-1)]
    public class BoardSpawnController : MonoBehaviour
    {
        #region UnityActions

        [Inject] private LevelReferenceDataSo _levelReferenceDataSo;
        private LevelDataSo _levelDataSo;

        #endregion

        public void FetchLevelDataSo()
        {
            _levelDataSo = _levelReferenceDataSo.GetCurrentLevelDataSo();
        }

        public void ClearTile()
        {
            BoardGlobalValues.TileDataList.Clear();
            BoardGlobalValues.VerticalTileDataList.Clear();
            BoardGlobalValues.HorizontalTileDataList.Clear();
        }
        
        public void CreateGridData()
        {
            foreach (var levelGridData in _levelDataSo.levelGridDataList)
            {
                var elementId = levelGridData.ElementId;
                var coordinate = levelGridData.coordinate;

                var elementGameObject = ElementGlobalValues.FactoryList[elementId].Create(levelGridData);
                if (elementGameObject == null) continue;

                var gridData = new GridData(coordinate, elementGameObject);

                DictionaryHelper.AddTileDataToDictionary(BoardGlobalValues.HorizontalTileDataList, coordinate.x, gridData);
                DictionaryHelper.AddTileDataToDictionary(BoardGlobalValues.VerticalTileDataList, coordinate.y, gridData);
                BoardGlobalValues.TileDataList.Add(gridData);
            }
            
            DictionaryHelper.OrderByHorizontal(BoardGlobalValues.HorizontalTileDataList);
            DictionaryHelper.OrderByVertical(BoardGlobalValues.VerticalTileDataList);
        }

        public void FetchGridNeighbor()
        {
            Parallel.ForEach(BoardGlobalValues.TileDataList, tileData =>
            {
                var neighborTileList = BoardHelper.GetNeighborGridDataListByCoordinate(tileData.Coordinate);
                tileData.SetNeighborGridData(neighborTileList);
            });
        }

        public async UniTask TryCreate()
        {
            foreach (var horizontalTileDataList in BoardGlobalValues.HorizontalTileDataList)
            {
                if (!horizontalTileDataList.Value.Last().IsEmpty) continue;
                var emptyTileGroups = new Dictionary<float, List<GridData>>();

                for (var i = horizontalTileDataList.Value.Count - 1; i >= 0; i--)
                {
                    var currentTileData = horizontalTileDataList.Value[i];
                    if (!currentTileData.IsEmpty)
                    {
                        if (currentTileData.GetGridComponents<IFall>() == null) break;
                        continue;
                    }

                    if (!emptyTileGroups.TryGetValue(currentTileData.Coordinate.x, out var tileGroup))
                    {
                        tileGroup = new List<GridData>();
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
                        var tileLevelData = new LevelGridData
                        {
                            coordinate = currentTileData.Coordinate,
                            ElementId = ElementId.Gem,
                            elementDataBase = new GemElementData
                            {
                                elementId = ElementId.Gem,
                                gemId = (GemId)Random.Range(0, 6)
                            }
                        };

                        var tile = ElementGlobalValues.FactoryList[ElementId.Gem].Create(tileLevelData);
                        tile.transform.position = topTile.Coordinate + Vector2.up * (i + 1);

                        currentTileData.SetHasNeedFall(true);
                        currentTileData.SetGameObject(tile);
                        currentTileData.SetIsEmpty(false);
                    }
                }
            }

            await UniTask.DelayFrame(1);
        }
    }
}