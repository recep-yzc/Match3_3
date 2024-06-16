using System;
using System.Collections.Generic;
using System.Threading;
using _Game.GridSystem.GridModel.Scripts;
using _Game.TileSystem.AbilityModel.Blast.Scripts;
using _Game.TileSystem.AbilityModel.ScaleUpDown.Scripts;
using _Game.TileSystem.AbilityModel.Shake.Scripts;
using _Game.TileSystem.GemModel.Scripts;
using _Game.TileSystem.TileModel.Scripts;
using _Game.TileSystem.WoodModel.Scripts;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEditor.VersionControl;
using UnityEngine;
using Zenject;
using Task = System.Threading.Tasks.Task;

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
            FetchCancellationToken();
            FetchCameraData();
            FetchGridDataSo();
            FetchLevelProperties();
            CreateTile();
        }

        #endregion

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;

            var inputPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            HandleTileClick(inputPosition).Forget();
        }

        private async UniTask HandleTileClick(Vector3 inputPosition)
        {
            foreach (var tileData in _tileDataList)
            {
                var isDotIn = GridHelper.CheckOverlapWithDot(tileData.BottomLeft, tileData.TopRight, inputPosition);
                if (!isDotIn) continue;

                if (tileData.Tile.TryGetComponent(out IBlast blast))
                {
                    var sameTileList = await GetSameTile<IBlast>(inputPosition);
                    foreach (var sameTile in sameTileList)
                    {
                        await UniTask.DelayFrame(1, PlayerLoopTiming.TimeUpdate, _destroyToken);
                        sameTile.Blast();
                    }

                    break;
                }

                if (tileData.Tile.TryGetComponent(out IShake shake))
                    shake.ShakeAsync(_shakeDataSo.duration, _shakeDataSo.force, _shakeDataSo.animationCurve).Forget();
                if (tileData.Tile.TryGetComponent(out IScaleUpDown scaleUpDown))
                    scaleUpDown.ScaleUpDownAsync(_scaleUpDownDataSo.duration, _scaleUpDownDataSo.force,
                        _scaleUpDownDataSo.animationCurve).Forget();

                break;
            }
        }

        private async UniTask<List<T>> GetSameTile<T>(Vector2 coordinate)
        {
            var sameTileList = new List<T>();
            await FindSameTile(sameTileList, coordinate);
            return sameTileList;
        }

        private async UniTask FindSameTile<T>(List<T> sameTileList, Vector2 coordinate)
        {
            var result = GetTileAsComponent<T>(coordinate);
            if (result is not { } tile || sameTileList.Contains(tile)) return;

            await UniTask.DelayFrame(1, PlayerLoopTiming.TimeUpdate, _destroyToken);
            sameTileList.Add(tile);
            
            foreach (var direction in DirectionHelper.GetAsArray())
            {
                var newCoordinate = coordinate + direction.ToVector();
                await FindSameTile(sameTileList, newCoordinate);
            }
        }

        private T GetTileAsComponent<T>(Vector2 coordinate)
        {
            foreach (var tileData in _tileDataList)
            {
                var isDotIn = GridHelper.CheckOverlapWithDot(tileData.BottomLeft, tileData.TopRight, coordinate);
                if (isDotIn)
                {
                    return tileData.Tile.GetComponent<T>();
                }
            }

            return default;
        }

        private void CreateTile()
        {
            for (var i = 0; i < _gridList.Count; i++)
            {
                var coordinate = _gridList[i];
                var iTile = i <= (int)(_gridList.Count * 0.5f)
                    ? woodFactory.CreateTile(coordinate)
                    : gemFactory.CreateTile(coordinate);
                var bottomLeft = coordinate - _halfGridSize;
                var topRight = coordinate + _halfGridSize;

                _tileDataList.Add(new TileData(coordinate, iTile, bottomLeft, topRight));
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

        private void FetchCancellationToken()
        {
            _destroyToken = this.GetCancellationTokenOnDestroy();
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

        private CancellationToken _destroyToken;
        private Camera _mainCamera;
        private readonly List<Vector2> _gridList = new();
        private readonly List<TileData> _tileDataList = new();
        private readonly Vector2 _halfGridSize = new(0.5f, 0.5f);

        #endregion
    }
}