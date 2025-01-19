using System;
using _Game.Core.Elements.Element.Scripts;
using UnityEngine;

namespace _Game.Core.Elements.Gem.Scripts
{
    [Serializable]
    public class GemElementData : ElementDataBase
    {
        [Header("Child Parameters")] public GemId gemId;
        public LevelOfSpriteData[] levelOfSpriteDataList;

        public GemElementData()
        {
            elementId = ElementId.Gem;
        }
    }
}