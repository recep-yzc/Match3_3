using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Level.Scripts.Scriptable;
using _Game.TileSystem.Elements.Empty.Scripts;
using _Game.TileSystem.Elements.Gem.Scripts;
using _Game.TileSystem.Elements.None.Scripts;
using _Game.TileSystem.Tile.Scripts;
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

            foreach (var tileData in levelDataSo.tileDataList)
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

        private void ChangeTileData(TileData tileData)
        {
            if (TileConstants.SelectedElementDataSo == null) return;
            var elementData = TileConstants.SelectedElementDataSo.GetElementData();

            tileData.TileId = elementData.tileId;
            tileData.elementData = elementData;
        }

        public void GenerateLevelDataSo(LevelDataSo dataSo, GenerateType generateType, List<GemId> gemIds)
        {
            dataSo.tileDataList.Clear();
            dataSo.tileDataList = new List<TileData>();

            var halfOfRows = dataSo.rows * 0.5f;
            var halfOfColumns = dataSo.columns * 0.5f;
            var offset = new Vector2(halfOfRows, halfOfColumns) - VectorHelper.HalfSize;

            TileData CreateTileData(int x, int y, TileId tileId, ElementData elementData)
            {
                return new TileData
                {
                    tileId = tileId,
                    elementData = elementData,
                    coordinate = new Vector2(x, y) - offset
                };
            }

            for (var x = 0; x < dataSo.rows; x++)
            for (var y = 0; y < dataSo.columns; y++)
                switch (generateType)
                {
                    case GenerateType.WithNone:
                        var noneElementData = noneElementDataSo.GetElementData();
                        dataSo.tileDataList.Add(CreateTileData(x, y, TileId.None, noneElementData));
                        break;

                    case GenerateType.WithEmpty:

                        var emptyElementData = emptyElementDataSo.GetElementData();
                        dataSo.tileDataList.Add(CreateTileData(x, y, TileId.Empty, emptyElementData));
                        break;

                    case GenerateType.WithSelectedRandomGem:

                        var randomGemId = gemIds[Random.Range(0, gemIds.Count)];
                        var gemElementData =
                            gemElementDataSoList.First(elementDataSo => elementDataSo.data.gemId == randomGemId);

                        dataSo.tileDataList.Add(CreateTileData(x, y, TileId.Gem, gemElementData.data));
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(generateType), generateType, null);
                }
        }
    }
}