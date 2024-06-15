using UnityEngine;

namespace _Game.GridSystem.GridModel.Scripts
{
    public static class GridHelper
    {
        public static bool CheckOverlapWithDot(Vector2 bottomLeft, Vector2 topRight, Vector2 point)
        {
            return point.x > bottomLeft.x && point.x < topRight.x && point.y < topRight.y && point.y > bottomLeft.y;
        }
    }
}