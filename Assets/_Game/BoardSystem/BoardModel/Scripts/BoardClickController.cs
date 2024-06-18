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
            var tileData = BoardHelper.GetTileDataByCoordinate(BoardConstants.TileData, inputPosition);
            if (tileData.Tile is null) return;

            await CheckBlast(tileData);

            CheckShake(tileData);
            CheckScaleUpDown(tileData);
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

        private async UniTask CheckBlast(TileData tileData)
        {
            // blast.BlastId

            var sameTileList = await GetSimilarTileAsComponent<IBlast>(tileData);
            sameTileList.ForEach(x => x.Blast());
        }

        private async UniTask<List<T>> GetSimilarTileAsComponent<T>(TileData tileData)
        {
            var similarTiles = new List<T>();
            await FindSimilarTileAsComponent(similarTiles, tileData);
            return similarTiles;
        }

        private async UniTask FindSimilarTileAsComponent<T>(List<T> similarTiles, TileData tileData)
        {
            var tileAsComponent = GetTileAsComponent<T>(tileData.Tile);
            if (tileAsComponent is null || similarTiles.Contains(tileAsComponent)) return;

            similarTiles.Add(tileAsComponent);

            foreach (var neighborTileData in tileData.NeighborTiles)
            {
                if (neighborTileData is { Tile: not null }) 
                    await FindSimilarTileAsComponent(similarTiles, neighborTileData);
            }
        }

        private T GetTileAsComponent<T>(GameObject tile)
        {
            return tile.TryGetComponent(out T t) ? t : default;
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