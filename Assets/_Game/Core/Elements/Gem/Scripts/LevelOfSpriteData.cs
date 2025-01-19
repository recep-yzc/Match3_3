using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Core.Elements.Gem.Scripts
{
    [Serializable]
    public class LevelOfSpriteData
    {
        public int level;
        [PreviewField] public Sprite sprite;
    }
}