using System;
using UnityEngine;

namespace _Game.BoardSystem.BoardModel.Scripts
{
    public static class DirectionHelper
    {
        public static Vector2 ToVector(this Direction direction)
        {
            return direction switch
            {
                Direction.Left => Vector2.left,
                Direction.Right => Vector2.right,
                Direction.Up => Vector2.up,
                Direction.Down => Vector2.down,
                _ => Vector2.zero,
            };
        }

        public static Direction[] GetAsArray()
        {
            return (Direction[])Enum.GetValues(typeof(Direction));
        }
    }
}