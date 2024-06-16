using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
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
            NeighborTiles = new TileData?[4];
        }

        public void SetNeighborTiles(TileData?[] neighborTiles)
        {
            for (var i = 0; i < NeighborTiles.Length; i++)
            {
                NeighborTiles[i] = neighborTiles[i];
            }
        }

        public Vector2 Coordinate { get; }
        public Tile Tile { get; }
        public Vector2 BottomLeft { get; }
        public Vector2 TopRight { get; }
        public TileData?[] NeighborTiles { get; private set; }
    }
}