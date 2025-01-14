using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.TileSystem.Elements.Gem.Scripts
{
    [Serializable]
    public struct GemSpriteData
    {
        public GemId gemId;
        [PreviewField] public Sprite sprite;
    }
}