using System;
using _Game.Code.Scripts.TileSystem.Tile.Handlers;
using UnityEngine;

namespace _Game.Code.Scripts.TileSystem.Tile.Utilities
{
    [Serializable]
    public struct TileData
    {
        public TileData(Vector2 coordinate, TileHandler tileHandler, Vector2 bottomLeft, Vector2 topRight)
        {
            Coordinate = coordinate;
            TileHandler = tileHandler;
            BottomLeft = bottomLeft;
            TopRight = topRight;
        }

        public Vector2 Coordinate { get; private set; }
        public TileHandler TileHandler { get; private set; }
        public Vector2 BottomLeft { get; private set; }
        public Vector2 TopRight { get; private set; }
    }
}