using System.Collections.Generic;
using _Game.GridSystem.GridModel.Scripts;
using _Game.TileSystem.AbilityModel.ScaleUpDown.Scripts;
using _Game.TileSystem.AbilityModel.Shake.Scripts;
using _Game.TileSystem.GemModel.Scripts;
using _Game.TileSystem.TileModel.Scripts;
using _Game.TileSystem.WoodModel.Scripts;
using UnityEngine;
using Zenject;

namespace _Game.BoardSystem.BoardModel.Scripts
{
    public class BoardController : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private GemFactory gemFactory;

        [SerializeField] private WoodFactory woodFactory;

        #region First

        private void Start()
        {
            FetchCameraData();
            FetchGridDataSo();
            FetchLevelProperties();
            CreateGridTile();
        }

        #endregion

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            var inputPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

            foreach (var tileData in _tileDataList)
            {
                var isDotIn = GridHelper.CheckOverlapWithDot(tileData.BottomLeft, tileData.TopRight, inputPosition);
                if (!isDotIn) continue;

                if (tileData.Tile.TryGetComponent(out IShake shake))
                    shake.Shake(_shakeDataSo.duration, _shakeDataSo.force, _shakeDataSo.animationCurve);
                if (tileData.Tile.TryGetComponent(out IScaleUpDown scaleUpDown))
                    scaleUpDown.ScaleUpDown(_scaleUpDownDataSo.duration, _scaleUpDownDataSo.force, _scaleUpDownDataSo.animationCurve);

                break;
            }
        }

        private void FetchCameraData()
        {
            _mainCamera = Camera.main;
        }

        private void FetchLevelProperties()
        {
            _gridList.Clear();
            _gridList.AddRange(_gridDataSo.playableGridList);
        }

        private void FetchGridDataSo()
        {
            _gridDataSo = _gridLevelDataSo.GetCurrentGridDataSo();
        }

        private void CreateGridTile()
        {
            for (var i = 0; i < _gridList.Count; i++)
            {
                var coordinate = _gridList[i];
                var iTile = i <= 10 ? woodFactory.CreateTile(coordinate) : gemFactory.CreateTile(coordinate);
                var bottomLeft = coordinate - _halfGridSize;
                var topRight = coordinate + _halfGridSize;

                _tileDataList.Add(new TileData(coordinate, iTile, bottomLeft, topRight));
            }
        }

        #region Private

        #region Ability

        [Inject] private ShakeDataSo _shakeDataSo;
        [Inject] private ScaleUpDownDataSo _scaleUpDownDataSo;

        #endregion

        #region Level

        [Inject] private GridLevelDataSo _gridLevelDataSo;
        private GridDataSo _gridDataSo;

        #endregion

        private Camera _mainCamera;
        private readonly List<Vector2> _gridList = new();
        private readonly List<TileData> _tileDataList = new();
        private readonly Vector2 _halfGridSize = new(0.5f, 0.5f);

        #endregion
    }
}