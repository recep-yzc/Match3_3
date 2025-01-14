using System;
using _Game.TileSystem.Tile.Scripts;
using UnityEngine;

namespace _Game.TileSystem.Elements.Wood.Scripts
{
    [Serializable]
    public class WoodElementData : ElementData
    {
        [Header("Child Parameters")] public int health;

        public WoodElementData()
        {
            tileId = TileId.Wood;
        }
    }
}