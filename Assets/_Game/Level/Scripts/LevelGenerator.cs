using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Core.Elements.Element.Scripts;
using _Game.Core.Elements.Empty.Scripts;
using _Game.Core.Elements.Gem.Scripts;
using _Game.Core.Elements.None.Scripts;
using _Game.Core.Grid.Scripts;
using _Game.Level.Scripts.Scriptable;
using _Game.Utilities.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace _Game.Level.Scripts
{
    public class LevelGenerator : Singleton<LevelGenerator>
    {
        [Header("NoneElementData")] [SerializeField]
        private NoneElementDataSo noneElementDataSo;

        [Header("EmptyElementData")] [SerializeField]
        private EmptyElementDataSo emptyElementDataSo;

        [Header("GemElementDataList")] [SerializeField]
        private List<GemElementDataSo> gemElementDataSoList;

        [Header("References")] public LevelDataSo levelDataSo;
        [SerializeField] [ReadOnly] private bool isEditScene;

        #region Parameters

        private Camera _mainCamera;

        #endregion

        private void Update()
        {
            if (!isEditScene) return;
            if (!Input.GetMouseButtonDown(0)) return;

            var mousePosition = Input.mousePosition;
            mousePosition.z = _mainCamera.transform.position.y;

            var inputPosition = _mainCamera.ScreenToWorldPoint(mousePosition);

            foreach (var tileData in levelDataSo.levelGridDataList)
            {
                var bottomLeft = tileData.coordinate - VectorHelper.HalfSize;
                var topRight = tileData.coordinate + VectorHelper.HalfSize;

                var isDotIn = VectorHelper.CheckOverlapWithDot(bottomLeft, topRight, inputPosition);
                if (!isDotIn) continue;

                ChangeTileData(tileData);

                levelDataSo.Save();
                break;
            }
        }

        private void OnValidate()
        {
            isEditScene = SceneManager.GetActiveScene().name.Contains("EditScene");
            _mainCamera = Camera.main;
        }

        private void ChangeTileData(LevelGridData levelGridData)
        {
            if (GridGlobalValues.SelectedElementDataBaseSo == null) return;
            var elementData = GridGlobalValues.SelectedElementDataBaseSo.GetElementDataBase();

            levelGridData.ElementId = elementData.elementId;
            levelGridData.elementDataBase = elementData;
        }

        public void GenerateLevelDataSo(LevelDataSo dataSo, GenerateType generateType, List<GemId> gemIds)
        {
            dataSo.levelGridDataList.Clear();
            dataSo.levelGridDataList = new List<LevelGridData>();

            var halfOfRows = dataSo.rows * 0.5f;
            var halfOfColumns = dataSo.columns * 0.5f;
            var offset = new Vector2(halfOfRows, halfOfColumns) - VectorHelper.HalfSize;

            LevelGridData CreateTileData(int x, int y, ElementId tileId, ElementDataBase elementData)
            {
                return new LevelGridData
                {
                    elementId = tileId,
                    elementDataBase = elementData,
                    coordinate = new Vector2(x, y) - offset
                };
            }

            for (var x = 0; x < dataSo.rows; x++)
            for (var y = 0; y < dataSo.columns; y++)
                switch (generateType)
                {
                    case GenerateType.WithNone:
                        var noneElementData = noneElementDataSo.GetElementDataBase();
                        dataSo.levelGridDataList.Add(CreateTileData(x, y, ElementId.None, noneElementData));
                        break;

                    case GenerateType.WithEmpty:

                        var emptyElementData = emptyElementDataSo.GetElementDataBase();
                        dataSo.levelGridDataList.Add(CreateTileData(x, y, ElementId.Empty, emptyElementData));
                        break;

                    case GenerateType.WithSelectedRandomGem:

                        var randomGemId = gemIds[Random.Range(0, gemIds.Count)];
                        var gemElementData =
                            gemElementDataSoList.First(elementDataSo => elementDataSo.data.gemId == randomGemId);

                        dataSo.levelGridDataList.Add(CreateTileData(x, y, ElementId.Gem, gemElementData.data));
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(generateType), generateType, null);
                }
        }
    }
}