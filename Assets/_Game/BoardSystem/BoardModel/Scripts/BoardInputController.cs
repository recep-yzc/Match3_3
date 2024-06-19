using System.Threading.Tasks;
using _Game.TileSystem.TileModel.Scripts;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Game.BoardSystem.BoardModel.Scripts
{
    public class BoardInputController : MonoBehaviour
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
            if (tileData is null) return;

            if (await TryBlast(tileData)) return;

            _boardShakeController.TryShake(tileData);
            _boardScaleUpDownController.TryScaleUpDown(tileData);
        }

        private async Task<bool> TryBlast(TileData tileData)
        {
            var result = await _boardBlastController.TryBlast(tileData);
            if (result is null) return false;

            foreach (var blastTileData in result)
            {
            }

            return result.Count > 0;
        }

        private void FetchCameraData()
        {
            _mainCamera = Camera.main;
        }

        #region Private

        #region Controller

        [Inject] private BoardBlastController _boardBlastController;
        [Inject] private BoardShakeController _boardShakeController;
        [Inject] private BoardScaleUpDownController _boardScaleUpDownController;

        #endregion

        private Camera _mainCamera;

        #endregion
    }
}