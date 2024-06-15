using System.Collections.Generic;
using _Game.GridSystem.GridModel.Scripts.Scriptable;
using _Game.GridSystem.GridModel.Scripts.Utilities;
using _Game.TileSystem.AbilityModel.Shake;
using _Game.TileSystem.AbilityModel.Shake.Scripts;
using _Game.TileSystem.TileModel.Scripts;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace _Game.GridSystem.GridModel.Scripts.Controllers
{
    public class GridController : MonoBehaviour
    {
        #region Private

        [Inject] private ShakeDataSo _shakeDataSo;
        [Inject] private DiContainer _diContainer;
        [Inject] private Tile _tilePrefab;
        [Inject] private GridLevelDataSo _gridLevelDataSo;
        private GridDataSo _gridDataSo;
        private Camera _mainCamera;
        private readonly List<Vector2> _gridList = new();
        private readonly List<TileData> _tileDataList = new();
        private readonly Vector2 _halfGridSize = new(0.5f, 0.5f);

        #endregion

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
                {
                    shake.Shake(_shakeDataSo.duration, _shakeDataSo.force, _shakeDataSo.animationCurve).Forget();
                }
                
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

        void CreateGridTile()
        {
            foreach (var coordinate in _gridList)
            {
                var tileHandler = _diContainer.InstantiatePrefabForComponent<Tile>(_tilePrefab, coordinate, quaternion.identity, null);

                var bottomLeft = coordinate - _halfGridSize;
                var topRight = coordinate + _halfGridSize;

                _tileDataList.Add(new TileData(coordinate, tileHandler, bottomLeft, topRight));
            }
        }
    }
}