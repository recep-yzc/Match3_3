using UnityEngine;

namespace _Game.TileSystem.TileModel.Scripts
{
    public abstract class Tile : MonoBehaviour
    {
        public void SetPosition(Vector2 coordinate)
        {
            transform.position = coordinate;
        }
        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }
    }
}