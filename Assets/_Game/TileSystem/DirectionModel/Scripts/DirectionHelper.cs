using System;
using UnityEngine;

namespace _Game.TileSystem.DirectionModel.Scripts
{
    public static class DirectionHelper
    {
        public static Vector2 ToVector(this DirectionId directionId)
        {
            return directionId switch
            {
                DirectionId.Left => Vector2.left,
                DirectionId.Right => Vector2.right,
                DirectionId.Up => Vector2.up,
                DirectionId.Down => Vector2.down,
                _ => Vector2.zero
            };
        }

        public static DirectionId[] GetAsArray()
        {
            return (DirectionId[])Enum.GetValues(typeof(DirectionId));
        }
    }
}