using System;
using _Game.Core.Elements.Element.Scripts;
using UnityEngine;

namespace _Game.Core.Elements.Wood.Scripts
{
    [Serializable]
    public class WoodElementData : ElementDataBase
    {
        [Header("Child Parameters")] public HealthOfSpriteData[] healthOfSpriteDataList;

        public WoodElementData()
        {
            elementId = ElementId.Wood;
        }
    }
}