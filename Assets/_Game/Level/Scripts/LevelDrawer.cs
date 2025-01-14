using _Game.TileSystem.Tile.Scripts;
using _Game.Utilities.Scripts;
using UnityEngine;

namespace _Game.Level.Scripts
{
    public class LevelDrawer : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private LevelGenerator levelGenerator;

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;

            DrawGrid();
            DrawMouseIcon();
        }

        private void DrawGrid()
        {
            foreach (var tileLevelData in levelGenerator.levelDataSo.tileDataList)
            {
                if (tileLevelData.elementData.icon == null) continue;

                var coordinate = tileLevelData.coordinate;
                Gizmos.DrawIcon(coordinate, "Resources/" + tileLevelData.elementData.icon.name, true);
                Gizmos.DrawWireCube(coordinate, VectorHelper.Size);
            }

            var row = (float)levelGenerator.levelDataSo.rows;
            var column = (float)levelGenerator.levelDataSo.columns;

            var gridCenterOffset = new Vector2(-row / 2, -column / 2);
            for (var x = 0; x < row; x++)
            for (var y = 0; y < column; y++)
            {
                var cellCenter = new Vector2(x, y) + gridCenterOffset + VectorHelper.HalfSize;
                Gizmos.DrawWireCube(cellCenter, VectorHelper.Size);
            }
        }

        private void DrawMouseIcon()
        {
            var tilePropertyDataSo = TileConstants.SelectedElementDataSo;
            if (tilePropertyDataSo == null) return;

            var mousePosition = Input.mousePosition;
            mousePosition.z = 10;
            if (Camera.main == null) return;

            var worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            var elementData = tilePropertyDataSo.GetElementData();

            Gizmos.DrawIcon(worldPosition, "Resources/" + elementData.icon.name, true);
            Gizmos.DrawWireCube(worldPosition, VectorHelper.Size);
        }
    }
}