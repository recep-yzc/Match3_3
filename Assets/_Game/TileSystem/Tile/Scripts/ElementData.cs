using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.TileSystem.Tile.Scripts
{
    [Serializable]
    public abstract class ElementData : ICloneable
    {
        [Header("Base Parameters")] public TileId tileId;

        [PreviewField] public Sprite icon;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}