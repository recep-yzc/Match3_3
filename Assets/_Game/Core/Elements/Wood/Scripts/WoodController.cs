using System.Collections.Generic;
using _Game.Core.Elements.Element.Scripts;
using UnityEngine;

namespace _Game.Core.Elements.Wood.Scripts
{
    public class WoodController : ElementControllerBase
    {
        #region Parameters

        private static readonly List<HealthOfSpriteData> HealthOfSpriteDataListByWoodId = new();

        #endregion

        [Header("References")] [SerializeField]
        private WoodElementDataSo woodElementDataSo;

        #region UnityActions

        private void Start()
        {
            HealthOfSpriteDataListByWoodId.AddRange(woodElementDataSo.data.healthOfSpriteDataList);
        }

        #endregion

        public Sprite GetSprite(int health)
        {
            foreach (var levelOfSpriteData in HealthOfSpriteDataListByWoodId)
                if (levelOfSpriteData.health == health)
                    return levelOfSpriteData.sprite;

            return HealthOfSpriteDataListByWoodId[0].sprite;
        }

        public WoodElementDataSo GetWoodDataSo()
        {
            return woodElementDataSo;
        }
    }
}