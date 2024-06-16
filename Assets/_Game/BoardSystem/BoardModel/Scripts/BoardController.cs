using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using _Game.GridSystem.GridModel.Scripts;
using _Game.TileSystem.AbilityModel.Blast.Scripts;
using _Game.TileSystem.AbilityModel.ScaleUpDown.Scripts;
using _Game.TileSystem.AbilityModel.Shake.Scripts;
using _Game.TileSystem.GemModel.Scripts;
using _Game.TileSystem.TileModel.Scripts;
using _Game.TileSystem.WoodModel.Scripts;
using Cysharp.Threading.Tasks;
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
            var result = GetTileDataByCoordinate(inputPosition);
            if (result.HasValue)
            {
                var tileData = result.Value;
                var tile = tileData.Tile;

                await CheckBlast(inputPosition, tile);

                CheckShake(tile);
                CheckScaleUpDown(tile, tileData);
            }
        }

        private void CheckScaleUpDown(Tile tile, TileData tileData)
        {
            if (!tile.TryGetComponent(out IScaleUpDown scaleUpDown)) return;
            foreach (var neighbor in tileData.NeighborTiles)
            {
                if (neighbor?.Tile?.TryGetComponent(out IScaleUpDown neighborScaleUpDown) == true)
                {
                    neighborScaleUpDown.ScaleUpDownAsync(_scaleUpDownDataSo.duration, _scaleUpDownDataSo.force,
                        _scaleUpDownDataSo.animationCurve).Forget();
                }
            }

            scaleUpDown.ScaleUpDownAsync(_scaleUpDownDataSo.duration, _scaleUpDownDataSo.force,
                _scaleUpDownDataSo.animationCurve).Forget();
        }

        private void CheckShake(Tile tile)
        {
            if (!tile.TryGetComponent(out IShake shake)) return;
            shake.ShakeAsync(_shakeDataSo.duration, _shakeDataSo.force, _shakeDataSo.animationCurve).Forget();
        }

        private async UniTask CheckBlast(Vector3 inputPosition, Tile tile)
        {
            if (!tile.TryGetComponent(out IBlast blast))
                return;

            var sameTileList = await GetSimilarTileAsComponent<IBlast>(inputPosition);
            foreach (var sameTile in sameTileList)
            {
                await UniTask.DelayFrame(1, PlayerLoopTiming.TimeUpdate, _destroyToken);
                sameTile.Blast();
            }
        }

        private async UniTask<List<T>> GetSimilarTileAsComponent<T>(Vector2 coordinate)
        {
            var sameTileList = new List<T>();
            await FindSimilarTileAsComponent(sameTileList, coordinate);
            return sameTileList;
        }

        private async UniTask FindSimilarTileAsComponent<T>(List<T> sameTileList, Vector2 coordinate)
        {
            var result = GetTileAsComponent<T>(coordinate);
            if (result is null || sameTileList.Contains(result)) return;

            await UniTask.DelayFrame(1, PlayerLoopTiming.TimeUpdate, _destroyToken);
            sameTileList.Add(result);

            foreach (var direction in DirectionHelper.GetAsArray())
            {
                var newCoordinate = coordinate + direction.ToVector();
                await FindSimilarTileAsComponent(sameTileList, newCoordinate);
            }
        }

        private T GetTileAsComponent<T>(Vector2 coordinate)
        {
            var tileData = GetTileDataByCoordinate(coordinate);
            if (tileData?.Tile is null) return default;
            return tileData.Value.Tile.TryGetComponent(out T t) ? t : default;
        }

        private TileData? GetTileDataByCoordinate(Vector2 coordinate)
        {
            return _tileDataList.FirstOrDefault(tileData =>
                GridHelper.CheckOverlapWithDot(tileData.BottomLeft, tileData.TopRight, coordinate));
        }

        private void CreateTile()
        {
            var tempList = new List<TileData>();

            for (var i = 0; i < _gridList.Count; i++)
            {
                var coordinate = _gridList[i];
                var iTile = i <= (int)(_gridList.Count * 0.5f)
                    ? woodFactory.CreateTile(coordinate)
                    : gemFactory.CreateTile(coordinate);
                var bottomLeft = coordinate - _halfGridSize;
                var topRight = coordinate + _halfGridSize;

                tempList.Add(new TileData(coordinate, iTile, bottomLeft, topRight));
            }

            _tileDataList.AddRange(tempList);

            Parallel.ForEach(tempList, tileData =>
            {
                var neighborTileList = GetNeighborTileList(tileData.Coordinate);
                tileData.SetNeighborTiles(neighborTileList);
            });
        }

        private TileData?[] GetNeighborTileList(Vector2 coordinate)
        {
            var array = DirectionHelper.GetAsArray();
            var tileDataList = new TileData?[array.Length];

            for (var i = 0; i < array.Length; i++)
            {
                var direction = array[i];
                var newCoordinate = coordinate + direction.ToVector();

                var result = GetTileDataByCoordinate(newCoordinate);
                tileDataList[i] = result;
            }

            return tileDataList;
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