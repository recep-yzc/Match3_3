using System;
using _Game.Core.Grid.Scripts;
using UnityEngine;

namespace _Game.Utilities.Scripts
{
    public static class VectorHelper
    {
        public static readonly Vector2 HalfSize = new(0.5f, 0.5f);
        public static readonly Vector2 Size = Vector2.one;

        public static bool CheckOverlapWithDot(Vector2 bottomLeft, Vector2 topRight, Vector2 point)
        {
            return point.x > bottomLeft.x && point.x < topRight.x && point.y < topRight.y && point.y > bottomLeft.y;
        }

        public static Vector2 DirectionToVector(this DirectionType directionType)
        {
            return directionType switch
            {
                DirectionType.Left => Vector2.left,
                DirectionType.Right => Vector2.right,
                DirectionType.Up => Vector2.up,
                DirectionType.Down => Vector2.down,
                _ => Vector2.zero
            };
        }

        public static DirectionType[] GetAsArray()
        {
            return (DirectionType[])Enum.GetValues(typeof(DirectionType));
        }
    }
}