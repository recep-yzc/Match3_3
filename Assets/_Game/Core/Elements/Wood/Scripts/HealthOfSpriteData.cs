using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Core.Elements.Wood.Scripts
{
    [Serializable]
    public class HealthOfSpriteData
    {
        public int health;
        [PreviewField] public Sprite sprite;
    }
}