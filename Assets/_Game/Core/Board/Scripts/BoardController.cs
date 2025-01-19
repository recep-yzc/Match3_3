using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Game.Core.Board.Scripts
{
    [DefaultExecutionOrder(-1)]
    public class BoardController : MonoBehaviour
    {
        private void Start()
        {
            Application.targetFrameRate = 60;
            Init().Forget();
        }

        private UniTaskVoid Init()
        {
            _boardSpawnController.FetchLevelDataSo();
            _boardSpawnController.ClearTile();
            _boardSpawnController.CreateGridData();
            _boardSpawnController.FetchGridNeighbor();

            _boardViewController.TryUpdateView().Forget();

            return default;
        }

        #region Parameters
        
        [Inject] private BoardSpawnController _boardSpawnController;
        [Inject] private BoardViewController _boardViewController;
        [Inject] private BoardInputController _boardInputController;
        [Inject] private BoardBlastController _boardBlastController;
        [Inject] private BoardFallController _boardFallController;
        
        [Inject] private BoardScaleUpDownController _boardScaleUpDownController;
        [Inject] private BoardShakeController _boardShakeController;
        
        #endregion
    }
}