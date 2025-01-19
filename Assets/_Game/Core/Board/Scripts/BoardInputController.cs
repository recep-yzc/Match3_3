using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Game.Core.Board.Scripts
{
    [DefaultExecutionOrder(-1)]
    public class BoardInputController : MonoBehaviour
    {
        private void Start()
        {
            FetchCameraData();
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;

            var inputPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            HandleTileClick(inputPosition).Forget();
        }

        private async UniTask HandleTileClick(Vector3 inputPosition)
        {
            var tileData = BoardHelper.GetGridDataByCoordinate(inputPosition);
            if (tileData is null) return;
            if (tileData.IsEmpty) return;

            var blastedTileDataList = await _boardBlastController.TryBlast(tileData);
            if (blastedTileDataList?.Count > 0)
            {
                _boardFallController.TryFall().Forget();
                _boardSpawnController.TryCreate().Forget();

                _boardFallController.TryFall().Forget();
                await _boardViewController.TryUpdateView();
                return;
            }

            _boardShakeController.TryShake(tileData);
            _boardScaleUpDownController.TryScaleUpDown(tileData);
        }

        private void FetchCameraData()
        {
            _mainCamera = Camera.main;
        }

        #region Parameters

        [Inject] private BoardSpawnController _boardSpawnController;
        [Inject] private BoardFallController _boardFallController;
        [Inject] private BoardViewController _boardViewController;
        [Inject] private BoardBlastController _boardBlastController;
        [Inject] private BoardShakeController _boardShakeController;
        [Inject] private BoardScaleUpDownController _boardScaleUpDownController;

        private Camera _mainCamera;

        #endregion
    }
}