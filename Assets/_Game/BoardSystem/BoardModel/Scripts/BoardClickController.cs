using System.Collections.Generic;
using _Game.TileSystem.AbilityModel.Blast.Scripts;
using _Game.TileSystem.AbilityModel.ScaleUpDown.Scripts;
using _Game.TileSystem.AbilityModel.Shake.Scripts;
using _Game.TileSystem.DirectionModel.Scripts;
using _Game.TileSystem.TileModel.Scripts;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Game.BoardSystem.BoardModel.Scripts
{
    public class BoardClickController : MonoBehaviour
    {
        #region First

        private void Start()
        {
            FetchCameraData();
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
            var result = BoardHelper.GetTileDataByCoordinate(BoardConstants.TileData, inputPosition);
            if (result?.Tile is null) return;

            await CheckBlast(inputPosition, result.Value);

            CheckShake(result.Value);
            CheckScaleUpDown(result.Value);
        }

        private void CheckScaleUpDown(TileData tileData)
        {
            if (!tileData.Tile.TryGetComponent(out IScaleUpDown scaleUpDown)) return;
            scaleUpDown.ScaleUpDownAsync(_scaleUpDownDataSo).Forget();
        }

        private void CheckShake(TileData tileData)
        {
            if (!tileData.Tile.TryGetComponent(out IShake shake)) return;
            shake.ShakeAsync(_shakeDataSo).Forget();
        }

        private async UniTask CheckBlast(Vector3 inputPosition, TileData tileData)
        {
            if (!tileData.Tile.TryGetComponent(out IBlast blast)) return;
            var sameTileList = await GetSimilarTileAsComponent<IBlast>(inputPosition);
            sameTileList.ForEach(x => x.Blast());
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

            sameTileList.Add(result);

            foreach (var direction in DirectionHelper.GetAsArray())
            {
                var newCoordinate = coordinate + direction.ToVector();
                await FindSimilarTileAsComponent(sameTileList, newCoordinate);
            }
        }

        private T GetTileAsComponent<T>(Vector2 coordinate)
        {
            var result = BoardHelper.GetTileDataByCoordinate(BoardConstants.TileData, coordinate);
            if (result?.Tile is null) return default;
            return result.Value.Tile.TryGetComponent(out T t) ? t : default;
        }

        private void FetchCameraData()
        {
            _mainCamera = Camera.main;
        }

        #region Private

        #region Ability

        [Inject] private ShakeDataSo _shakeDataSo;
        [Inject] private ScaleUpDownDataSo _scaleUpDownDataSo;

        #endregion

        private Camera _mainCamera;

        #endregion
    }
}