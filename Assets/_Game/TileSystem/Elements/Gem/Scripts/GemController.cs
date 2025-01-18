using System.Collections.Generic;
using UnityEngine;

namespace _Game.TileSystem.Elements.Gem.Scripts
{
    [DefaultExecutionOrder(-3)]
    public class GemController : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private GemElementDataSo[] gemElementDataSoList;

        #region UnityActions

        private void Start()
        {
            foreach (var gemElementDataSo in gemElementDataSoList)
            {
                var gemId = gemElementDataSo.data.gemId;

                GemElementDataSoByGemId.Add(gemId, gemElementDataSo);
                LevelOfSpriteDataListByGemId.Add(gemId, gemElementDataSo.data.levelOfSpriteDataList);
            }
        }

        #endregion

        public Sprite GetSprite(GemId gemId, int level)
        {
            var levelOfSpriteDataList = LevelOfSpriteDataListByGemId[gemId];
            for (var i = levelOfSpriteDataList.Length - 1; i >= 0; i--)
            {
                var levelOfSpriteData = levelOfSpriteDataList[i];
                if (levelOfSpriteData.level <= level)
                    return levelOfSpriteData.sprite;
            }

            return levelOfSpriteDataList[0].sprite;
        }

        public GemElementDataSo GetGemDataSo(GemId gemId)
        {
            return GemElementDataSoByGemId[gemId];
        }

        #region Parameters

        private static readonly Dictionary<GemId, LevelOfSpriteData[]> LevelOfSpriteDataListByGemId = new();
        private static readonly Dictionary<GemId, GemElementDataSo> GemElementDataSoByGemId = new();

        #endregion
    }
}