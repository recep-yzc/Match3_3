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
            NeighborTileData = new TileData[4];
        }

        public void SetNeighborTileData(TileData[] neighborTileData)
        {
            Array.Copy(neighborTileData, NeighborTileData, NeighborTileData.Length);
        }

        #region Public

        public Vector2 Coordinate { get; }
        public GameObject Tile { get; }
        public Vector2 BottomLeft { get; }
        public Vector2 TopRight { get; }
        public TileData[] NeighborTileData { get; private set; }

        #endregion
    }
}