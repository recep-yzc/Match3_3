using _Game.TileSystem.TileModel.Scripts;
using UnityEngine;

namespace _Game.GridSystem.Scripts
{
    public class GridDrawHandler : MonoBehaviour
    {
        [SerializeField] private GridDataSo gridDataSo;

        private void OnDrawGizmos()
        {
            foreach (var tileLevelData in gridDataSo.tileLevelData)
            {
                var coordinate = tileLevelData.coordinate;

                switch (tileLevelData.tileId)
                {
                    case TileId.Empty:
                        //Gizmos.DrawIcon(coordinate, "", true);
                        break;
                    case TileId.Gem:
                        Gizmos.DrawIcon(coordinate, tileLevelData.gemId.ToString(), true);
                        break;
                    case TileId.Wood:
                        //Gizmos.DrawIcon(coordinate, "", true);
                        break;
                }

                Gizmos.color = Color.white;
                Gizmos.DrawWireCube(coordinate, Vector3.one);
            }
        }
    }
}