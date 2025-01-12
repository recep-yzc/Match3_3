using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Game.BoardSystem.Scripts
{
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
            var tileData = BoardHelper.GetTileDataByCoordinate(inputPosition);
            if (tileData is null) return;
            if (tileData.IsEmpty) return;

            var blastedTileDataList = await _boardBlastController.TryBlast(tileData);
            if (blastedTileDataList?.Count > 0)
            {
                _boardFallController.TryFall().Forget();
                _boardController.TryCreate().Forget();
                _boardFallController.TryFall().Forget();

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
        
        [Inject] private BoardController _boardController;
        [Inject] private BoardFallController _boardFallController;
        [Inject] private BoardBlastController _boardBlastController;
        [Inject] private BoardShakeController _boardShakeController;
        [Inject] private BoardScaleUpDownController _boardScaleUpDownController;

        private Camera _mainCamera;

        #endregion
    }
}