using System;
using _Game.TileSystem.Tile.Scripts;
using UnityEngine;

namespace _Game.TileSystem.Elements.Gem.Scripts
{
    [Serializable]
    public class GemElementData : ElementData
    {
        [Header("Child Parameters")] public GemId gemId;

        public GemElementData()
        {
            tileId = TileId.Gem;
        }
    }
}