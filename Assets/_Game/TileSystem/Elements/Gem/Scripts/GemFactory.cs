using System.Collections.Generic;
using System.Linq;
using _Game.TileSystem.Tile.Scripts;
using UnityEngine;
using Zenject;
using TileData = _Game.Level.Scripts.TileData;

namespace _Game.TileSystem.Elements.Gem.Scripts
{
    public class GemFactory : TileFactory
    {
        [Header("References")] [SerializeField]
        private List<GemElementDataSo> gemDataSoList;

        [Header("References")] [SerializeField]
        private Gem gemPrefab;

        protected override void Awake()
        {
            TileConstants.FactoryList.TryAdd(TileId.Gem, this);
        }

        public override GameObject Create(TileData tileData)
        {
            var tilePropertyGem = (GemElementData)tileData.elementData;
            var gem = GetGemInPool();

            var iTile = gem.GetComponent<ITile>();
            var iGem = gem.GetComponent<IGem>();

            iTile.SetPosition(tileData.coordinate);
            iTile.SetParent(transform);
            iTile.TileId = TileId.Gem;

            var gemDataSo = gemDataSoList.First(x => x.data.gemId == tilePropertyGem.gemId);

            iGem.SetGemId(gemDataSo.data.gemId);
            iGem.SetSprite(gemDataSo.data.icon);

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
        private readonly List<GameObject> _createdGemList = new();

        #endregion
    }
}