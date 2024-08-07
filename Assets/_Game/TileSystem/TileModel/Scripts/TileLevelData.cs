using System;
using _Game.TileSystem.GemModel.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.TileSystem.TileModel.Scripts
{
    [Serializable]
    public class TileLevelData
    {
        public TileId tileId;
        [HideInInspector] public Vector2 coordinate;

        [ShowIf("tileId", TileId.Gem)] public GemId gemId;
    }
}