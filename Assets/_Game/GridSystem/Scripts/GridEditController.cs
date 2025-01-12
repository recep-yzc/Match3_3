using _Game.TileSystem.GemModel.Scripts;
using _Game.TileSystem.TileModel.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game.GridSystem.Scripts
{
    public class GridEditController : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private GridDataSo gridDataSo;

        [SerializeField] [ReadOnly] private bool isEditScene;

        [Header("Properties")] [SerializeField]
        private TileId tileId;

        [ShowIf("tileId", TileId.Gem)] [SerializeField]
        private GemId gemId;

        [SerializeField] private KeyCode changeTile;

        private void Update()
        {
            if (!isEditScene) return;
            if (!Input.anyKeyDown) return;

            var inputPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

            foreach (var tileLevelData in gridDataSo.tileLevelData)
            {
                var bottomLeft = tileLevelData.coordinate - _halfGridSize;
                var topRight = tileLevelData.coordinate + _halfGridSize;

                var isDotIn = GridHelper.CheckOverlapWithDot(bottomLeft, topRight, inputPosition);
                if (!isDotIn) continue;

                if (Input.GetKeyDown(changeTile)) ChangeTileData(tileLevelData);

                gridDataSo.Save();
                break;
            }
        }

        private void OnValidate()
        {
            isEditScene = SceneManager.GetActiveScene().name.Contains("EditScene");
            _mainCamera = Camera.main;
        }

        private void ChangeTileData(TileLevelData tileLevelData)
        {
            tileLevelData.tileId = tileId;
            tileLevelData.gemId = gemId;
        }

        #region Parameters

        private readonly Vector2 _halfGridSize = new(0.5f, 0.5f);
        private Camera _mainCamera;

        #endregion
    }
}