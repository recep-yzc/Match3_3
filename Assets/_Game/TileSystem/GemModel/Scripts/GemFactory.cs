using System.Collections.Generic;
using System.Linq;
using _Game.TileSystem.TileModel.Scripts;
using UnityEngine;
using Zenject;

namespace _Game.TileSystem.GemModel.Scripts
{
    public class GemFactory : TileFactory
    {
        [Header("References")] [SerializeField]
        private Gem gemPrefab;

        public override GameObject CreateTile(TileLevelData tileLevelData)
        {
            var gem = GetGemInPool();

            var iTile = gem.GetComponent<ITile>();
            var iGem = gem.GetComponent<IGem>();

            iTile.SetPosition(tileLevelData.coordinate);
            iTile.SetParent(transform);
            iTile.TileId = TileId.Gem;

            var sprite = _gemDataSo.GetSpriteByGemId(tileLevelData.gemId);

            iGem.SetGemId(tileLevelData.gemId);
            iGem.SetSprite(sprite);

            return gem;
        }

        private GameObject GetGemInPool()
        {
            var gem = _createdGemList.FirstOrDefault(x => !x.activeInHierarchy);
            if (gem != null)
            {
                gem.SetActive(true);
                return gem;
            }

            gem = _diContainer.InstantiatePrefab(gemPrefab);
            _createdGemList.Add(gem);

            return gem;
        }

        #region Parameters

        [Inject] private DiContainer _diContainer;
        [Inject] private GemDataSo _gemDataSo;
        private readonly List<GameObject> _createdGemList = new();

        #endregion
    }
}