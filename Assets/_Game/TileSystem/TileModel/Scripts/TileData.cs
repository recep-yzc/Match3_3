using System;
using UnityEngine;

namespace _Game.TileSystem.TileModel.Scripts
{
    [Serializable]
    public struct TileData
    {
        public TileData(Vector2 coordinate, Tile tile, Vector2 bottomLeft, Vector2 topRight)
        {
            Coordinate = coordinate;
            Tile = tile;
            BottomLeft = bottomLeft;
            TopRight = topRight;
        }

        public Vector2 Coordinate { get; private set; }
        public Tile Tile { get; private set; }
        public Vector2 BottomLeft { get; private set; }
        public Vector2 TopRight { get; private set; }
    }
}