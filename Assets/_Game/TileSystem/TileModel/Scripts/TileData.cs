using System;
using UnityEngine;

namespace _Game.TileSystem.TileModel.Scripts
{
    [Serializable]
    public struct TileData
    {
        public TileData(Vector2 coordinate, GameObject tile, Vector2 bottomLeft, Vector2 topRight)
        {
            Coordinate = coordinate;
            Tile = tile;
            BottomLeft = bottomLeft;
            TopRight = topRight;
            NeighborTiles = new TileData[4];
        }

        public void SetNeighborTiles(TileData[] neighborTiles)
        {
            Array.Copy(neighborTiles, NeighborTiles, NeighborTiles.Length);
        }

        public Vector2 Coordinate { get; }
        public GameObject Tile { get; }
        public Vector2 BottomLeft { get; }
        public Vector2 TopRight { get; }
        public TileData[] NeighborTiles { get; private set; }
    }
}