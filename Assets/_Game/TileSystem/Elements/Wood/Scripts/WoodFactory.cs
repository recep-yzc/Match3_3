using System.Collections.Generic;
using System.Linq;
using _Game.TileSystem.Tile.Scripts;
using UnityEngine;
using Zenject;
using TileData = _Game.Level.Scripts.TileData;

namespace _Game.TileSystem.Elements.Wood.Scripts
{
    [DefaultExecutionOrder(-2)]
    public class WoodFactory : TileFactory
    {
        [Header("References")] [SerializeField]
        private List<WoodElementDataSo> woodDataSoList;

        [Header("References")] [SerializeField]
        private Wood woodPrefab;

        protected override void Start()
        {
            TileConstants.FactoryList.TryAdd(TileId.Wood, this);
        }

        public override GameObject Create(TileData tileData)
        {
            var wood = GetWoodInPool();

            var iWood = wood.GetComponent<IWood>();
            var iTile = wood.GetComponent<ITile>();

            iTile.SetPosition(tileData.coordinate);
            iTile.SetParent(transform);
            iTile.SetTileId(TileId.Wood);

            var woodElementData = (WoodElementData)tileData.elementData;
            var woodDataSo = woodDataSoList.First(x => x.data.health == woodElementData.health);

            iWood.SetShield(2);
            iWood.SetSprite(woodDataSo.data.icon);

            return wood;
        }

        private GameObject GetWoodInPool()
        {
            var wood = _createdWoodList.FirstOrDefault(x => !x.activeInHierarchy);
            if (wood != null)
            {
                wood.SetActive(true);
                return wood;
            }

            wood = _diContainer.InstantiatePrefab(woodPrefab);
            _createdWoodList.Add(wood);

            return wood;
        }

        #region Parameters

        private readonly List<GameObject> _createdWoodList = new();
        [Inject] private DiContainer _diContainer;

        #endregion
    }
}