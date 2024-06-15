using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game.GridSystem.GridModel.Scripts
{
    public class GridEditController : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private GridDataSo gridDataSo;

        [SerializeField] [ReadOnly] private bool isEditScene;

        [Header("Properties")] [SerializeField]
        private KeyCode enableDisableKey;

        private void Update()
        {
            if (!isEditScene) return;
            if (!Input.anyKeyDown) return;

            var inputPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

            foreach (var grid in gridDataSo.allGridList)
            {
                var bottomLeft = grid - _halfGridSize;
                var topRight = grid + _halfGridSize;

                var isDotIn = GridHelper.CheckOverlapWithDot(bottomLeft, topRight, inputPosition);
                if (!isDotIn) continue;

                if (Input.GetKeyDown(enableDisableKey)) EnableDisable(grid);

                gridDataSo.Save();
                break;
            }
        }

        #region First

        private void OnValidate()
        {
            isEditScene = SceneManager.GetActiveScene().name.Contains("EditScene");
            _mainCamera = Camera.main;
        }

        #endregion

        private void EnableDisable(Vector2 grid)
        {
            if (gridDataSo.playableGridList.Contains(grid))
                gridDataSo.playableGridList.Remove(grid);
            else
                gridDataSo.playableGridList.Add(grid);
        }

        #region Private

        private readonly Vector2 _halfGridSize = new(0.5f, 0.5f);
        private Camera _mainCamera;

        #endregion
    }
}