using _Game.GridSystem.GridModel.Scripts.Scriptable;
using UnityEngine;

namespace _Game.GridSystem.GridModel.Scripts.Handlers
{
    public class GridDrawHandler : MonoBehaviour
    {
        [SerializeField] private GridDataSo gridDataSo;

        private void OnDrawGizmos()
        {
            foreach (var vector2 in gridDataSo.allGridList)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawWireCube(vector2, Vector3.one);
            }

            foreach (var vector2 in gridDataSo.playableGridList)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(vector2, Vector3.one * 0.9f);
            }
        }
    }
}